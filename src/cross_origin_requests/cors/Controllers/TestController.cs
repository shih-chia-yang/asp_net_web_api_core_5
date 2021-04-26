using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cors.Controllers
{
    [EnableCors(Startup.cors_middleware)]    
    [Produces("application/json")]
    [Route("api/Test")]
    [ApiController]
    public class TestController : ControllerBase
    {

        
        [Route("List")]
        [HttpGet]
        public string GetTestResult()
        {
            return "Test api result";
        }

        [DisableCors]
        // [EnableCors("AnotherPolicy")]
        [Route("GetResult/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            return Content($" Test Id: {id}");
        }

        // [EnableCors("AnotherPolicy")]
        [Route("{id}")]
        [HttpPost]
        public IActionResult AddId(int id){
            return Content($" Test Post Id: {id}");
        }
    }
}