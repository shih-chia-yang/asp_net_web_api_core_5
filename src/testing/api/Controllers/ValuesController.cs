using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<string> value = await Task.Run(()=>
                new List<string>(){ "value1", "value2" }
                );
            return Ok(value);
        }

        [Route("post")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            return await Task.Run(()=>Created("",value));
        }
    }
}