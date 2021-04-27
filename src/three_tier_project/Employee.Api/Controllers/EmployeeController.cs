using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee.Domain.Models;
using Employee.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            _context = context;
            DbInitializer.Initializer(_context);
        }

        [Route("GetEmployees")]
        [HttpGet]
        public IEnumerable<EmployeeModel> GetEmployees()
        {
            return _context.Employees.ToList();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<EmployeeModel> Get(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        [Route("AddEmployee")]
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]EmployeeModel input)
        {
            _context.Employees.Add(input);
            await _context.SaveChangesAsync();
            return new CreatedResult("", "Employee added successfully");
        }

        [Route("UpdateEmployee")]
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody]EmployeeModel update)
        {
            _context.Employees.Update(update);
            await _context.SaveChangesAsync();
            return new ObjectResult("Employee modified successfully");
        }

        [Route("DeleteEmployee")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            _context.Employees.Remove(_context.Employees.Find(id));
            await _context.SaveChangesAsync();
            return new ObjectResult("Employee deleted successfully");
        }
    }
}