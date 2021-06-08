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
    public class EstructurasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public EstructurasController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<int> GetEmpresaIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.EmpresaId;
        }

        // GET: Estructuras
        public async Task<IActionResult> Index()
        {
            int EmpresaId = await GetEmpresaIdAsync();
            var applicationDbContext = _context.Estructuras.Include(e => e.Articulo).Include(e => e.Empresa);
            return View(await applicationDbContext.Where(x => x.EmpresaId == EmpresaId).ToListAsync());
        }

        // GET: Estructuras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int empresaId = await GetEmpresaIdAsync();

            var estructura = await _context.Estructuras
                .Include(e => e.Articulo)
                .Include(e => e.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId);
            if (estructura == null)
            {
                return NotFound();
            }

            return View(estructura);
        }

        // GET: Estructuras/Create
        public async Task<IActionResult> Create()
        {
            int empresaId = await GetEmpresaIdAsync();
            ViewData["ArticuloId"] = new SelectList(await _context.Articulos.Where(x => x.EmpresaId == empresaId).ToListAsync(), "Id", "CodigoYDescripcion");
            return View();
        }

        // POST: Estructuras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArticuloId,Version,Cantidad,Predeterminada,Activo")] Estructura estructura)
        {
            int empresaId = estructura.EmpresaId = await GetEmpresaIdAsync();
            if (ModelState.IsValid)
            {
                _context.Add(estructura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticuloId"] = new SelectList(await _context.Articulos.Where(x => x.EmpresaId == empresaId).ToListAsync(), "Id", "CodigoYDescripcion", estructura.ArticuloId);
            return View(estructura);
        }

        // GET: Estructuras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int empresaId = await GetEmpresaIdAsync();

            var estructura = await _context.Estructuras
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId);
            if (estructura == null)
            {
                return NotFound();
            }
            ViewData["ArticuloId"] = new SelectList(await _context.Articulos.Where(x => x.EmpresaId == empresaId).ToListAsync(), "Id", "CodigoYDescripcion", estructura.ArticuloId);
            ViewData["EmpresaId"] = empresaId;
            return View(estructura);
        }

        // POST: Estructuras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpresaId,ArticuloId,Version,Cantidad,Predeterminada,Activo")] Estructura estructura)
        {
            if (id != estructura.Id)
            {
                return NotFound();
            }

            int empresaId = await GetEmpresaIdAsync();
            estructura.EmpresaId = empresaId;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estructura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstructuraExists(estructura.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticuloId"] = new SelectList(await _context.Articulos.Where(x => x.EmpresaId == empresaId).ToListAsync(), "Id", "CodigoYDescripcion", estructura.ArticuloId);
            ViewData["EmpresaId"] = empresaId;
            return View(estructura);
        }

        // GET: Estructuras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int empresaId = await GetEmpresaIdAsync();

            var estructura = await _context.Estructuras
                .Include(e => e.Articulo)
                .Include(e => e.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId);
            if (estructura == null)
            {
                return NotFound();
            }

            return View(estructura);
        }

        // POST: Estructuras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estructura = await _context.Estructuras.FindAsync(id);
            _context.Estructuras.Remove(estructura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstructuraExists(int id)
        {
            return _context.Estructuras.Any(e => e.Id == id);
        }
    }
}
