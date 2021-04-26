using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Controllers
{
    [Authorize(Policy="Member")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var dictionary = new Dictionary<string, string>();
            HttpContext.User.Claims.ToList().ForEach(item => dictionary.Add(item.Type, item.Value));
            return Ok(dictionary);
        }
    }
}