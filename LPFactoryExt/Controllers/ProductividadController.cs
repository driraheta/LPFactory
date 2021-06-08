using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LPFactory.Data;
using LPFactory.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace LPFactory.Controllers
{
    public class ProductividadController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductividadController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductividadController
        public async Task<ActionResult> Index()
        {
            var vm = new ProductividadViewModel
            {
                LineaDesde = 1,
                LineaHasta = 100,
                FechaDesde = DateTime.Today.AddMonths(-1),
                FechaHasta = DateTime.Today,
                FechaDesdeAnterior = DateTime.Today.AddMonths(-2),
                FechaHastaAnterior = DateTime.Today.AddMonths(-1).AddDays(-1),
                
            };
            
            return View(await EjecutaConsulta(vm));
        }

        // POST: ProductividadController/Buscar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buscar(ProductividadViewModel vm)
        {
            vm = await EjecutaConsulta(vm);

            return View("Index", vm);
        }

        private async Task<ProductividadViewModel> EjecutaConsulta(ProductividadViewModel vm)
        {
            try
            {
                vm.Produccion = new LineaResultado();
                vm.Mermas = new LineaResultado();
                vm.TiempoPreparacion = new LineaResultado();
                vm.TiempoProduccion = new LineaResultado();
                vm.NumeroIncidencias = new LineaResultado();
                vm.TiempoIncidencias = new LineaResultado();
                vm.NumeroMicroparadas = new LineaResultado();
                vm.TiempoMicroparadas = new LineaResultado();
                vm.Disponibilidad = new LineaResultado();
                vm.Rendimiento = new LineaResultado();
                vm.Calidad = new LineaResultado();
                vm.OEE = new LineaResultado();
                vm.LineasDatos = new List<LineaDatos>();
                vm.LineasDatosCompleta = new List<LineaDatosCompleta>();

                var query = from o in _context.Ordenes
                            from e in _context.OrdenOperaciones
                            from s in _context.Secciones
                            from f in _context.Fases
                            from op in _context.Operaciones
                            from m in _context.Maquinas
                            from a in _context.Articulos
                            from t in _context.Operarios
                            where e.OrdenId == o.Id
                            where e.SeccionId == s.Id
                            where e.FaseId == f.Id
                            where e.OperacionId == op.Id
                            where e.MaquinaId == m.Id
                            where e.ArticuloId == a.Id
                            where e.OperarioId == t.Id
                            // Esta comparación no se puede hacer en bd, hay que traerse los datos
                            // y hacerlo en local.
                            //where (vm.SeccionDesde == 0) ||
                            //    (vm.SeccionDesde > 0 && int.Parse(s.Codigo) >= vm.SeccionDesde)
                            select new { o, e, s, f, op, a, m, t };

                var ordenesPeriodoActual = await query
                    .Where(x => x.o.Fecha >= vm.FechaDesde.Date && x.o.Fecha.Date < vm.FechaHasta.AddDays(1).Date)
                    .ToListAsync();

                var ordenesPeriodoAnterior = await query.Where(
                    x => x.o.Fecha >= vm.FechaDesdeAnterior.Date && x.o.Fecha.Date < vm.FechaHastaAnterior.AddDays(1).Date)
                    .ToListAsync();

                int TiempoPreparacionAcumulado = 0;
                int TiempoProduccionAcumulado = 0;
                int CantidadAcumulada = 0;


                foreach (var x in ordenesPeriodoActual)
                {
                    // Filtros
                    int a = 0;
                    if (vm.SeccionDesde > 0 && (int.TryParse(x.s.Codigo, out a) ? a : int.MaxValue) < vm.SeccionDesde) continue;
                    if (vm.SeccionHasta > 0 && (int.TryParse(x.s.Codigo, out a) ? a : int.MinValue) > vm.SeccionHasta) continue;
                    if (vm.MaquinaDesde > 0 && (int.TryParse(x.m.Codigo, out a) ? a : int.MaxValue) < vm.MaquinaDesde) continue;
                    if (vm.MaquinaHasta > 0 && (int.TryParse(x.m.Codigo, out a) ? a : int.MinValue) > vm.MaquinaHasta) continue;
                    if (vm.OperarioIdDesde > 0 && (int.TryParse(x.t.Codigo, out a) ? a : int.MaxValue) < vm.OperarioIdDesde) continue;
                    if (vm.OperarioIdHasta > 0 && (int.TryParse(x.t.Codigo, out a) ? a : int.MinValue) > vm.OperarioIdHasta) continue;
                    if (vm.FaseDesde > 0 && (int.TryParse(x.f.Codigo, out a) ? a : int.MaxValue) < vm.FaseDesde) continue;
                    if (vm.FaseHasta > 0 && (int.TryParse(x.f.Codigo, out a) ? a : int.MinValue) > vm.FaseHasta) continue;
                    if (vm.OrdenDesde > 0 && (int.TryParse(x.o.Codigo, out a) ? a : int.MaxValue) < vm.OrdenDesde) continue;
                    if (vm.OrdenHasta > 0 && (int.TryParse(x.o.Codigo, out a) ? a : int.MinValue) > vm.OrdenHasta) continue;
                    if (vm.ArticuloId > 0 && (int.TryParse(x.a.Codigo, out a) ? a : 0) != vm.ArticuloId) continue;
                    

                    vm.Produccion.Teorico += x.e.Cantidad;
                    vm.Produccion.Real +=  x.e.CantidadProducida;
                    vm.Mermas.Teorico +=  x.e.MermasTeoricas;
                    vm.Mermas.Real +=  x.e.MermasReales;
                    vm.TiempoPreparacion.Teorico +=  x.e.TiempoPreparacion;
                    vm.TiempoPreparacion.Real +=  x.e.TiempoPreparacionReal;
                    vm.TiempoProduccion.Teorico +=  x.e.TiempoProduccion;
                    vm.TiempoProduccion.Real +=  x.e.TiempoProduccionReal;
                    vm.NumeroIncidencias.Teorico +=  x.e.NumeroIncidencias;
                    vm.TiempoIncidencias.Teorico +=  x.e.TiempoIncidencias;

                    vm.LineasDatos.Add(new LineaDatos
                    {
                        Orden = x.o.Codigo,
                        Articulo = x.a.Codigo,
                        Descripcion = x.e.Descripcion,
                        Cantidad =  x.e.Cantidad,
                        Producido =  x.e.CantidadProducida,
                        TiempoPreparacionTeorico =  x.e.TiempoPreparacion,
                        TiempoPreparacionReal =  x.e.TiempoPreparacionReal,
                        TiempoProduccionTeorico =  x.e.TiempoProduccion,
                        TiempoProduccionReal =  x.e.TiempoProduccionReal,
                        Mermas =  x.e.MermasReales
                    });

                    var queryIncidencias = from i in _context.Incidencias
                                           where i.OrdenId == x.o.Id
                                           select i;
                    var incidencias = await queryIncidencias.ToListAsync();
                    foreach (var i in incidencias)
                    {
                        vm.NumeroIncidencias.Real += 1;
                        vm.TiempoIncidencias.Real += i.Tiempo;
                    }

                    TiempoPreparacionAcumulado += x.e.TiempoProduccionReal;
                    TiempoProduccionAcumulado += x.e.TiempoProduccionReal;
                    CantidadAcumulada += x.e.CantidadProducida;

                    vm.LineasDatosCompleta.Add(new LineaDatosCompleta
                    {
                        Id = x.o.Id,
                        Orden = x.o.Codigo,
                        Articulo = x.a.Codigo,
                        Descripcion = x.e.Descripcion,
                        NumeroLote = "",
                        Operario = x.t.CodigoYDescripcion,
                        Seccion = x.s.CodigoYDescripcion,
                        Fase = x.f.CodigoYDescripcion,
                        Operacion = x.op.CodigoYDescripcion,
                        Maquina = x.m.CodigoYDescripcion,
                        Secuencia = x.e.Secuencia,
                        TiempoPreparacion = x.e.TiempoPreparacionReal,
                        TiempoProduccion = x.e.TiempoProduccionReal,
                        Cantidad = x.e.CantidadProducida,
                        MermasTeoricas = x.e.MermasTeoricas,
                        FechaInicio = x.e.FechaInicio?.ToString("dd/MM/yyyy HH:mm:ss"),
                        TiempoPreparacionAcumulado = TiempoPreparacionAcumulado,
                        TiempoProduccionAcumulado = TiempoProduccionAcumulado,
                        CantidadAcumulada = CantidadAcumulada,
                        Abierta = x.e.Orden.Estado != OrdenEstado.Cerrada,
                        Cerrada = x.e.Orden.Estado == OrdenEstado.Cerrada,
                        Mermas = x.e.MermasReales,
                        NumeroIncidencias = incidencias.Count,
                        //TiempoIncidencias = 


                    });

                }

                foreach (var x in ordenesPeriodoAnterior)
                {
                    vm.Produccion.RealAnt += x.e.CantidadProducida;
                    vm.Mermas.RealAnt += x.e.MermasReales;
                    vm.TiempoPreparacion.RealAnt += x.e.TiempoPreparacionReal;
                    vm.TiempoProduccion.RealAnt += x.e.TiempoProduccionReal;
                }

                if (vm.TiempoPreparacion.Teorico + vm.TiempoProduccion.Teorico > 0)
                    vm.Disponibilidad.Teorico = (vm.TiempoProduccion.Teorico /
                    (vm.TiempoProduccion.Teorico + vm.TiempoPreparacion.Teorico));

                if (vm.TiempoProduccion.Real + vm.TiempoPreparacion.Real +
                    vm.TiempoIncidencias.Real + vm.TiempoMicroparadas.Real > 0)
                {
                    vm.Disponibilidad.Real = vm.TiempoProduccion.Real / (
                        vm.TiempoProduccion.Real + vm.TiempoPreparacion.Real +
                        vm.TiempoIncidencias.Real + vm.TiempoMicroparadas.Real
                    );
                }

                vm.Rendimiento.Teorico = (vm.TiempoProduccion.Teorico > 0) ?
                    vm.Produccion.Teorico / vm.TiempoProduccion.Teorico : 0;

                vm.Rendimiento.Real = (vm.TiempoProduccion.Real > 0) ?
                    vm.Produccion.Real / vm.TiempoProduccion.Real : 0;

                vm.Calidad.Teorico = (vm.Produccion.Teorico + vm.Mermas.Teorico > 0) ?
                    vm.TiempoProduccion.Teorico / (vm.Produccion.Teorico + vm.Mermas.Teorico) : 0;

                vm.Calidad.Real = (vm.Produccion.Real + vm.Mermas.Real > 0) ?
                    vm.Produccion.Real / (vm.Produccion.Real + vm.Mermas.Real) : 0;

                decimal disponibilidad = vm.Disponibilidad.Real;

                decimal rendimiento = (vm.Rendimiento.Teorico > 0) ?
                    vm.Rendimiento.Real / vm.Rendimiento.Teorico : 0;

                decimal calidad = (vm.Calidad.Teorico > 0) ?
                    vm.Calidad.Real / vm.Calidad.Teorico : 0;

                vm.OEE.Real = disponibilidad * rendimiento * calidad;


                // Gráfico 1
                vm.DisponibilidadActual = (disponibilidad * 100).ToString("F2", CultureInfo.InvariantCulture);
                vm.DisponibilidadAnterior = (disponibilidad * 80).ToString("F2", CultureInfo.InvariantCulture);
                vm.RendimientoActual = (rendimiento * 100).ToString("F2", CultureInfo.InvariantCulture);
                vm.RendimientoAnterior = (rendimiento * 75).ToString("F2", CultureInfo.InvariantCulture);
                vm.CalidadActual = (calidad * 100).ToString("F2", CultureInfo.InvariantCulture);
                vm.CalidadAnterior = (calidad * 65).ToString("F2", CultureInfo.InvariantCulture);
                vm.OEEActual = (vm.OEE.Real * 100).ToString("F2", CultureInfo.InvariantCulture);
                vm.OEEAnterior = (vm.OEE.Real * 48).ToString("F2", CultureInfo.InvariantCulture);


                // Html selects
                vm.Ordenes = await _context.Ordenes.OrderBy(x => x.Codigo.Length).ThenBy(x => x.Codigo).ToListAsync();
                vm.Secciones = await _context.Secciones.OrderBy(x => x.Codigo.Length).ThenBy(x => x.Codigo).ToListAsync();
                vm.Maquinas = await _context.Maquinas.OrderBy(x => x.Codigo.Length).ThenBy(x => x.Codigo).ToListAsync();
                vm.Operarios = await _context.Operarios.OrderBy(x => x.Codigo.Length).ThenBy(x => x.Codigo).ToListAsync();
                vm.Fases = await _context.Fases.OrderBy(x => x.Codigo.Length).ThenBy(x => x.Codigo).ToListAsync();
                vm.Articulos = await _context.Articulos.OrderBy(x => x.Codigo.Length).ThenBy(x => x.Codigo).ToListAsync();
                vm.ArticuloFamilias = await _context.ArticuloFamilias.OrderBy(x => x.Codigo.Length).ThenBy(x => x.Codigo).ToListAsync();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return vm;
        }

        // GET: ProductividadController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductividadController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductividadController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductividadController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductividadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductividadController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductividadController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
