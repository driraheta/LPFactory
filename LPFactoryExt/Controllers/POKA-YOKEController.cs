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
    public class POKA_YOKEController : Controller
    {
        // GET: POKA_YOKEController
        public ActionResult Index()
        {
            return View();
        }

        // GET: POKA_YOKEController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: POKA_YOKEController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: POKA_YOKEController/Create
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

        // GET: POKA_YOKEController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: POKA_YOKEController/Edit/5
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

        // GET: POKA_YOKEController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: POKA_YOKEController/Delete/5
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
