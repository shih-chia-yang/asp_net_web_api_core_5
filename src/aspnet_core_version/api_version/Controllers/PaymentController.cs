using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_version.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()=>Content("Version 1");

        [MapToApiVersion("2.0")]
        [HttpGet]
        public IActionResult GetV2()=>Content("Version 2");
    }
}