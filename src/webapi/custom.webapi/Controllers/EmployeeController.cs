using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using custom.webapi.Models;
using custom.webapi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace custom.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        public EmployeeController(IEmployeeRepository repo)
        {
            _repo = repo;
        }
        [Route("~/api/Employee")]
        // GET: api/Employee
        [HttpGet]
        public IActionResult GetEmployees()
        {
            return Ok(_repo.GetAll());
        }

        // GET: api/Employee/5
        // [HttpGet("{id}", Name = "Get")]
        [Route("~/api/Employee/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var employee = _repo.FindById(id);
            if (employee == null)
                return NotFound();
            else
                return Ok(employee);
        }

        // POST: api/Employee
        [Route("~/api/Employee")]
        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            if(employee==null)
                return BadRequest();
            else
            {
                _repo.Add(employee);
                return new CreatedResult($"/api/Employee",employee);
            }
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Employee employee)
        {
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
