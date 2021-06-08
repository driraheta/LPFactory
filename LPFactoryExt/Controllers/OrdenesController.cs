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
    public class OrdenesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public OrdenesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<int> GetEmpresaIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.EmpresaId;
        }

        private static object MapToGridModel(Orden x)
        {
            return
                new
                {
                    x.Id,
                    x.Codigo,
                    Articulo = x.Articulo.CodigoYDescripcion,
                    x.Descripcion,
                    x.Fecha,
                    x.Prioridad,
                    x.Cantidad,
                    x.MermasTeoricas,
                    x.MermasReales,
                    UbicacionDestino = x.UbicacionDestino.Id,
                    x.Producido,
                    x.Pendiente,
                    x.Estado,
                    x.FechaInicio,
                    x.FechaFin,
                    x.Observaciones,
                    x.Lote
                };
        }

        public async Task<IActionResult> GetItemsFiltered(GridParams g, string[] forder,
            string codigo, int? articulo, string descripcion, string lote, int? estado, DateTime? fecha)
        {
            forder = forder ?? new string[] { };
            var frow = new Row();

            int empresaId = await GetEmpresaIdAsync();
            var query = _context.Ordenes
                .Include(x => x.Articulo)
                .Include(x => x.UbicacionDestino)                
                .Where(x => x.EmpresaId == empresaId)
                .OrderBy(x => x.Codigo)
                .AsQueryable();

            // Fitros
            if (codigo != null)
            {
                query = query.Where(x => x.Codigo.Contains(codigo));
            }

            if (descripcion != null)
            {
                query = query.Where(x => x.Descripcion.Contains(descripcion));
            }

            if (lote != null)
            {
                query = query.Where(x => x.Lote.Contains(lote));
            }

            if (estado != null)
            {
                query = query.Where(x => x.Estado == (Models.OrdenEstado)estado);
            }

            if (fecha != null)
            {
                query = query.Where(x => x.Fecha == fecha);
            }

            // Arrays para completar los selects del grid
            frow.Articulos = await _context.Articulos
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.CodigoYDescripcion))
                .ToArrayAsync();

            frow.Ubicaciones = await _context.Ubicaciones
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.Descripcion))
                .ToArrayAsync();

            var enviar = new GridModelBuilder<LPFactory.Data.Orden>(query, g)
            {
                KeyProp = x => x.Id,// needed for Entity Framework | nesting | tree | api
                GetItem = () => _context.Ordenes
                    .Include(x => x.Articulo)
                .Include(x => x.UbicacionDestino)
                    .First(x => x.Id == int.Parse(g.Key)),
                Map = x => MapToGridModel(x),
                Tag = new { frow = frow }
            }.Build();

            return Json(enviar);
        }

        class Row
        {
            public KeyContent[] Articulos { get; set; }

            public KeyContent[] Ubicaciones { get; set; }
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
        public async Task<IActionResult> Create(Orden input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            input.EmpresaId = await GetEmpresaIdAsync();
            _context.Add(input);
            await _context.SaveChangesAsync();

            // the create PopupForm's success function utils.itemCreated expects the grid row model obj, to render and append the new row
            input.Articulo = await _context.Articulos.FindAsync(input.ArticuloId);
            input.UbicacionDestino = await _context.Ubicaciones.FindAsync(input.UbicacionDestinoId);
            return Json(MapToGridModel(input));
        }

        public async Task<IActionResult> Edit(int id)
        {
            int empresaId = await GetEmpresaIdAsync();

            var entidad = await _context.Ordenes
                .Include(x => x.Articulo)
                .Include(x => x.UbicacionDestino)
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
        public async Task<IActionResult> Edit(Orden input)
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

            var entidad = await _context.Ordenes
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId); ;
            if (entidad == null)
            {
                return NotFound();
            }

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                Type = "Orden",
                Name = entidad.Id.ToString()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteConfirmInput input)
        {
            int empresaId = await GetEmpresaIdAsync();
            var entidad = await _context.Ordenes
                .FirstOrDefaultAsync(m => m.Id == input.Id && m.EmpresaId == empresaId);
            _context.Remove(entidad);
            await _context.SaveChangesAsync();

            // the delete PopupForm's success function utils.itemDeleted expects an obj with "Id" property
            return Json(new { Id = input.Id });
        }
    }
}
