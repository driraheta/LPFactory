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
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace LPFactory.Controllers
{
    [Authorize]
    public class EmpresasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public EmpresasController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<int> GetEmpresaIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.EmpresaId;
        }

        // GET: Empresas
        public async Task<IActionResult> Index()
        {
            int EmpresaId = await GetEmpresaIdAsync();
            return View(await _context.Empresas.FindAsync(EmpresaId));
        }

        // GET: Empresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int EmpresaId = await GetEmpresaIdAsync();

            var empresa = await _context.Empresas
                .FirstOrDefaultAsync(m => m.Id == EmpresaId);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // GET: Empresas/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Codigo,Nombre,Activo,Direccion,NombreERP,LogoId")] Empresa empresa)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(empresa);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(empresa);
        //}

        // GET: Empresas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            int EmpresaId = await GetEmpresaIdAsync();

            var empresa = await _context.Empresas.FindAsync(EmpresaId);
            if (empresa == null)
            {
                return NotFound();
            }

            string logoImageBase64 = "";
            var recurso = await _context.Recursos
                .FirstOrDefaultAsync(x => x.EmpresaId == EmpresaId
                    && x.Tipo == TipoRecurso.Logo
                    && x.Id == empresa.LogoId);
            if (recurso != null)
            {
                string extension = Path.GetExtension(recurso.Nombre).Replace(".", "");
                logoImageBase64 = Convert.ToBase64String(recurso.Binario);
                logoImageBase64 = string.Format("data:image/{0};base64,{1}", extension, logoImageBase64);
            }

            ViewData["logoImageBase64"] = logoImageBase64;

            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,Nombre,Activo,Direccion,NombreERP,LogoId")] Empresa empresa, IFormFile ImageFile)
        {
            if (id != empresa.Id)
            {
                return NotFound();
            }

            int EmpresaId = await GetEmpresaIdAsync();

            empresa.Id = EmpresaId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresa);
                    await _context.SaveChangesAsync();

                    // Guardamos el logo
                    if (ImageFile != null)
                    {
                        // Borramos los logos de la empresa
                        _context.Recursos.RemoveRange(
                            _context.Recursos.Where(x => x.EmpresaId == EmpresaId && x.Tipo == TipoRecurso.Logo)
                        );

                        MemoryStream ms = new MemoryStream();
                        ImageFile.CopyTo(ms);

                        var recurso = new Recurso
                        {
                            Nombre = ImageFile.FileName,
                            Tipo = TipoRecurso.Logo,
                            EmpresaId = EmpresaId,
                            Binario = ms.ToArray()
                        };
                        _context.Recursos.Add(recurso);
                        await _context.SaveChangesAsync();

                        empresa.LogoId = recurso.Id;
                        _context.Entry(empresa).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        ms.Close();
                        ms.Dispose();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaExists(empresa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var empresa = await _context.Empresas
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (empresa == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(empresa);
        //}

        // POST: Empresas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var empresa = await _context.Empresas.FindAsync(id);
        //    _context.Empresas.Remove(empresa);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool EmpresaExists(int id)
        {
            return _context.Empresas.Any(e => e.Id == id);
        }
    }
}
