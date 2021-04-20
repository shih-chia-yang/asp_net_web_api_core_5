using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace url.version.v1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HiController : ControllerBase
    {
        [HttpGet("Get")]
        public IActionResult Get()=>Content("hi version 1");
    }
}

namespace url.version.v2.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class HiController:ControllerBase
    {
        [HttpGet("Get")]
        public IActionResult Get()=>Content("hi version 2");
    }
}