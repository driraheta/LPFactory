using LPFactory.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Omu.AwesomeMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Controllers
{
    [Authorize]
    public class DataController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public DataController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<int> GetEmpresaIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.EmpresaId;
        }

        public async Task<IActionResult> GetAllPlantas()
        {
            int empresaId = await GetEmpresaIdAsync();
            var list = await _context.Plantas
                .Where(o => o.EmpresaId == empresaId)
                .Select(o => new KeyContent(o.Id, o.CodigoYNombre))
                .ToListAsync();
            return Json(list);
        }

        public async Task<IActionResult> GetAllSecciones()
        {
            int empresaId = await GetEmpresaIdAsync();
            var list = await _context.Secciones
                .Where(o => o.EmpresaId == empresaId)
                .Select(o => new KeyContent(o.Id, o.CodigoYDescripcion))
                .ToListAsync();
            return Json(list);
        }

        public async Task<IActionResult> GetAllFamiliasArticulos()
        {
            int empresaId = await GetEmpresaIdAsync();
            var list = await _context.ArticuloFamilias
                .Where(o => o.EmpresaId == empresaId)
                .Select(o => new KeyContent(o.Id, o.CodigoYDescripcion))
                .ToListAsync();
            return Json(list);
        }

        public async Task<IActionResult> GetAllTipoArticulos()
        {
            int empresaId = await GetEmpresaIdAsync();
            var list = await _context.ArticuloTipos
                .Select(o => new KeyContent(o.Id, o.CodigoYDescripcion))
                .ToListAsync();
            return Json(list);
        }

        public async Task<IActionResult> GetAllUbicaciones()
        {
            int empresaId = await GetEmpresaIdAsync();
            var list = await _context.Ubicaciones
                .Where(o => o.EmpresaId == empresaId)
                .Select(o => new KeyContent(o.Id, o.Descripcion))
                .ToListAsync();
            return Json(list);
        }

        public async Task<IActionResult> GetAllLineas()
        {
            int empresaId = await GetEmpresaIdAsync();
            var list = await _context.Lineas
                .Where(o => o.EmpresaId == empresaId)
                .Select(o => new KeyContent(o.Id, o.CodigoYNombre))
                .ToListAsync();
            return Json(list);
        }

        public async Task<IActionResult> GetAllMaquinas()
        {
            int empresaId = await GetEmpresaIdAsync();
            var list = await _context.Maquinas
                .Where(o => o.EmpresaId == empresaId)
                .Select(o => new KeyContent(o.Id, o.CodigoYDescripcion))
                .ToListAsync();
            return Json(list);
        }

        public async Task<IActionResult> GetAllAlmacenes()
        {
            int empresaId = await GetEmpresaIdAsync();
            var list = await _context.Almacenes
                .Where(o => o.EmpresaId == empresaId)
                .Select(o => new KeyContent(o.Id, o.CodigoYDescripcion))
                .ToListAsync();
            return Json(list);
        }

        public async Task<IActionResult> GetAllOrdenes()
        {
            int empresaId = await GetEmpresaIdAsync();
            var list = await _context.Ordenes
                .Where(o => o.EmpresaId == empresaId)
                .Select(o => new KeyContent(o.Id, o.CodigoYDescripcion))
                .ToListAsync();
            return Json(list);
        }

        public async Task<IActionResult> GetAllFases()
        {
            int empresaId = await GetEmpresaIdAsync();
            var list = await _context.Fases
                .Where(o => o.EmpresaId == empresaId)
                .Select(o => new KeyContent(o.Id, o.CodigoYDescripcion))
                .ToListAsync();
            return Json(list);
        }

        public async Task<IActionResult> GetAllOperaciones()
        {
            int empresaId = await GetEmpresaIdAsync();
            var list = await _context.Operaciones
                .Where(o => o.EmpresaId == empresaId)
                .Select(o => new KeyContent(o.Id, o.CodigoYDescripcion))
                .ToListAsync();
            return Json(list);
        }

        public async Task<IActionResult> GetAllArticulos()
        {
            int empresaId = await GetEmpresaIdAsync();
            var list = await _context.Articulos
                .Where(o => o.EmpresaId == empresaId)
                .Select(o => new KeyContent(o.Id, o.CodigoYDescripcion))
                .ToListAsync();
            return Json(list);
        }

        public async Task<IActionResult> GetAllIncidenciaTipo()
        {
            int empresaId = await GetEmpresaIdAsync();
            var list = await _context.IncidenciaTipos
                .Where(o => o.EmpresaId == empresaId)
                .Select(o => new KeyContent(o.Id, o.Descripcion))
                .ToListAsync();
            return Json(list);
        }

        public async Task<IActionResult> GetAllArgumentos()
        {
            int empresaId = await GetEmpresaIdAsync();
            var list = await _context.Argumentos
                .Where(o => o.EmpresaId == empresaId)
                .Select(o => new KeyContent(o.Id, o.Nombre))
                .ToListAsync();
            return Json(list);
        }

        public async Task<IActionResult> GetAllArgumentoValor()
        {
            int empresaId = await GetEmpresaIdAsync();
            var list = await _context.ArgumentoValores
                .Where(o => o.EmpresaId == empresaId)
                .Select(o => new KeyContent(o.Id, o.Nombre))
                .ToListAsync();
            return Json(list);
        }

        public async Task<IActionResult> GetAllOperarios()
        {
            int empresaId = await GetEmpresaIdAsync();
            var list = await _context.Operarios
                .Where(o => o.EmpresaId == empresaId)
                .Select(o => new KeyContent(o.Id, o.Nombre))
                .ToListAsync();
            return Json(list);
        }

        public async Task<IActionResult> GetAllOrdenEstado()
        {
            Dictionary<int, string> myDic = 
                ((Models.OrdenEstado[])Enum.GetValues(typeof(Models.OrdenEstado))).ToDictionary(v => (int)v, k => k.ToString());
            var list = myDic.ToList();            
            return Json(list);
        }

    }
}
