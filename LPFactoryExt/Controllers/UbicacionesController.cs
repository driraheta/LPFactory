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
    public class UbicacionesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public UbicacionesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<int> GetEmpresaIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.EmpresaId;
        }

        private static object MapToGridModel(Ubicacion x)
        {
            return
                new
                {
                    x.Id,
                    x.Descripcion,
                    x.X,
                    x.Y,
                    x.Z,
                    Almacen = x.Almacen.CodigoYDescripcion                    
                };
        }

        public async Task<IActionResult> GetItemsFiltered(GridParams g, string[] forder,
            string descripcion, string x, string y, string z, int? almacen)
        {
            forder = forder ?? new string[] { };
            var frow = new Row();

            int empresaId = await GetEmpresaIdAsync();
            var query = _context.Ubicaciones
                .Include(x => x.Almacen)
                .Where(x => x.EmpresaId == empresaId)
                .OrderBy(x => x.Id)
                .AsQueryable();

            // Fitros
            if (descripcion != null)
            {
                query = query.Where(x => x.Descripcion.Contains(descripcion));
            }

            if (x != null)
            {
                query = query.Where(j => j.X.Contains(x));
            }

            if (y != null)
            {
                query = query.Where(x => x.Y == y);
            }

            if (z != null)
            {
                query = query.Where(x => x.Z == z);
            }

            if (almacen != null)
            {
                query = query.Where(x => x.AlmacenId == almacen);
            }

            // Arrays para completar los selects del grid
            frow.Almacen = await _context.Almacenes
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.CodigoYDescripcion))
                .ToArrayAsync();

            var enviar = new GridModelBuilder<LPFactory.Data.Ubicacion>(query, g)
            {
                KeyProp = x => x.Id,// needed for Entity Framework | nesting | tree | api
                GetItem = () => _context.Ubicaciones
                    .Include(x => x.Almacen)
                    .First(x => x.Id == int.Parse(g.Key)),
                Map = x => MapToGridModel(x),
                Tag = new { frow = frow }
            }.Build();

            return Json(enviar);
        }

        class Row
        {
            public KeyContent[] Almacen { get; set; }
        }

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
        public async Task<IActionResult> Create(Ubicacion input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            input.EmpresaId = await GetEmpresaIdAsync();
            _context.Add(input);
            await _context.SaveChangesAsync();

            // the create PopupForm's success function utils.itemCreated expects the grid row model obj, to render and append the new row
            input.Almacen = await _context.Almacenes.FindAsync(input.AlmacenId);
            return Json(MapToGridModel(input));
        }

        public async Task<IActionResult> Edit(int id)
        {
            int empresaId = await GetEmpresaIdAsync();

            var entidad = await _context.Ubicaciones
                .Include(x => x.Almacen)
                .Where(x => x.Id == id && x.EmpresaId == empresaId)
                .FirstOrDefaultAsync();
            if (entidad == null)
            {
                return NotFound();
            }

            return PartialView("Create", entidad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Ubicacion input)
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
            // No debe existir vista Delete.cshtml en la carpeta vistas de este 
            // controlador.

            int empresaId = await GetEmpresaIdAsync();

            var entidad = await _context.Ubicaciones
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId); ;
            if (entidad == null)
            {
                return NotFound();
            }

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                Type = "ubicacion",
                Name = entidad.Descripcion
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteConfirmInput input)
        {
            int empresaId = await GetEmpresaIdAsync();
            var entidad = await _context.Ubicaciones
                .FirstOrDefaultAsync(m => m.Id == input.Id && m.EmpresaId == empresaId);
            _context.Remove(entidad);
            await _context.SaveChangesAsync();

            // the delete PopupForm's success function utils.itemDeleted expects an obj with "Id" property
            return Json(new { Id = input.Id });
        }
    }
}
