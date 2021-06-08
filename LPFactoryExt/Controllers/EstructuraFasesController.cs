using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LPFactory.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LPFactory.Controllers
{
    [Authorize]
    public class EstructuraFasesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public EstructuraFasesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<int> GetEmpresaIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.EmpresaId;
        }

        // GET: EstructuraFases
        public async Task<IActionResult> Index(int id)
        {
            int empresaId = await GetEmpresaIdAsync();

            var querye = from ef in _context.EstructurasFases
                         where ef.EmpresaId == empresaId
                         where ef.EstructuraId == id
                         select ef;

            var fases = await querye.Include(e => e.Estructura).Include(e => e.Fase).ToListAsync();

            var estructura = await _context.Estructuras
                .Where(x => x.Id == id && x.EmpresaId == empresaId)
                .Include(x => x.Articulo)
                .FirstAsync();

            ViewData["NombreArticulo"] = estructura.Articulo.CodigoYDescripcion;
            ViewData["Version"] = estructura.Version;

            ViewData["EstructuraId"] = id;

            //var estructura = await querye.FirstOrDefaultAsync();
            //if (estructura == null)
            //{
            //    return NotFound();
            //}
            //var applicationDbContext = _context.EstructurasFases.Include(e => e.Empresa).Include(e => e.Estructura).Include(e => e.Fase);
            //return View(await applicationDbContext.Where(x => x.EstructuraId == estructuraId).ToListAsync());

            return View(fases);
        }

        // GET: EstructuraFases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int empresaId = await GetEmpresaIdAsync();

            var estructuraFase = await _context.EstructurasFases
                .Include(e => e.Empresa)
                .Include(e => e.Estructura)
                .ThenInclude(e => e.Articulo)
                .Include(e => e.Fase)
                .Include(e => e.Materiales)
                .ThenInclude(e => e.Articulo)
                .Include(e => e.Operaciones)
                .ThenInclude(e => e.Maquina)
                .Include(e => e.Operaciones)
                .ThenInclude(e => e.Operacion)
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId);
            if (estructuraFase == null)
            {
                return NotFound();
            }

            return View(estructuraFase);
        }

        // GET: EstructuraFases/Create
        public async Task<IActionResult> Create(int id)
        {
            int empresaId = await GetEmpresaIdAsync();

            ViewData["EstructuraId"] = id;
            ViewData["FaseId"] = new SelectList(_context.Fases.Where(x => x.EmpresaId == empresaId), "Id", "CodigoYDescripcion");
            return View();
        }

        // POST: EstructuraFases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstructuraId,FaseId")] EstructuraFase estructuraFase)
        {
            int empresaId = await GetEmpresaIdAsync();
            estructuraFase.EmpresaId = empresaId;
            if (ModelState.IsValid)
            {
                _context.Add(estructuraFase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = estructuraFase.EstructuraId });
            }
            ViewData["EstructuraId"] = estructuraFase.EstructuraId;
            ViewData["FaseId"] = new SelectList(_context.Fases.Where(x => x.EmpresaId == empresaId), "Id", "CodigoYDescripcion", estructuraFase.FaseId);
            return View(estructuraFase);
        }

        // GET: EstructuraFases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int empresaId = await GetEmpresaIdAsync();

            var estructuraFase = await _context.EstructurasFases
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId);
            if (estructuraFase == null)
            {
                return NotFound();
            }
            ViewData["EstructuraId"] = estructuraFase.EstructuraId;
            ViewData["FaseId"] = new SelectList(_context.Fases.Where(x => x.EmpresaId == empresaId), "Id", "CodigoYDescripcion", estructuraFase.FaseId);
            return View(estructuraFase);
        }

        // POST: EstructuraFases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpresaId,EstructuraId,FaseId")] EstructuraFase estructuraFase)
        {
            if (id != estructuraFase.Id)
            {
                return NotFound();
            }

            int empresaId = await GetEmpresaIdAsync();
            estructuraFase.EmpresaId = empresaId;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estructuraFase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstructuraFaseExists(estructuraFase.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = estructuraFase.EstructuraId });
            }
            ViewData["EstructuraId"] = estructuraFase.EstructuraId;
            ViewData["FaseId"] = new SelectList(_context.Fases.Where(x => x.EmpresaId == empresaId), "Id", "CodigoYDescripcion", estructuraFase.FaseId);
            return View(estructuraFase);
        }

        // GET: EstructuraFases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int empresaId = await GetEmpresaIdAsync();

            var estructuraFase = await _context.EstructurasFases
                .Include(e => e.Empresa)
                .Include(e => e.Estructura)
                .Include(e => e.Fase)
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId);
            if (estructuraFase == null)
            {
                return NotFound();
            }

            return View(estructuraFase);
        }

        // POST: EstructuraFases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estructuraFase = await _context.EstructurasFases.FindAsync(id);
            _context.EstructurasFases.Remove(estructuraFase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = estructuraFase.EstructuraId });
        }

        private bool EstructuraFaseExists(int id)
        {
            return _context.EstructurasFases.Any(e => e.Id == id);
        }
    }
}
