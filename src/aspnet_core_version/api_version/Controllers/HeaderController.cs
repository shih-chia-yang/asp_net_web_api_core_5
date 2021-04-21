using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_version.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/Header")]
    [ApiController]
    public class HeaderController : ControllerBase
    {

        [MapToApiVersion("1.0")]
        [HttpGet]
        public IActionResult Get()=>Content("Version 1");

        [Route("Get")]
        [MapToApiVersion("2.0")]
        [HttpGet]
        public IActionResult Getv2()=>Content("Version 2");
    }
}