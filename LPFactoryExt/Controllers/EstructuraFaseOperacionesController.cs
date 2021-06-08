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
    public class EstructuraFaseOperacionesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public EstructuraFaseOperacionesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<int> GetEmpresaIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.EmpresaId;
        }

        // GET: EstructuraFaseOperaciones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EstructuraFaseOperaciones.Include(e => e.Empresa).Include(e => e.EstructuraFase).Include(e => e.Maquina);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EstructuraFaseOperaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int empresaId = await GetEmpresaIdAsync();

            var estructuraFaseOperacion = await _context.EstructuraFaseOperaciones
                .Include(e => e.Empresa)
                .Include(e => e.EstructuraFase)
                .Include(e => e.Maquina)
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId);
            if (estructuraFaseOperacion == null)
            {
                return NotFound();
            }

            return View(estructuraFaseOperacion);
        }

        // GET: EstructuraFaseOperaciones/Create
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

            var empresa = await _context.Empresas.Include(e => e.Operaciones).Include(e => e.Maquinas).FirstAsync(e => e.Id == empresaId);
            ViewData["EmpresaId"] = empresaId;
            ViewData["EstructuraFaseId"] = id;
            ViewData["MaquinaId"] = new SelectList(empresa.Maquinas, "Id", "CodigoYDescripcion");
            ViewData["OperacionId"] = new SelectList(empresa.Operaciones, "Id", "CodigoYDescripcion");
            return View(new EstructuraFaseOperacion { EstructuraFase = estructuraFase });
        }

        // POST: EstructuraFaseOperaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OperacionId,EstructuraFaseId,MaquinaId,TiempoPreparacion,TiempoProduccion,MermasTeoricas")] EstructuraFaseOperacion estructuraFaseOperacion)
        {
            int empresaId = await GetEmpresaIdAsync();
            estructuraFaseOperacion.EmpresaId = empresaId;
            if (ModelState.IsValid)
            {
                _context.Add(estructuraFaseOperacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), "EstructuraFases", new { id = estructuraFaseOperacion.EstructuraFaseId });
            }

            var empresa = await _context.Empresas.Include(e => e.Operaciones).Include(e => e.Maquinas).FirstAsync(e => e.Id == empresaId);
            ViewData["EmpresaId"] = empresaId;
            ViewData["EstructuraFaseId"] = estructuraFaseOperacion.EstructuraFaseId;
            ViewData["MaquinaId"] = new SelectList(empresa.Maquinas, "Id", "CodigoYDescripcion", estructuraFaseOperacion.MaquinaId);
            ViewData["OperacionId"] = new SelectList(empresa.Operaciones, "Id", "CodigoYDescripcion", estructuraFaseOperacion.OperacionId);
            return View(estructuraFaseOperacion);
        }

        // GET: EstructuraFaseOperaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int empresaId = await GetEmpresaIdAsync();

            var estructuraFaseOperacion = await _context.EstructuraFaseOperaciones
                .Include(e => e.EstructuraFase)
                .ThenInclude(e => e.Estructura)
                .ThenInclude(e => e.Articulo)
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId);
            if (estructuraFaseOperacion == null)
            {
                return NotFound();
            }
            var empresa = await _context.Empresas.Include(e => e.Operaciones).Include(e => e.Maquinas).FirstAsync(e => e.Id == empresaId);
            ViewData["EmpresaId"] = empresaId;
            ViewData["EstructuraFaseId"] = estructuraFaseOperacion.EstructuraFaseId;
            ViewData["MaquinaId"] = new SelectList(empresa.Maquinas, "Id", "CodigoYDescripcion", estructuraFaseOperacion.MaquinaId);
            ViewData["OperacionId"] = new SelectList(empresa.Operaciones, "Id", "CodigoYDescripcion", estructuraFaseOperacion.OperacionId);
            return View(estructuraFaseOperacion);
        }

        // POST: EstructuraFaseOperaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OperacionId,EmpresaId,EstructuraFaseId,MaquinaId,TiempoPreparacion,TiempoProduccion,MermasTeoricas")] EstructuraFaseOperacion estructuraFaseOperacion)
        {
            if (id != estructuraFaseOperacion.Id)
            {
                return NotFound();
            }

            int empresaId = await GetEmpresaIdAsync();
            estructuraFaseOperacion.EmpresaId = empresaId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estructuraFaseOperacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstructuraFaseOperacionExists(estructuraFaseOperacion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), "EstructuraFases", new { id = estructuraFaseOperacion.EstructuraFaseId });
            }
            var empresa = await _context.Empresas.Include(e => e.Operaciones).Include(e => e.Maquinas).FirstAsync(e => e.Id == empresaId);
            ViewData["EmpresaId"] = empresaId;
            ViewData["EstructuraFaseId"] = estructuraFaseOperacion.EstructuraFaseId;
            ViewData["MaquinaId"] = new SelectList(empresa.Maquinas, "Id", "CodigoYDescripcion", estructuraFaseOperacion.MaquinaId);
            ViewData["OperacionId"] = new SelectList(empresa.Operaciones, "Id", "CodigoYDescripcion", estructuraFaseOperacion.OperacionId);

            return View(estructuraFaseOperacion);
        }

        // GET: EstructuraFaseOperaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int empresaId = await GetEmpresaIdAsync();

            var estructuraFaseOperacion = await _context.EstructuraFaseOperaciones
                .Include(e => e.Empresa)
                .Include(e => e.EstructuraFase)
                .ThenInclude(e => e.Fase)
                .Include(e => e.Maquina)
                .Include(e => e.Operacion)
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId);
            if (estructuraFaseOperacion == null)
            {
                return NotFound();
            }

            return View(estructuraFaseOperacion);
        }

        // POST: EstructuraFaseOperaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estructuraFaseOperacion = await _context.EstructuraFaseOperaciones.FindAsync(id);
            _context.EstructuraFaseOperaciones.Remove(estructuraFaseOperacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), "EstructuraFases", new { id = estructuraFaseOperacion.EstructuraFaseId });
        }

        private bool EstructuraFaseOperacionExists(int id)
        {
            return _context.EstructuraFaseOperaciones.Any(e => e.Id == id);
        }
    }
}
