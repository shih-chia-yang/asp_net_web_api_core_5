using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace empty_core_project.Controllers
{
    [Route("api/Test1")]
    [Route("api/[controller]")]
    [ApiController]
    public class MultiRouteController : ControllerBase
    {
        [Route("All")]
        [Route("GetContents")]
        public string GetContent()
        {
            return "Multiple route sample";
        }
    }
}