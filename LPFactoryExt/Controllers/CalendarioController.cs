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
    public class CalendarioController : Controller
    {
        // GET: CalendarioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CalendarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CalendarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CalendarioController/Create
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

        // GET: CalendarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CalendarioController/Edit/5
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

        // GET: CalendarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CalendarioController/Delete/5
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
