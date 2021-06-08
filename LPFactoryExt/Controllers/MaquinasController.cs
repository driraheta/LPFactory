﻿using System;
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
    public class MaquinasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public MaquinasController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<int> GetEmpresaIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.EmpresaId;
        }

        private static object MapToGridModel(Maquina x)
        {
            return
                new
                {
                    x.Id,
                    x.Codigo,
                    x.Descripcion,
                    x.Modelo,
                    x.NumeroSerie,
                    x.AnioFabricacion,
                    x.Version,
                    x.Actividad,
                    Activo = x.Activo ? "X" : "",
                    Seccion = x.Seccion.CodigoYDescripcion,
                    Linea = x.Linea.CodigoYNombre,
                };
        }

        public async Task<IActionResult> GetItemsFiltered(GridParams g, string[] forder,
            string codigo, string descripcion, string modelo, string numeroserie,
            string aniofabricacion, string version, int? seccion, int? linea)
        {
            forder = forder ?? new string[] { };
            var frow = new Row();

            int empresaId = await GetEmpresaIdAsync();
            var query = _context.Maquinas
                .Include(x => x.Seccion)
                .Include(x => x.Linea)
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

            if (modelo != null)
            {
                query = query.Where(x => x.Modelo.Contains(modelo));
            }

            if (numeroserie != null)
            {
                query = query.Where(x => x.NumeroSerie.Contains(numeroserie));
            }

            if (aniofabricacion != null && int.TryParse(aniofabricacion, out int svalor))
            {
                query = query.Where(x => x.AnioFabricacion == svalor);
            }

            if (seccion != null)
            {
                query = query.Where(x => x.SeccionId == seccion);
            }

            if (linea != null)
            {
                query = query.Where(x => x.LineaId == linea);
            }

            // Arrays para completar los selects del grid
            frow.Seccion = await _context.Secciones
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.CodigoYDescripcion))
                .ToArrayAsync();

            frow.Linea = await _context.Lineas
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.CodigoYNombre))
                .ToArrayAsync();

            return Json(new GridModelBuilder<Maquina>(query, g)
            {
                KeyProp = x => x.Id,// needed for Entity Framework | nesting | tree | api
                GetItem = () => _context.Maquinas
                    .Include(x => x.Seccion)
                    .Include(x => x.Linea)
                    .First(x => x.Id == int.Parse(g.Key)),
                Map = x => MapToGridModel(x),
                Tag = new { frow = frow }
            }.Build());
        }

        class Row
        {
            public KeyContent[] Seccion { get; set; }

            public KeyContent[] Linea { get; set; }
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
        public async Task<IActionResult> Create(Maquina input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            input.EmpresaId = await GetEmpresaIdAsync();
            _context.Add(input);
            await _context.SaveChangesAsync();

            // the create PopupForm's success function utils.itemCreated expects the grid row model obj, to render and append the new row
            input.Seccion = await _context.Secciones.FindAsync(input.SeccionId);
            input.Linea = await _context.Lineas.FindAsync(input.LineaId);
            return Json(MapToGridModel(input));
        }

        public async Task<IActionResult> Edit(int id)
        {
            int empresaId = await GetEmpresaIdAsync();

            var entidad = await _context.Maquinas
                .Include(x => x.Seccion)
                .Include(x => x.Linea)
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
        public async Task<IActionResult> Edit(Maquina input)
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

            var entidad = await _context.Maquinas
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId); ;
            if (entidad == null)
            {
                return NotFound();
            }

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                Type = "máquina",
                Name = entidad.CodigoYDescripcion
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteConfirmInput input)
        {
            int empresaId = await GetEmpresaIdAsync();
            var entidad = await _context.Maquinas
                .FirstOrDefaultAsync(m => m.Id == input.Id && m.EmpresaId == empresaId);
            _context.Remove(entidad);
            await _context.SaveChangesAsync();

            // the delete PopupForm's success function utils.itemDeleted expects an obj with "Id" property
            return Json(new { Id = input.Id });
        }
    }
}
