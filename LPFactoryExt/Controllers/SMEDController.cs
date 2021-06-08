using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Controllers
{
    public class SMEDController : Controller
    {
        // GET: SMEDController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SMEDController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SMEDController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SMEDController/Create
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

        // GET: SMEDController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SMEDController/Edit/5
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

        // GET: SMEDController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SMEDController/Delete/5
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
