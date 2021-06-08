using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LPFactory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Omu.AwesomeMvc;
using LPFactory.ViewModels.Input;

namespace LPFactory.Controllers
{
    [Authorize]
    public class IncidenciasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public IncidenciasController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<int> GetEmpresaIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.EmpresaId;
        }

        private static object MapToGridModel(Incidencia x)
        {
            return
                new
                {
                    x.Id,
                    Orden = x.Orden.CodigoYDescripcion,
                    Seccion = x.Seccion.CodigoYDescripcion,
                    Fase = x.Fase.CodigoYDescripcion,
                    Operacion = x.Operacion.CodigoYDescripcion,
                    Maquina = x.Maquina.CodigoYDescripcion,
                    x.Secuencia,
                    x.NumeroLinea,
                    Articulo = x.Articulo.CodigoYDescripcion,
                    IncidenciaTipo = x.IncidenciaTipo.Descripcion,
                    x.Tiempo,
                    x.Observaciones,
                    x.Fecha
                };
        }

        public async Task<IActionResult> GetItemsFiltered(GridParams g, string[] forder,
            int? orden, int? seccion, int? fase, int? operacion, int? maquina, int? secuencia, int? numerolinea,
            int? articulo, int? incidenciatipo, int? tiempo, string observaciones, DateTime? fecha)
        {
            forder = forder ?? new string[] { };
            var frow = new Row();

            int empresaId = await GetEmpresaIdAsync();
            var query = _context.Incidencias
                .Include(x => x.Orden)
                .Include(x => x.Seccion)
                .Include(x => x.Fase)
                .Include(x => x.Operacion)
                .Include(x => x.Maquina)
                .Include(x => x.Articulo)
                .Include(x => x.IncidenciaTipo)
                .Where(x => x.EmpresaId == empresaId)
                .OrderBy(x => x.Id)
                .AsQueryable();

            // Fitros
            if (orden != null)
            {
                query = query.Where(x => x.OrdenId == orden);
            }

            if (seccion != null)
            {
                query = query.Where(x => x.SeccionId == seccion);
            }

            if (fase != null)
            {
                query = query.Where(x => x.FaseId == fase);
            }

            if (operacion != null)
            {
                query = query.Where(x => x.OperacionId == operacion);
            }

            if (maquina != null)
            {
                query = query.Where(x => x.MaquinaId == maquina);
            }

            if (secuencia != null)
            {
                query = query.Where(x => x.Secuencia == secuencia);
            }

            if (numerolinea != null)
            {
                query = query.Where(x => x.NumeroLinea == numerolinea);
            }

            if (articulo != null)
            {
                query = query.Where(x => x.ArticuloId == articulo);
            }

            if (incidenciatipo != null)
            {
                query = query.Where(x => x.IncidenciaTipoId == incidenciatipo);
            }

            if (tiempo != null)
            {
                query = query.Where(x => x.Tiempo == tiempo);
            }

            if (observaciones != null)
            {
                query = query.Where(x => x.Observaciones.Contains(observaciones));
            }

            if (fecha != null)
            {
                query = query.Where(x => x.Fecha.Equals(fecha));
            }

            // Arrays para completar los selects del grid
            frow.Seccion = await _context.Secciones
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.CodigoYDescripcion))
                .ToArrayAsync();

            frow.Orden = await _context.Ordenes
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.CodigoYDescripcion))
                .ToArrayAsync();

            frow.Maquina = await _context.Maquinas
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.CodigoYDescripcion))
                .ToArrayAsync();

            frow.Fase = await _context.Fases
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.Descripcion))
                .ToArrayAsync();

            frow.Operacion = await _context.Operaciones
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.CodigoYDescripcion))
                .ToArrayAsync();

            frow.Articulo = await _context.Articulos
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.CodigoYDescripcion))
                .ToArrayAsync();

            frow.IncidenciaTipo = await _context.IncidenciaTipos
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.Descripcion))
                .ToArrayAsync();

            var enviar = new GridModelBuilder<LPFactory.Data.Incidencia>(query, g)
            {
                KeyProp = x => x.Id,// needed for Entity Framework | nesting | tree | api
                GetItem = () => _context.Incidencias
                    .Include(x => x.Seccion)
                    .Include(x => x.Orden)
                    .Include(x => x.Maquina)
                    .Include(x => x.Fase)
                    .Include(x => x.Operacion)
                    .Include(x => x.Articulo)
                    .Include(x => x.IncidenciaTipo)
                    .First(x => x.Id == int.Parse(g.Key)),
                Map = x => MapToGridModel(x),
                Tag = new { frow = frow }
            }.Build();

            return Json(enviar);
        }

        class Row
        {
            public KeyContent[] Seccion { get; set; }

            public KeyContent[] Orden { get; set; }

            public KeyContent[] Maquina { get; set; }
            public KeyContent[] Fase { get; set; }
            public KeyContent[] Operacion { get; set; }

            public KeyContent[] Articulo { get; set; }

            public KeyContent[] IncidenciaTipo { get; set; }
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
        public async Task<IActionResult> Create(Incidencia input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            input.EmpresaId = await GetEmpresaIdAsync();
            _context.Add(input);
            await _context.SaveChangesAsync();

            // the create PopupForm's success function utils.itemCreated expects the grid row model obj, to render and append the new row
            input.Seccion = await _context.Secciones.FindAsync(input.SeccionId);
            input.Orden = await _context.Ordenes.FindAsync(input.Orden);
            input.Maquina = await _context.Maquinas.FindAsync(input.MaquinaId);
            input.Fase = await _context.Fases.FindAsync(input.Fase);
            input.Operacion = await _context.Operaciones.FindAsync(input.Operacion);
            input.Articulo = await _context.Articulos.FindAsync(input.Articulo);
            input.IncidenciaTipo = await _context.IncidenciaTipos.FindAsync(input.IncidenciaTipo);
            return Json(MapToGridModel(input));
        }

        public async Task<IActionResult> Edit(int id)
        {
            int empresaId = await GetEmpresaIdAsync();

            var entidad = await _context.Incidencias
                .Include(x => x.Seccion)
                    .Include(x => x.Orden)
                    .Include(x => x.Maquina)
                    .Include(x => x.Fase)
                    .Include(x => x.Operacion)
                    .Include(x => x.Articulo)
                    .Include(x => x.IncidenciaTipo)
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
        public async Task<IActionResult> Edit(Incidencia input)
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

            var entidad = await _context.Incidencias
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId); ;
            if (entidad == null)
            {
                return NotFound();
            }

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                Type = "incidencia",
                Name = entidad.Id.ToString()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteConfirmInput input)
        {
            int empresaId = await GetEmpresaIdAsync();
            var entidad = await _context.Incidencias
                .FirstOrDefaultAsync(m => m.Id == input.Id && m.EmpresaId == empresaId);
            _context.Remove(entidad);
            await _context.SaveChangesAsync();

            // the delete PopupForm's success function utils.itemDeleted expects an obj with "Id" property
            return Json(new { Id = input.Id });
        }
    }
}
