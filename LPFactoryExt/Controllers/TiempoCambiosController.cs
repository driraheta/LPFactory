using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LPFactory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Omu.AwesomeMvc;
using LPFactory.ViewModels.Input;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace LPFactory.Controllers
{
    [Authorize]
    public class TiempoCambiosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly INotyfService _notyf;

        public TiempoCambiosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, INotyfService notyf)
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

        private static object MapToGridModel(TiempoCambio x)
        {
            return
                new
                {
                    x.Id,
                    Argumento = x.Argumento.Nombre,
                    ValorActual = x.ValorActual.Nombre,
                    ValorSiguiente = x.ValorSiguiente.Nombre,
                    x.Tiempo
                };
        }

        public async Task<IActionResult> GetItemsFiltered(GridParams g, string[] forder,
            int? argumento, int? valoractual, int? valorsiguiente)
        {
            forder = forder ?? new string[] { };
            var frow = new Row();

            int empresaId = await GetEmpresaIdAsync();
            var query = _context.TiemposCambio
                .Include(x => x.Argumento)
                .Include(x => x.ValorActual)
                .Include(x => x.ValorSiguiente)
                .Where(x => x.EmpresaId == empresaId)
                .OrderBy(x => x.Id)
                .AsQueryable();

            // Fitros
            if (argumento != null)
            {
                query = query.Where(x => x.ArgumentoId == argumento);
            }

            if (valoractual != null)
            {
                query = query.Where(x => x.ValorActualId == valoractual);
            }

            if (valorsiguiente != null)
            {
                query = query.Where(x => x.ValorSiguienteId == valorsiguiente);
            }



            // Arrays para completar los selects del grid
            frow.Argumento = await _context.Argumentos
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.Nombre))
                .ToArrayAsync();

            frow.ValorActual = await _context.ArgumentoValores
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.Nombre))
                .ToArrayAsync();

            frow.ValorSiguiente = await _context.ArgumentoValores
                .Where(x => x.EmpresaId == empresaId)
                .Select(x => new KeyContent(x.Id, x.Nombre))
                .ToArrayAsync();

            var enviar = new GridModelBuilder<LPFactory.Data.TiempoCambio>(query, g)
            {
                KeyProp = x => x.Id,// needed for Entity Framework | nesting | tree | api
                GetItem = () => _context.TiemposCambio
                    .Include(x => x.Argumento)
                    .Include(x => x.ValorActual)
                    .Include(x => x.ValorSiguiente)
                    .First(x => x.Id == int.Parse(g.Key)),
                Map = x => MapToGridModel(x),
                Tag = new { frow = frow }
            }.Build();

            return Json(enviar);
        }

        class Row
        {
            public KeyContent[] Argumento { get; set; }

            public KeyContent[] ValorActual { get; set; }

            public KeyContent[] ValorSiguiente { get; set; }
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
        public async Task<IActionResult> Create(TiempoCambio input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            input.EmpresaId = await GetEmpresaIdAsync();
            _context.Add(input);
            await _context.SaveChangesAsync();

            // the create PopupForm's success function utils.itemCreated expects the grid row model obj, to render and append the new row
            input.Argumento = await _context.Argumentos.FindAsync(input.ArgumentoId);
            input.ValorActual = await _context.ArgumentoValores.FindAsync(input.ValorActualId);
            input.ValorSiguiente = await _context.ArgumentoValores.FindAsync(input.ValorSiguienteId);
            return Json(MapToGridModel(input));
        }

        public async Task<IActionResult> Edit(int id)
        {
            int empresaId = await GetEmpresaIdAsync();

            var entidad = await _context.TiemposCambio
                .Include(x => x.Argumento)
                .Include(x => x.ValorActual)
                .Include(x => x.ValorSiguiente)
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
        public async Task<IActionResult> Edit(TiempoCambio input)
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

            var entidad = await _context.TiemposCambio
                .FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == empresaId); ;
            if (entidad == null)
            {
                return NotFound();
            }

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                Type = "Tiempo cambio",
                Name = entidad.Id.ToString()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteConfirmInput input)
        {
            int empresaId = await GetEmpresaIdAsync();
            var entidad = await _context.TiemposCambio
                .FirstOrDefaultAsync(m => m.Id == input.Id && m.EmpresaId == empresaId);
            _context.Remove(entidad);
            await _context.SaveChangesAsync();

            // the delete PopupForm's success function utils.itemDeleted expects an obj with "Id" property
            return Json(new { Id = input.Id });
        }

        private bool TiempoCambioExists(int id)
        {
            return _context.TiemposCambio.Any(e => e.Id == id);
        }

        public IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubirFichero(List<IFormFile> files)
        {
            var file = files.FirstOrDefault();
            if (file == null)
            {
                _notyf.Error("No se seleccionó ningún fichero.");
                return View();
            }

            int empresaId = await GetEmpresaIdAsync();

            try
            {
                //FileInfo existingFile = new FileInfo(FilePath);
                using (ExcelPackage package = new ExcelPackage(file.OpenReadStream()))
                {
                    //get the first worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[3];
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count

                    var listaTiempos = await _context.TiemposCambio.Where(x => x.EmpresaId == empresaId).ToListAsync();
                    Random random = new Random();

                    for (int row = 2; row <= rowCount; row++)
                    {
                        string nArgumento = worksheet.Cells[row, 2].Text.Trim();
                        string nActual = worksheet.Cells[row, 3].Text.Trim();
                        string nSiguiente = worksheet.Cells[row, 4].Text.Trim();
                        string nTiempo = worksheet.Cells[row, 5].Text.Trim();

                        int tiempo = 0;

                        if (string.IsNullOrEmpty(nTiempo))
                        {
                            // TODO para desarrollo
                            //tiempo = 3;
                            tiempo = random.Next(120, 3600);
                        }
                        else
                        {
                            tiempo = int.Parse(nTiempo);
                        }

                        if (nActual == nSiguiente) continue;

                        var argumento = await _context.Argumentos.FirstOrDefaultAsync(x => x.Nombre == nArgumento && x.EmpresaId == empresaId);
                        if (argumento == null)
                        {
                            argumento = new Argumento
                            {
                                EmpresaId = empresaId,
                                Nombre = nArgumento
                            };
                            _context.Argumentos.Add(argumento);
                            await _context.SaveChangesAsync();
                        }
                        var vActual = await _context.ArgumentoValores.FirstOrDefaultAsync(x => x.Nombre == nActual && x.EmpresaId == empresaId);
                        if (vActual == null)
                        {
                            vActual = new ArgumentoValor
                            {
                                EmpresaId = empresaId,
                                Nombre = nActual,
                                ArgumentoId = argumento.Id
                            };
                            _context.ArgumentoValores.Add(vActual);
                            await _context.SaveChangesAsync();
                        }
                        var vSiguiente = await _context.ArgumentoValores.FirstOrDefaultAsync(x => x.Nombre == nSiguiente && x.EmpresaId == empresaId);
                        if (vSiguiente == null)
                        {
                            vSiguiente = new ArgumentoValor
                            {
                                EmpresaId = empresaId,
                                Nombre = nSiguiente,
                                ArgumentoId = argumento.Id
                            };
                            _context.ArgumentoValores.Add(vSiguiente);
                            await _context.SaveChangesAsync();
                        }

                        //var tcquery = from c in _context.TiemposCambio
                        //              where c.EmpresaId == empresaId
                        //              where c.ArgumentoId == argumento.Id
                        //              where c.ValorActualId == vActual.Id
                        //              where c.ValorSiguienteId == vSiguiente.Id
                        //              select c;
                        //var tiempoCambios = await tcquery.ToListAsync();
                        //var tiempoCambio = tiempoCambios.FirstOrDefault();

                        var tcquery = from c in listaTiempos
                                      where c.EmpresaId == empresaId
                                      where c.ArgumentoId == argumento.Id
                                      where c.ValorActualId == vActual.Id
                                      where c.ValorSiguienteId == vSiguiente.Id
                                      select c;
                        var tiempoCambios = tcquery.ToList();
                        var tiempoCambio = tiempoCambios.FirstOrDefault();
                        if (tiempoCambio == null)
                        {
                            tiempoCambio = new TiempoCambio
                            {
                                EmpresaId = empresaId,
                                ArgumentoId = argumento.Id,
                                ValorActualId = vActual.Id,
                                ValorSiguienteId = vSiguiente.Id,
                                Tiempo = tiempo
                            };
                            _context.TiemposCambio.Add(tiempoCambio);
                        }
                        else
                        {
                            if (tiempoCambio.Tiempo == tiempo) continue;

                            tiempoCambio.Tiempo = tiempo;
                            _context.Entry(tiempoCambio).State = EntityState.Modified;
                        }
                        await _context.SaveChangesAsync();



                        //for (int col = 1; col <= colCount; col++)
                        //{
                        //    Console.WriteLine(" Row:" + row + " column:" + col + " Value:" + worksheet.Cells[row, col].Value.ToString().Trim());
                        //    string valorCelda = worksheet.Cells[row, col].Value.ToString().Trim();


                        //}
                    }
                }

                _notyf.Success("OK");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyf.Error(ex.Message);
            }

            return View();

        }


    }
}
