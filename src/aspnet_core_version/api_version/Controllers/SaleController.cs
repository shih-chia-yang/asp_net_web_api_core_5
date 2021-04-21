using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_version.Controllers
{
    // [Route("api/[controller]")]
    // [ApiController]
    // public class SaleController : ControllerBase
    // {
    // }

    [Route("api/sale")]
    [ApiController]
    public class SaleControllerV1:ControllerBase
    {
        [MapToApiVersion("1.0")]
        [HttpGet]           
        public IActionResult Get()=> Content("Version 1");
    }

    [Route("api/sale")]
    [ApiController]
    public class SaleControllerV2:ControllerBase
    {
        [MapToApiVersion("2.0")]
        [HttpGet]
        public IActionResult Get()=>Content("Version 2");
    }
}