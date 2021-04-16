using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using empty_core_project.Models;

namespace empty_core_project.Controllers
{

    public class HiController:Controller
    {
        public ActionResult Index()
        {
            var viewModel = new Hi();
            return View(viewModel);
        }

        [HTTPPost]
        public IActionResult test()
        {
            return View();
        }
    }
}