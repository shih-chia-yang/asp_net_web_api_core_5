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
        [Route("~/api/Employee")]
        [HttpPut]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            if(employee == null||id!=employee.Id)
                return BadRequest();
            else
            {
                _repo.Update(employee);
                return new CreatedResult($"/api/Employee/{id}",employee);
            }
        }

        [Route("~/api/Employee")]
        // DELETE: api/Employee/5
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var value =_repo.FindById(id);
            if(value ==null)
                return NotFound();
            else
            {
                _repo.Update(value);
                return NoContent();
            }
        }
    }
}
