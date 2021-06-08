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
    public class ArticulosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ArticulosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<int> GetEmpresaIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.EmpresaId;
        }

        private static object MapToGridModel(Articulo x)
        {
            return
                new
                {
                    x.Id,
                    x.Codigo,
                    x.Referencia,
                    x.Descripcion,
                    Activo = x.Activo ? "X" : "",
                    Tipo = x.Tipo.CodigoYDescripcion,
                    Familia = x.Familia.CodigoYDescripcion,
                    Ubicacion = x.Ubicacion.Descripcion,
                    x.Lote,
                    x.Stock,
                };
        }

        public async Task<IActionResult> GetItemsFiltered(GridParams g, string[] forder,
            string codigo, string referencia, string descripcion, int? tipo, int? familia,
            string ubicacion, string lote, string stock)
        {
            forder = forder ?? new string[] { };
            var frow = new Row();

            int empresaId = await GetEmpresaIdAsync();
            var query = _context.Articulos
                .Include(x => x.Tipo)
                .Include(x => x.Familia)
                .Include(x => x.Ubicacion)
                .Where(x => x.EmpresaId == empresaId)
                .OrderBy(x => x.Codigo)
                .AsQueryable();

            if (codigo != null)
            {
                query = query.Where(x => x.Codigo.Contains(codigo));
            }

            if (referencia != null)
            {
                query = query.Where(x => x.Referencia.Contains(referencia));
            }

            if (descripcion != null)
            {
                query = query.Where(x => x.Descripcion.Contains(descripcion));
            }

            if (lote != null)
            {
                query = query.Where(x => x.Lote.Contains(lote));
            }

            if (stock != null && int.TryParse(stock, out int sstock))
            {
                query = query.Where(x => x.Stock == sstock);
            }

            if (tipo != null)
            {
                query = query.Where(x => x.ArticuloTipoId == tipo);
            }

            if (familia != null)
            {
                query = query.Where(x => x.ArticuloFamiliaId == familia);
            }

            if (ubicacion != null)
            {
                query = query.Where(x => x.Ubicacion.Descripcion.Contains(ubicacion));
            }

            frow.Tipo = await _context.ArticuloTipos
                .Select(x => new KeyContent(x.Id, x.CodigoYDescripcion))
                .ToArrayAsync();

            frow.Familia = await _context.ArticuloFamilias
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.CodigoYDescripcion))
                .ToArrayAsync();

            return Json(new GridModelBuilder<Articulo>(query, g)
            {
                KeyProp = x => x.Id,// needed for Entity Framework | nesting | tree | api
                GetItem = () => _context.Articulos
                    .Include(x => x.Tipo)
                    .Include(x => x.Familia)
                    .Include(x => x.Ubicacion)
                    .First(x => x.Id == int.Parse(g.Key)),
                Map = x => MapToGridModel(x),
                Tag = new { frow = frow }
            }.Build());
        }

        class Row
        {
            public KeyContent[] Tipo { get; set; }
            public KeyContent[] Familia { get; set; }
            public KeyContent[] Ubicacion { get; set; }
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
        public async Task<IActionResult> Create(Articulo input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            input.EmpresaId = await GetEmpresaIdAsync();
            _context.Add(input);
            await _context.SaveChangesAsync();

            // the create PopupForm's success function utils.itemCreated expects the grid row model obj, to render and append the new row
            // Recargamos la entidad con sus referencias para agregarla al grid directamente
            input = await _context.Articulos
                .Include(x => x.Tipo)
                .Include(x => x.Familia)
                .Include(x => x.Ubicacion)
                .Where(x => x.EmpresaId == input.EmpresaId)
                .Where(x => x.Id == input.Id)
                .FirstOrDefaultAsync();

            return Json(MapToGridModel(input));
        }

        public async Task<IActionResult> Edit(int id)
        {
            int empresaId = await GetEmpresaIdAsync();

            var entidad = await _context.Articulos
                .Include(x => x.Tipo)
                .Include(x => x.Familia)
                .Include(x => x.Ubicacion)
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
        public async Task<IActionResult> Edit(Articulo input)
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

            var entidad = await _context.Articulos
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId); ;
            if (entidad == null)
            {
                return NotFound();
            }

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                Type = "articulo",
                Name = entidad.CodigoYDescripcion
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteConfirmInput input)
        {
            int empresaId = await GetEmpresaIdAsync();
            var entidad = await _context.Articulos
                .FirstOrDefaultAsync(m => m.Id == input.Id && m.EmpresaId == empresaId);
            _context.Remove(entidad);
            await _context.SaveChangesAsync();

            // the delete PopupForm's success function utils.itemDeleted expects an obj with "Id" property
            return Json(new { Id = input.Id });
        }
    }
}
