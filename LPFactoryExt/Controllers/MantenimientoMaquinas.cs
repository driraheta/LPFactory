using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Controllers
{
    [Authorize]
    public class MantenimientoMaquinas : Controller
    {
        // GET: MantenimientoMaquinas
        public ActionResult Index()
        {
            return View();
        }

        // GET: MantenimientoMaquinas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MantenimientoMaquinas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MantenimientoMaquinas/Create
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

        // GET: MantenimientoMaquinas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MantenimientoMaquinas/Edit/5
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

        // GET: MantenimientoMaquinas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MantenimientoMaquinas/Delete/5
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
