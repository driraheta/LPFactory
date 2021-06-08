using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Controllers
{
    public class Recambios : Controller
    {
        // GET: Recambios
        public ActionResult Index()
        {
            return View();
        }

        // GET: Recambios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Recambios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recambios/Create
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

        // GET: Recambios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Recambios/Edit/5
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

        // GET: Recambios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Recambios/Delete/5
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
