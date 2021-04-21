using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_version.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/Test")]
    [ApiController]
    public class TestControllerv1 : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()=>Content("Version 1");
    }
}