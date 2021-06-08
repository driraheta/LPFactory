using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LPFactory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Omu.AwesomeMvc;
using LPFactory.ViewModels.Input;

namespace LPFactory.Controllers
{
    [Authorize]
    public class PlantasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public PlantasController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<int> GetEmpresaIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.EmpresaId;
        }

        // GET: Plantas
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            // make sure to use "return PartialView" for PopupForm/Popup views 
            // this will ignore _viewstart.cshtml so that you don't use the _Layout.cshtml and reload all the scripts
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlantaInput input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            var planta = new Planta
            {
                Codigo = input.Codigo,
                Nombre = input.Nombre,
                Direccion = input.Direccion,
                Activo = input.Activo,
                EmpresaId = await GetEmpresaIdAsync(),
            };
            _context.Add(planta);
            await _context.SaveChangesAsync();

            // the create PopupForm's success function utils.itemCreated expects the grid row model obj, to render and append the new row
            return Json(MapToGridModel(planta));
        }

        public async Task<IActionResult> Edit(int id)
        {
            int empresaId = await GetEmpresaIdAsync();

            var planta = await _context.Plantas.Where(x => x.Id == id && x.EmpresaId == empresaId).FirstOrDefaultAsync();
            if (planta == null)
            {
                return NotFound();
            }

            var input = new PlantaInput
            {
                Id = planta.Id,
                Codigo = planta.Codigo,
                Nombre = planta.Nombre,
                Direccion = planta.Direccion,
                Activo = planta.Activo,
            };

            return PartialView("Create", input);
        }

        // POST: Plantas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PlantaInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);

            int empresaId = await GetEmpresaIdAsync();
            var planta = await _context.Plantas.Where(x => x.Id == input.Id && x.EmpresaId == empresaId).FirstOrDefaultAsync();
            if (planta == null)
            {
                return NotFound();
            }

            try
            {
                planta.Codigo = input.Codigo;
                planta.Nombre = input.Nombre;
                planta.Activo = input.Activo;
                planta.Direccion = input.Direccion;
                _context.Update(planta);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantaExists(planta.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Json(new { Id = planta.Id });
        }



        //// GET: Plantas/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    int EmpresaId = await GetEmpresaIdAsync();

        //    var planta = await _context.Plantas
        //        .Include(p => p.Empresa)
        //        .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == EmpresaId);
        //    if (planta == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(planta);
        //}

        //// POST: Plantas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    int EmpresaId = await GetEmpresaIdAsync();
        //    var planta = await _context.Plantas.Where(x => x.Id == id && x.EmpresaId == EmpresaId).FirstOrDefaultAsync();
        //    _context.Plantas.Remove(planta);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool PlantaExists(int id)
        {
            return _context.Plantas.Any(e => e.Id == id);
        }

        public async Task<IActionResult> GetItems(GridParams g)
        {
            int empresaId = await GetEmpresaIdAsync();
            var list = _context.Plantas
                .Where(o => o.EmpresaId == empresaId)
            .AsQueryable();

            return Json(new GridModelBuilder<Planta>(list, g)
            {
                KeyProp = o => o.Id,// needed for Entity Framework | nesting | tree | api
                GetItem = () => _context.Plantas.Find(int.Parse(g.Key)),
                Map = o => MapToGridModel(o)
            }.Build());
        }

        private static object MapToGridModel(Planta o)
        {
            return
                new
                {
                    o.Id,
                    o.Codigo,
                    o.Nombre,
                    o.Direccion,
                    Activo = o.Activo ? "X" : "",
                };
        }

        public async Task<IActionResult> Delete(int id)
        {
            int empresaId = await GetEmpresaIdAsync();

            var planta = await _context.Plantas
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId);
            if (planta == null)
            {
                return NotFound();
            }

            //return PartialView(planta);

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                Type = "planta",
                Name = planta.CodigoYNombre
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteConfirmInput input)
        {
            int empresaId = await GetEmpresaIdAsync();
            var planta = await _context.Plantas
                .FirstOrDefaultAsync(m => m.Id == input.Id && m.EmpresaId == empresaId);

            _context.Plantas.Remove(planta);
            await _context.SaveChangesAsync();

            // the delete PopupForm's success function utils.itemDeleted expects an obj with "Id" property
            return Json(new { Id = input.Id });
        }

    }
}
