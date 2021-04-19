using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace empty_core_project.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Home/Index");
        }

        public IActionResult ViewOne()
        {
            return Content("Home/One");
        }

        [HttpGet]
        public IActionResult ViewTwo()
        {
            return Content("Get-Home/Two");
        }

        [HttpPost]
        public IActionResult ViewTwo(string id)
        {
            return Content($"Post-Home/{id}");
        }

    }
}