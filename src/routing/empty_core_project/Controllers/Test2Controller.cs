using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace empty_core_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test2Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult GetListProduct()
        {
            return Content("test2 product list");
        }

        [HttpGet("{id}",Name="Get Product by id")]
        public IActionResult GetProduct(string id)
        {
            return Content($"Product id: {id}");
        }

        [HttpGet("int/{id:int}")]
        public IActionResult GetIntProduct(int id)
        {
            return Content($"Product int id : {id}");
        }

    }
}