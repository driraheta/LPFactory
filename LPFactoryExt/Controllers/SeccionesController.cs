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
    public class SeccionesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public SeccionesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<int> GetEmpresaIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.EmpresaId;
        }

        private static object MapToGridModel(Seccion x)
        {
            return
                new
                {
                    x.Id,
                    x.Codigo,
                    x.Descripcion,
                    x.Actividad,
                    Activo = x.Activo ? "X" : "",
                    Planta = x.Planta.CodigoYNombre
                };
        }

        //public async Task<IActionResult> GetItems(GridParams g)
        //{
        //    int empresaId = await GetEmpresaIdAsync();
        //    var list = _context.Secciones
        //        .Include(x => x.Planta)
        //        .Where(x => x.EmpresaId == empresaId)
        //        .OrderBy(x => x.Codigo)
        //    .AsQueryable();

        //    return Json(new GridModelBuilder<Seccion>(list, g)
        //    {
        //        KeyProp = x => x.Id,// needed for Entity Framework | nesting | tree | api
        //        GetItem = () => _context.Secciones
        //            .Include(x => x.Planta)
        //            .First(x => x.Id == int.Parse(g.Key)),
        //        Map = x => MapToGridModel(x)
        //    }.Build());
        //}

        public async Task<IActionResult> GetItemsFiltered(GridParams g, string[] forder, string descripcion, int? planta)
        {
            forder = forder ?? new string[] { };
            var frow = new SeccionRow();

            int empresaId = await GetEmpresaIdAsync();
            var query = _context.Secciones
                .Include(x => x.Planta)
                .Where(x => x.EmpresaId == empresaId)
                .OrderBy(x => x.Codigo)
                .AsQueryable();

            if (descripcion != null)
            {
                query = query.Where(x => x.Descripcion.Contains(descripcion));
            }

            if (planta != null)
            {
                query = query.Where(x => x.PlantaId == planta);
            }

            frow.Planta = await _context.Plantas
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.CodigoYNombre))
                .ToArrayAsync();

            return Json(new GridModelBuilder<Seccion>(query, g)
            {
                KeyProp = x => x.Id,// needed for Entity Framework | nesting | tree | api
                GetItem = () => _context.Secciones
                    .Include(x => x.Planta)
                    .First(x => x.Id == int.Parse(g.Key)),
                Map = x => MapToGridModel(x),
                Tag = new { frow = frow }
            }.Build());
        }
        
        class SeccionRow
        {
            public KeyContent[] Planta { get; set; }
        }

        // GET: Secciones
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
        public async Task<IActionResult> Create(Seccion input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            input.EmpresaId = await GetEmpresaIdAsync();
            _context.Add(input);
            await _context.SaveChangesAsync();

            // the create PopupForm's success function utils.itemCreated expects the grid row model obj, to render and append the new row
            input.Planta = await _context.Plantas.FindAsync(input.PlantaId);
            return Json(MapToGridModel(input));
        }

        public async Task<IActionResult> Edit(int id)
        {
            int empresaId = await GetEmpresaIdAsync();

            var seccion = await _context.Secciones
                .Include(x => x.Planta)
                .Where(x => x.Id == id && x.EmpresaId == empresaId)
                .FirstOrDefaultAsync();
            if (seccion == null)
            {
                return NotFound();
            }

            return PartialView("Create", seccion);
        }

        // POST: Plantas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Seccion input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);

            try
            {
                input.EmpresaId = await GetEmpresaIdAsync();
                _context.Update(input);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return Json(new { Id = input.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            // No debe existir vista Delete.cshtml

            int empresaId = await GetEmpresaIdAsync();

            var entidad = await _context.Secciones
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId); ;
            if (entidad == null)
            {
                return NotFound();
            }

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                Type = "sección",
                Name = entidad.CodigoYDescripcion
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteConfirmInput input)
        {
            int empresaId = await GetEmpresaIdAsync();
            var entidad = await _context.Secciones
                .FirstOrDefaultAsync(m => m.Id == input.Id && m.EmpresaId == empresaId);

            _context.Secciones.Remove(entidad);
            await _context.SaveChangesAsync();

            // the delete PopupForm's success function utils.itemDeleted expects an obj with "Id" property
            return Json(new { Id = input.Id });
        }





    }
}
