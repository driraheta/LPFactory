using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Controllers
{
    public class BasicoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
