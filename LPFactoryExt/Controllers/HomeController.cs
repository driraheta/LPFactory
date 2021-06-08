using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LPFactory.Models;
using LPFactory.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LPFactory.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        private async Task<int> GetEmpresaIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.EmpresaId;
        }

        public async Task<IActionResult> Index()
        {
            int EmpresaId = await GetEmpresaIdAsync();
            var empresa = await _context.Empresas.FindAsync(EmpresaId);

            ViewData["NombreEmpresa"] = empresa.Nombre;
            ViewData["DireccionEmpresa"] = empresa.Direccion;
            ViewData["NombreERP"] = empresa.NombreERP;

            // Carga de logo de la empresa
            string logoImageBase64 = "";
            var recurso = await _context.Recursos
                .FirstOrDefaultAsync(x => x.EmpresaId == EmpresaId
                    && x.Tipo == TipoRecurso.Logo
                    && x.Id == empresa.LogoId);
            if (recurso != null)
            {
                string extension = System.IO.Path.GetExtension(recurso.Nombre).Replace(".", "");
                logoImageBase64 = Convert.ToBase64String(recurso.Binario);
                logoImageBase64 = string.Format("data:image/{0};base64,{1}", extension, logoImageBase64);
            }

            ViewData["logoImageBase64"] = logoImageBase64;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
