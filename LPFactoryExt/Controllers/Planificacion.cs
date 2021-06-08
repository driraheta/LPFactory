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
    public class Planificacion : Controller
    {
        // GET: Planificacion
        public ActionResult Index()
        {
            return View();
        }

        // GET: Planificacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Planificacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Planificacion/Create
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

        // GET: Planificacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Planificacion/Edit/5
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

        // GET: Planificacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Planificacion/Delete/5
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
