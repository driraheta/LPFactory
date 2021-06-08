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
using Newtonsoft.Json;
using LPFactory.Models;
using System.Text;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace LPFactory.Controllers
{
    [Authorize]
    public class OrdenOperacionesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly INotyfService _notyf;

        public OrdenOperacionesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, INotyfService notyf)
        {
            _context = context;
            _userManager = userManager;
            _notyf = notyf;
        }

        private async Task<int> GetEmpresaIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.EmpresaId;
        }

        private static object MapToGridModel(OrdenOperacion x)
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
                    Articulo = x.Articulo.CodigoYDescripcion,
                    x.Descripcion,
                    x.TiempoProduccion,
                    x.TiempoPreparacion,
                    x.Cantidad,
                    x.MermasTeoricas,
                    Operario = x.Operario.CodigoYDescripcion,
                    x.TiempoPreparacionReal,
                    x.TiempoProduccionReal,
                    x.CantidadProducida,
                    x.MermasReales,
                    x.TiempoIncidencias,
                    x.NumeroIncidencias,
                    x.TiempoMicroparadas,
                    x.NumeroMicroparadas,
                    x.FechaInicio,
                    x.FechaFin,
                    x.Estado,
                    x.Observaciones,
                    x.CodigoOrden,
                    x.CodigoArticulo
                };
        }

        public async Task<IActionResult> GetItemsFiltered(GridParams g, string[] forder,
            int? orden, int? seccion, int? fase, int? operacion, int? maquina, int? articulo, string descripcion,
            int? operario, DateTime? fechainicio, DateTime? fechafin, int? estado)
        {
            forder = forder ?? new string[] { };
            var frow = new Row();

            int empresaId = await GetEmpresaIdAsync();
            var query = _context.OrdenOperaciones
                .Include(x => x.Orden)
                .Include(x => x.Seccion)
                .Include(x => x.Fase)
                .Include(x => x.Operacion)
                .Include(x => x.Maquina)
                .Include(x => x.Articulo)
                .Include(x => x.Operario)
                .Where(x => x.EmpresaId == empresaId)
                .OrderBy(x => x.Id)
                .AsQueryable();

            // Fitros
            if (orden != null)
            {
                query = query.Where(x => x.Orden.Id == orden);
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
                query = query.Where(x => x.OperarioId == fase);
            }

            if (maquina != null)
            {
                query = query.Where(x => x.MaquinaId == maquina);
            }

            if (articulo != null)
            {
                query = query.Where(x => x.ArticuloId == articulo);
            }

            if (descripcion != null)
            {
                query = query.Where(x => x.Descripcion.Contains(descripcion));
            }

            if (operario != null)
            {
                query = query.Where(x => x.OperarioId == operario);
            }

            if (fechainicio != null)
            {
                query = query.Where(x => x.FechaInicio == fechainicio);
            }

            if (fechafin != null)
            {
                query = query.Where(x => x.FechaFin == fechafin);
            }

            if (estado != null)
            {
                query = query.Where(x => x.Estado == (Models.OrdenEstado)estado);
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

            frow.Fase = await _context.Fases
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.CodigoYDescripcion))
                .ToArrayAsync();

            frow.Operacion = await _context.Operaciones
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.CodigoYDescripcion))
                .ToArrayAsync();

            frow.Maquina = await _context.Maquinas
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.CodigoYDescripcion))
                .ToArrayAsync();

            frow.Articulo = await _context.Articulos
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.CodigoYDescripcion))
                .ToArrayAsync();

            frow.Operario = await _context.Operarios
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.Nombre))
                .ToArrayAsync();

            var enviar = new GridModelBuilder<LPFactory.Data.OrdenOperacion>(query, g)
            {
                KeyProp = x => x.Id,// needed for Entity Framework | nesting | tree | api
                GetItem = () => _context.OrdenOperaciones
                    .Include(x => x.Orden)
                .Include(x => x.Seccion)
                .Include(x => x.Fase)
                .Include(x => x.Operacion)
                .Include(x => x.Maquina)
                .Include(x => x.Articulo)
                .Include(x => x.Operario)
                    .First(x => x.Id == int.Parse(g.Key)),
                Map = x => MapToGridModel(x),
                Tag = new { frow = frow }
            }.Build();

            return Json(enviar);
        }

        class Row
        {
            public KeyContent[] Seccion { get; set; }
            public KeyContent[] Fase { get; set; }
            public KeyContent[] Maquina { get; set; }
            public KeyContent[] Operacion { get; set; }
            public KeyContent[] Articulo { get; set; }
            public KeyContent[] Operario { get; set; }
            public KeyContent[] Orden { get; set; }
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
        public async Task<IActionResult> Create(OrdenOperacion input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            input.EmpresaId = await GetEmpresaIdAsync();
            _context.Add(input);
            await _context.SaveChangesAsync();

            // the create PopupForm's success function utils.itemCreated expects the grid row model obj, to render and append the new row
            input.Seccion = await _context.Secciones.FindAsync(input.SeccionId);
            input.Fase = await _context.Fases.FindAsync(input.FaseId);
            input.Maquina = await _context.Maquinas.FindAsync(input.MaquinaId);
            input.Operacion = await _context.Operaciones.FindAsync(input.OperacionId);
            input.Articulo = await _context.Articulos.FindAsync(input.ArticuloId);
            input.Operario = await _context.Operarios.FindAsync(input.OperarioId);
            input.Orden = await _context.Ordenes.FindAsync(input.OrdenId);
            return Json(MapToGridModel(input));
        }

        public async Task<IActionResult> Edit(int id)
        {
            int empresaId = await GetEmpresaIdAsync();

            var entidad = await _context.OrdenOperaciones
                .Include(x => x.Orden)
                .Include(x => x.Seccion)
                .Include(x => x.Fase)
                .Include(x => x.Operacion)
                .Include(x => x.Maquina)
                .Include(x => x.Articulo)
                .Include(x => x.Operario)
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
        public async Task<IActionResult> Edit(Puesto input)
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

            var entidad = await _context.OrdenOperaciones
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId); ;
            if (entidad == null)
            {
                return NotFound();
            }

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                Type = "Orden Operaciones",
                Name = entidad.Id.ToString()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteConfirmInput input)
        {
            int empresaId = await GetEmpresaIdAsync();
            var entidad = await _context.OrdenOperaciones
                .FirstOrDefaultAsync(m => m.Id == input.Id && m.EmpresaId == empresaId);
            _context.Remove(entidad);
            await _context.SaveChangesAsync();

            // the delete PopupForm's success function utils.itemDeleted expects an obj with "Id" property
            return Json(new { Id = input.Id });
        }

        #region subirArchivo
        private bool OrdenEscandalloExists(int id)
        {
            return _context.OrdenOperaciones.Any(e => e.Id == id);
        }

        public IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> ModificarLineas(string jsonInput = "")
        {
            int empresaId = await GetEmpresaIdAsync();

            var data = new { estado = "", lista = new List<int>() };
            var dato = JsonConvert.DeserializeAnonymousType(jsonInput, data);

            if (dato.estado == "borrar")
            {
                _context.OrdenOperaciones.RemoveRange(
                    _context.OrdenOperaciones.Where(x => x.EmpresaId == empresaId && dato.lista.Contains(x.Id)));
            }
            else
            {
                OrdenEstado nuevoEstado = (OrdenEstado)Enum.Parse(typeof(OrdenEstado), dato.estado);
                var oos = await _context.OrdenOperaciones
                    .Where(x => x.EmpresaId == empresaId && dato.lista.Contains(x.Id))
                    .ToListAsync();
                foreach (var oo in oos)
                {
                    oo.Estado = nuevoEstado;
                    _context.Entry(oo).State = EntityState.Modified;
                }
            }
            await _context.SaveChangesAsync();

            string respuesta = string.Format("{0} {1} {2}",
                dato.lista.Count,
                (dato.lista.Count == 1) ? "línea" : "líneas",
                (dato.estado == "borrar") ? "borradas" : "modificadas");

            return respuesta;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubirFichero(List<IFormFile> files, ImportacionFicheroOperacionesViewModel importacionFicheroOperacionesViewModel)
        {
            var file = files.FirstOrDefault();
            if (file == null)
            {
                _notyf.Error("No se seleccionó ningún fichero.");
                return View();
            }

            int empresaId = await GetEmpresaIdAsync();
            var errores = new StringBuilder();

            try
            {
                //FileInfo existingFile = new FileInfo(FilePath);
                using (ExcelPackage package = new ExcelPackage(file.OpenReadStream()))
                {
                    //get the first worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count

                    var listaTiempos = await _context.TiemposCambio
                        .Include(x => x.Argumento).Include(x => x.ValorActual).Include(x => x.ValorSiguiente)
                        .Where(x => x.EmpresaId == empresaId).ToListAsync();
                    var articulos = await _context.Articulos.Where(x => x.EmpresaId == empresaId).ToListAsync();

                    // Actualización de artículos
                    if (importacionFicheroOperacionesViewModel.AltaArticulos ||
                        importacionFicheroOperacionesViewModel.ActualizarArticulosExistentes)
                    {
                        for (int row = 2; row <= rowCount; row++)
                        {
                            string nArticulo = worksheet.Cells[row, 3].Text.Trim();
                            string nReferencia = worksheet.Cells[row, 4].Text.Trim();
                            string nDescripcion = worksheet.Cells[row, 5].Text.Trim();
                            string nEan = worksheet.Cells[row, 6].Text.Trim();
                            string nStock = worksheet.Cells[row, 7].Text.Trim();

                            var articulo = articulos.FirstOrDefault(x => x.Codigo == nArticulo);
                            if (articulo == null)
                            {
                                if (importacionFicheroOperacionesViewModel.AltaArticulos)
                                    articulo = new Articulo();
                                else
                                    continue;
                            }
                            else
                            {
                                if (!importacionFicheroOperacionesViewModel.ActualizarArticulosExistentes)
                                    continue;
                            }
                            articulo.EmpresaId = empresaId;
                            articulo.ArticuloTipoId = 2;
                            articulo.UbicacionId = 1;
                            articulo.ArticuloFamiliaId = 1;
                            articulo.Activo = true;

                            articulo.Codigo = nArticulo;
                            articulo.Referencia = nReferencia;
                            articulo.Descripcion = nDescripcion;
                            articulo.CodigoBarras = nEan;
                            articulo.Stock = (int.TryParse(nStock, out int stock)) ? stock : 0;

                            if (articulo.Id == 0)
                                _context.Articulos.Add(articulo);
                            else
                                _context.Entry(articulo).State = EntityState.Modified;

                        }
                        await _context.SaveChangesAsync();
                    }


                    // Creación de operaciones
                    if (importacionFicheroOperacionesViewModel.GenerarOrdenes)
                    {
                        var argumentos = await _context.Argumentos.Include(x => x.Valores).Where(x => x.EmpresaId == empresaId).ToListAsync();
                        var qordenesPendientes = from o in _context.Ordenes
                                                 where o.EmpresaId == empresaId
                                                 where o.Estado == OrdenEstado.Pendiente
                                                 select o;
                        var ordenesPendientes = await qordenesPendientes.ToListAsync();

                        for (int row = 2; row <= rowCount; row++)
                        {
                            string nLog52 = worksheet.Cells[row, 1].Text.Trim();
                            string nSecuencia = worksheet.Cells[row, 2].Text.Trim();
                            string nArticulo = worksheet.Cells[row, 3].Text.Trim();
                            string nReferencia = worksheet.Cells[row, 4].Text.Trim();
                            string nDescripcion = worksheet.Cells[row, 5].Text.Trim();
                            string nEan = worksheet.Cells[row, 6].Text.Trim();
                            string nStock = worksheet.Cells[row, 7].Text.Trim();
                            string nCantidad = worksheet.Cells[row, 8].Text.Trim();
                            string nVelocidad = worksheet.Cells[row, 9].Text.Trim();
                            string nTiempo = worksheet.Cells[row, 10].Text.Trim();
                            string nTamaño = worksheet.Cells[row, 11].Text.Trim();
                            string nTapa = worksheet.Cells[row, 12].Text.Trim();
                            string nHojas = worksheet.Cells[row, 13].Text.Trim();
                            string nGramaje = worksheet.Cells[row, 14].Text.Trim();
                            string nRayado = worksheet.Cells[row, 15].Text.Trim();
                            string nBandasDeColor = worksheet.Cells[row, 16].Text.Trim();
                            string nMicroperforado = worksheet.Cells[row, 17].Text.Trim();
                            string nTaladros = worksheet.Cells[row, 18].Text.Trim();
                            string nPuntasRedondas = worksheet.Cells[row, 19].Text.Trim();
                            string nHusillo = worksheet.Cells[row, 20].Text.Trim();
                            string nColorAlambre = worksheet.Cells[row, 21].Text.Trim();
                            string nPlanta = worksheet.Cells[row, 22].Text.Trim();
                            string nSeccion = worksheet.Cells[row, 23].Text.Trim();
                            string nLinea = worksheet.Cells[row, 24].Text.Trim();
                            string nMaquina = worksheet.Cells[row, 25].Text.Trim();
                            string nPuesto = worksheet.Cells[row, 26].Text.Trim();
                            string nFase = worksheet.Cells[row, 27].Text.Trim();
                            string nEstado = worksheet.Cells[row, 28].Text.Trim();

                            var articulo = articulos.First(x => x.Codigo == nArticulo);
                            if (!importacionFicheroOperacionesViewModel.DuplicarOrdenesExistentes)
                            {
                                var ordenPendiente = ordenesPendientes.Where(o => o.ArticuloId == articulo.Id).FirstOrDefault();
                                if (ordenPendiente != null) continue;
                            }

                            var orden = new Orden
                            {
                                Codigo = nLog52,
                                ArticuloId = articulo.Id,
                                Descripcion = nDescripcion,
                                Fecha = DateTime.Now,
                                Prioridad = 0,
                                Cantidad = (int.TryParse(nCantidad, out int x)) ? x : 0,
                                Estado = importacionFicheroOperacionesViewModel.LanzarOrdenesGeneradas ? OrdenEstado.Lanzada : OrdenEstado.Pendiente,
                                EmpresaId = empresaId
                            };
                            _context.Ordenes.Add(orden);

                            var operacion = new OrdenOperacion
                            {
                                Orden = orden,
                                SeccionId = 1,
                                FaseId = 1,
                                OperacionId = 1,
                                MaquinaId = 1,
                                Secuencia = int.Parse(nSecuencia),
                                ArticuloId = articulo.Id,
                                Descripcion = articulo.Descripcion
                            };


                            // Cálculo tiempo de preparacion
                            int tiempoPreparacion = 0;
                            if (row > 2)
                            {
                                for (int i = 11; i <= 21; i++)
                                {
                                    string tArgumento = worksheet.Cells[1, i].Text.Trim();
                                    string valorActual = worksheet.Cells[row, i].Text.Trim();
                                    string valorAnterior = worksheet.Cells[row - 1, i].Text.Trim();

                                    if (valorActual == valorAnterior) continue;

                                    var tc = from t in listaTiempos
                                             where t.Argumento.Nombre == tArgumento
                                             where t.ValorActual.Nombre == valorActual
                                             where t.ValorSiguiente.Nombre == valorAnterior
                                             select t;
                                    var tcc = tc.FirstOrDefault();
                                    if (tcc == null)
                                        errores.AppendLine($"Argumento {tArgumento} no encontrado.");
                                    else
                                        tiempoPreparacion += tcc.Tiempo;
                                }
                            }


                            operacion.TiempoPreparacion = tiempoPreparacion;
                            _context.OrdenOperaciones.Add(operacion);
                        }

                        await _context.SaveChangesAsync();
                    }


                }

                if (errores.Length > 0)
                    _notyf.Information(errores.ToString());
                else
                    _notyf.Success("OK");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyf.Error(ex.Message);
            }

            return View();

        }
        #endregion
    }
}
