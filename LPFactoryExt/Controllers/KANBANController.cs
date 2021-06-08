using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Controllers
{
    public class KANBANController : Controller
    {
        // GET: KANBANController
        public ActionResult Index()
        {
            return View();
        }

        // GET: KANBANController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: KANBANController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KANBANController/Create
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

        // GET: KANBANController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: KANBANController/Edit/5
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

        // GET: KANBANController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: KANBANController/Delete/5
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
