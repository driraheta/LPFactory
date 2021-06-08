using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LPFactory.Data;
using Microsoft.AspNetCore.Identity;

namespace LPFactory.Controllers
{
    public class EstructuraFaseMaterialesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public EstructuraFaseMaterialesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<int> GetEmpresaIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.EmpresaId;
        }

        // GET: EstructuraFaseMateriales
        public async Task<IActionResult> Index(int id)
        {
            int empresaId = await GetEmpresaIdAsync();

            var applicationDbContext = _context.EstructurasFasesMateriales
                .Include(e => e.Articulo)
                .Include(e => e.Empresa)
                .Include(e => e.EstructuraFase)
                .Where(e => e.EstructuraFaseId == id && e.EmpresaId == empresaId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EstructuraFaseMateriales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int empresaId = await GetEmpresaIdAsync();

            var estructuraFaseMaterial = await _context.EstructurasFasesMateriales
                .Include(e => e.Articulo)
                .Include(e => e.Empresa)
                .Include(e => e.EstructuraFase)
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId);
            if (estructuraFaseMaterial == null)
            {
                return NotFound();
            }

            return View(estructuraFaseMaterial);
        }

        // GET: EstructuraFaseMateriales/Create
        public async Task<IActionResult> Create(int id)
        {
            int empresaId = await GetEmpresaIdAsync();

            var estructuraFase = await _context.EstructurasFases
                .Include(e => e.Estructura)
                .ThenInclude(e => e.Articulo)
                .Include(e => e.Fase)
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId);
            if (estructuraFase == null)
            {
                return NotFound();
            }

            ViewData["ArticuloId"] = new SelectList(_context.Articulos.Where(x => x.EmpresaId == empresaId), "Id", "CodigoYDescripcion");
            ViewData["EmpresaId"] = empresaId;
            ViewData["EstructuraFaseId"] = id;
            return View(new EstructuraFaseMaterial { EstructuraFase = estructuraFase });
        }

        // POST: EstructuraFaseMateriales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpresaId,EstructuraFaseId,ArticuloId,Cantidad")] EstructuraFaseMaterial estructuraFaseMaterial)
        {
            int empresaId = await GetEmpresaIdAsync();
            estructuraFaseMaterial.EmpresaId = empresaId;
            if (ModelState.IsValid)
            {
                _context.Add(estructuraFaseMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), "EstructuraFases", new { id = estructuraFaseMaterial.EstructuraFaseId});
            }
            ViewData["ArticuloId"] = new SelectList(_context.Articulos.Where(x => x.EmpresaId == empresaId), "Id", "CodigoYDescripcion", estructuraFaseMaterial.ArticuloId);
            ViewData["EmpresaId"] = empresaId;
            ViewData["EstructuraFaseId"] = estructuraFaseMaterial.EstructuraFaseId;
            return View(estructuraFaseMaterial);
        }

        // GET: EstructuraFaseMateriales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int empresaId = await GetEmpresaIdAsync();

            var estructuraFaseMaterial = await _context.EstructurasFasesMateriales
                .Include(e => e.Articulo)
                .Include(e => e.EstructuraFase)
                .ThenInclude(e => e.Estructura)
                .ThenInclude(e => e.Articulo)
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId);
            if (estructuraFaseMaterial == null)
            {
                return NotFound();
            }
            ViewData["ArticuloId"] = new SelectList(_context.Articulos.Where(x => x.EmpresaId == empresaId), "Id", "CodigoYDescripcion", estructuraFaseMaterial.ArticuloId);
            ViewData["EmpresaId"] = empresaId;
            ViewData["EstructuraFaseId"] = estructuraFaseMaterial.EstructuraFaseId;
            return View(estructuraFaseMaterial);
        }

        // POST: EstructuraFaseMateriales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpresaId,EstructuraFaseId,ArticuloId,Cantidad")] EstructuraFaseMaterial estructuraFaseMaterial)
        {
            if (id != estructuraFaseMaterial.Id)
            {
                return NotFound();
            }

            int empresaId = await GetEmpresaIdAsync();
            estructuraFaseMaterial.EmpresaId = empresaId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estructuraFaseMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstructuraFaseMaterialExists(estructuraFaseMaterial.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), "EstructuraFases", new { id = estructuraFaseMaterial.EstructuraFaseId });
            }
            ViewData["ArticuloId"] = new SelectList(_context.Articulos.Where(x => x.EmpresaId == empresaId), "Id", "CodigoYDescripcion", estructuraFaseMaterial.ArticuloId);
            ViewData["EmpresaId"] = empresaId;
            ViewData["EstructuraFaseId"] = estructuraFaseMaterial.EstructuraFaseId;
            return View(estructuraFaseMaterial);
        }

        // GET: EstructuraFaseMateriales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int empresaId = await GetEmpresaIdAsync();

            var estructuraFaseMaterial = await _context.EstructurasFasesMateriales
                .Include(e => e.Articulo)
                .Include(e => e.Empresa)
                .Include(e => e.EstructuraFase)
                .ThenInclude(e => e.Fase)
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId);
            if (estructuraFaseMaterial == null)
            {
                return NotFound();
            }

            return View(estructuraFaseMaterial);
        }

        // POST: EstructuraFaseMateriales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estructuraFaseMaterial = await _context.EstructurasFasesMateriales.FindAsync(id);
            _context.EstructurasFasesMateriales.Remove(estructuraFaseMaterial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), "EstructuraFases", new { id = estructuraFaseMaterial.EstructuraFaseId });
        }

        private bool EstructuraFaseMaterialExists(int id)
        {
            return _context.EstructurasFasesMateriales.Any(e => e.Id == id);
        }
    }
}
