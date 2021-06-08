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
    public class StocksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public StocksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<int> GetEmpresaIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.EmpresaId;
        }

        private static object MapToGridModel(Stock x)
        {
            return
                new
                {
                    x.Id,
                    Almacen = x.Almacen.CodigoYDescripcion,
                    x.CodigoAlmacen,
                    x.DescripcionAlmacen,
                    Ubicacion = x.Ubicacion.Id,
                    x.DescripcionUbicacion,
                    x.X,
                    x.Y,
                    x.Z,
                    Articulo = x.Articulo.CodigoYDescripcion,
                    x.CodigoArticulo,
                    x.DescripcionArticulo,
                    x.Lote,
                    x.Referencia,
                    x.Cantidad
                };
        }

        public async Task<IActionResult> GetItemsFiltered(GridParams g, string[] forder,
            int? almacen, string codigoalmacen, string descripcionalmacen, int? ubicacion, string descripcionubicacion,
            string x, string y, string z, int? articulo, string descripcionarticulo, string codigoarticuo, string lote,
            string referencia, int? cantidad)
        {
            forder = forder ?? new string[] { };
            var frow = new Row();

            int empresaId = await GetEmpresaIdAsync();
            var query = _context.Stocks
                .Include(x => x.Almacen)
                .Include(x => x.Ubicacion)
                .Include(x => x.Articulo)
                .Where(x => x.EmpresaId == empresaId)
                .OrderBy(x => x.Id)
                .AsQueryable();

            // Fitros
            if (codigoalmacen != null)
            {
                query = query.Where(x => x.CodigoAlmacen.Contains(codigoalmacen));
            }

            if (descripcionalmacen != null)
            {
                query = query.Where(x => x.DescripcionAlmacen.Contains(descripcionalmacen));
            }

            if (descripcionubicacion != null)
            {
                query = query.Where(x => x.DescripcionUbicacion.Contains(descripcionubicacion));
            }

            if (x != null)
            {
                query = query.Where(d => d.X.Contains(x));
            }

            if (y != null)
            {
                query = query.Where(x => x.Y.Contains(y));
            }

            if (z != null)
            {
                query = query.Where(x => x.Z.Contains(z));
            }

            if (descripcionarticulo != null)
            {
                query = query.Where(x => x.DescripcionArticulo.Contains(descripcionarticulo));
            }

            if (codigoarticuo != null)
            {
                query = query.Where(x => x.CodigoArticulo.Contains(codigoarticuo));
            }

            if (lote != null)
            {
                query = query.Where(x => x.CodigoArticulo.Contains(lote));
            }

            if (referencia != null)
            {
                query = query.Where(x => x.CodigoArticulo.Contains(referencia));
            }

            if (almacen != null)
            {
                query = query.Where(x => x.AlmacenId == almacen);
            }

            if (ubicacion != null)
            {
                query = query.Where(x => x.UbicacionId == ubicacion);
            }

            if (articulo != null)
            {
                query = query.Where(x => x.ArticuloId == articulo);
            }

            if (cantidad != null)
            {
                query = query.Where(x => x.Cantidad == cantidad);
            }

            // Arrays para completar los selects del grid
            frow.Almacen = await _context.Almacenes
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.CodigoYDescripcion))
                .ToArrayAsync();

            frow.Ubicacion = await _context.Ubicaciones
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.Id.ToString()))
                .ToArrayAsync();

            frow.Articulo = await _context.Articulos
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.CodigoYDescripcion))
                .ToArrayAsync();

            var enviar = new GridModelBuilder<LPFactory.Data.Stock>(query, g)
            {
                KeyProp = x => x.Id,// needed for Entity Framework | nesting | tree | api
                GetItem = () => _context.Stocks
                    .Include(x => x.Almacen)
                    .Include(x => x.Ubicacion)
                    .Include(x => x.Articulo)
                    .First(x => x.Id == int.Parse(g.Key)),
                Map = x => MapToGridModel(x),
                Tag = new { frow = frow }
            }.Build();

            return Json(enviar);
        }

        class Row
        {
            public KeyContent[] Almacen { get; set; }

            public KeyContent[] Ubicacion { get; set; }

            public KeyContent[] Articulo { get; set; }
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
        public async Task<IActionResult> Create(Stock input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            input.EmpresaId = await GetEmpresaIdAsync();
            _context.Add(input);
            await _context.SaveChangesAsync();

            // the create PopupForm's success function utils.itemCreated expects the grid row model obj, to render and append the new row
            input.Almacen = await _context.Almacenes.FindAsync(input.AlmacenId);
            input.Ubicacion = await _context.Ubicaciones.FindAsync(input.UbicacionId);
            input.Articulo = await _context.Articulos.FindAsync(input.ArticuloId);
            return Json(MapToGridModel(input));
        }

        public async Task<IActionResult> Edit(int id)
        {
            int empresaId = await GetEmpresaIdAsync();

            var entidad = await _context.Stocks
                .Include(x => x.Almacen)
                .Include(x => x.Ubicacion)
                .Include(x => x.Articulo)
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
        public async Task<IActionResult> Edit(Stock input)
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

            var entidad = await _context.Stocks
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId); ;
            if (entidad == null)
            {
                return NotFound();
            }

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                Type = "stock",
                Name = entidad.Id.ToString()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteConfirmInput input)
        {
            int empresaId = await GetEmpresaIdAsync();
            var entidad = await _context.Stocks
                .FirstOrDefaultAsync(m => m.Id == input.Id && m.EmpresaId == empresaId);
            _context.Remove(entidad);
            await _context.SaveChangesAsync();

            // the delete PopupForm's success function utils.itemDeleted expects an obj with "Id" property
            return Json(new { Id = input.Id });
        }
    }
}
