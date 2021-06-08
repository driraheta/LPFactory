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
    public class TPMController : Controller
    {
        // GET: TPMController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TPMController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TPMController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TPMController/Create
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

        // GET: TPMController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TPMController/Edit/5
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

        // GET: TPMController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TPMController/Delete/5
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
