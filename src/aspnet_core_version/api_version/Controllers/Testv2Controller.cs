using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_version.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/Test")]
    [ApiController]
    public class TestControllerv2 : ControllerBase
    {
        /// <summary>
        /// get test2 version
        /// </summary>
        /// <returns>api version</returns>
        /// <response code="200">return api version</response>
        /// <response code="400">if something wrong</response>
        /// 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get()=>Content("Version 2");
    }
}