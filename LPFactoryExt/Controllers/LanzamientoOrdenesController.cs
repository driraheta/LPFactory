using AspNetCoreHero.ToastNotification.Abstractions;
using LPFactory.Data;
using LPFactory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Omu.AwesomeMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Controllers
{
    [Authorize]
    public class LanzamientoOrdenesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly INotyfService _notyf;

        public LanzamientoOrdenesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, INotyfService notyf)
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

        // GET: LanzamientoOrdenes
        public async Task<IActionResult> Index()
        {
            int empresaId = await GetEmpresaIdAsync();

            //var queryArticulos = from a in _context.Articulos
            //                     where a.EmpresaId == empresaId
            //                     select new { a.CodigoYDescripcion, a.Id };
            //var articulos = queryArticulos.ToList();

            //string json = JsonConvert.SerializeObject(articulos);

            //ViewData["articulos"] = json;

            ViewData["Id"] = new SelectList(await _context.Estructuras.Include(x => x.Articulo).Where(x => x.EmpresaId == empresaId).ToListAsync(), "Id", "Descripcion");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Resumen(IFormCollection collection)
        {
            int empresaId = await GetEmpresaIdAsync();
            var estructura = await _context.Estructuras
                .Include(x => x.Articulo)
                .Where(x => x.Id == int.Parse(collection["Id"]))
                .Where(x => x.EmpresaId == empresaId).FirstAsync();

            ViewData["estructuraId"] = collection["Id"];
            ViewData["descripcion"] = estructura.Descripcion;
            ViewData["cantidad"] = collection["Cantidad"];
            return View();
        }

        public async Task<IActionResult> GetResumen(GridParams g, int estructuraId, int cantidad)
        {
            int empresaId = await GetEmpresaIdAsync();
            var fases = await _context.EstructurasFases
                .Include(x => x.Fase)
                .Include(x => x.Estructura)
                .Where(x => x.EmpresaId == empresaId)
                .Where(x => x.EstructuraId == estructuraId)
                .OrderBy(x => x.Fase.Codigo)
                .Select(x => new TreeNode { 
                    Id = x.Id,
                    FaseId = x.FaseId,
                    Titulo = "Fase " + x.Fase.CodigoYDescripcion ,
                    TPrep = MuestraTiempoDesdeSegundos(x.Operaciones.Sum(x => x.TiempoPreparacion) * cantidad / x.Estructura.Cantidad),
                    TProd = MuestraTiempoDesdeSegundos(x.Operaciones.Sum(x => x.TiempoProduccion) * cantidad / x.Estructura.Cantidad),
                    TTotal = MuestraTiempoDesdeSegundos(x.Operaciones.Sum(x => x.TiempoPreparacion + x.TiempoProduccion) * cantidad / x.Estructura.Cantidad),
                    TPrepSegundos = (x.Operaciones.Sum(x => x.TiempoPreparacion) * cantidad / x.Estructura.Cantidad),
                    TProdSegundos = (x.Operaciones.Sum(x => x.TiempoProduccion) * cantidad / x.Estructura.Cantidad),
                })
                .ToListAsync();

            var total = new TreeNode { Titulo = "TOTAL" };
            foreach (var f in fases)
            {
                total.TPrepSegundos += f.TPrepSegundos;
                total.TProdSegundos += f.TProdSegundos;
                total.TTotalSegundos += f.TPrepSegundos + f.TProdSegundos;
            }
            total.TPrep = MuestraTiempoDesdeSegundos(total.TPrepSegundos);
            total.TProd = MuestraTiempoDesdeSegundos(total.TProdSegundos);
            total.TTotal = MuestraTiempoDesdeSegundos(total.TTotalSegundos);
            fases.Add(total);

            var builder = new GridModelBuilder<TreeNode>(fases.AsQueryable(), g)
            {
                Key = "Id", //required for the TreeGrid,
                DefaultKeySort = Sort.None,
                //GetChildren = (node, nodeLevel) => Db.TreeNodes.Where(o => o.Parent == node).AsQueryable(),
                GetChildren = (node, nodeLevel) => {

                    var children = new List<TreeNode>();

                    if (node.Titulo.StartsWith("Fase"))
                    {
                        children.Add(new TreeNode { Id = node.Id, Titulo = "Materiales" });
                        children.Add(new TreeNode { Id = node.Id, Titulo = "Operaciones" });
                    }
                    else if (node.Titulo.StartsWith("Materiales"))
                    {
                        children.AddRange(_context.EstructurasFasesMateriales
                            .Include(x => x.Articulo)
                            .Include(x => x.EstructuraFase)
                            .ThenInclude(x => x.Estructura)
                            .Where(x => x.EstructuraFaseId == node.Id)
                            .Select(x => new TreeNode {
                                Titulo = string.Format("{0} x ({1}) {2}",
                                    (x.Cantidad * cantidad / x.EstructuraFase.Estructura.Cantidad).ToString().PadRight(6),
                                    x.Articulo.Codigo,
                                    x.Articulo.Descripcion
                                )
                            })
                        );
                    }
                    else if (node.Titulo.StartsWith("Operaciones"))
                    {
                        children.AddRange(_context.EstructuraFaseOperaciones
                            .Include(x => x.Operacion)
                            .Include(x => x.EstructuraFase)
                            .ThenInclude(x => x.Estructura)
                            .Where(x => x.EstructuraFaseId == node.Id)
                            .Select(x => new TreeNode {
                                Titulo = x.Operacion.CodigoYDescripcion,
                                TPrep = MuestraTiempoDesdeSegundos(x.TiempoPreparacion * cantidad / x.EstructuraFase.Estructura.Cantidad),
                                TProd = MuestraTiempoDesdeSegundos(x.TiempoProduccion * cantidad / x.EstructuraFase.Estructura.Cantidad),
                                TTotal = MuestraTiempoDesdeSegundos((x.TiempoPreparacion + x.TiempoProduccion) * cantidad / x.EstructuraFase.Estructura.Cantidad),
                            })
                        );
                    }

                    return children.AsQueryable();
                },
                Map = o => new { o.Titulo, o.Cantidad, o.TPrep, o.TProd, o.TTotal },
                MakeHeader = gh => new GroupHeader { Item = gh.NodeItem, Collapsed = true }
            };

            return Json(builder.Build());
        }

        class TreeNode
        {
            public int Id { get; set; }
            public int FaseId { get; set; }
            public string Titulo { get; set; }
            public string Cantidad { get; set; }
            public string TPrep { get; set; }
            public string TProd { get; set; }
            public string TTotal { get; set; }
            public int TPrepSegundos { get; set; }
            public int TProdSegundos { get; set; }
            public int TTotalSegundos { get; set; }

        }

        static string MuestraTiempoDesdeSegundos(int segundos)
        {
            int horas = segundos / 3600;
            int minutos = (segundos % 3600) / 60;
            return string.Format("{0:D2}:{1:D2}:{2:D2}", horas, minutos, segundos % 60);
        }

    }
}
