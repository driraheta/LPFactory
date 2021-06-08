using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Controllers
{
    public class ANDONController : Controller
    {
        // GET: ANDONController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ANDONController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ANDONController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ANDONController/Create
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

        // GET: ANDONController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ANDONController/Edit/5
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

        // GET: ANDONController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ANDONController/Delete/5
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
