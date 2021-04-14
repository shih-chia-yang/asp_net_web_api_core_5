using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todo.domain.Models;
using todo.infrastructure;
using todo.infrastructure.Repositories;

namespace todo.mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TodoApiController : ControllerBase
    {
        private ITodoRepository _iTodoRepo;

        private TodoContext _context;
        public TodoApiController(ITodoRepository iTodoRepo,TodoContext context)
        {
            _iTodoRepo=iTodoRepo;
            _context=context;
        }

        [Route("~/api/GetAllTodos")]
        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetAllTodos()
        {
            return await _iTodoRepo.GetAll();
        }

        [Route("~/api/AddTodo")]
        [HttpPost]
        public async Task<TodoItem> AddTodo([FromBody]TodoItem item)
        {
            var addNew=_iTodoRepo.Add(item);
            await _context.SaveChangesAsync();
            return addNew;
        }

        [Route("~/api/UpdateTodo")]
        [HttpPut]
        public async Task<IActionResult> UpdateTodo([FromBody] TodoItem item)
        {
            _iTodoRepo.Update(item);
            await _context.SaveChangesAsync();
            return Ok(HttpStatusCode.OK);
        }

        [Route("~/api/DeleteTodo")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTodo([FromBody]TodoItem item)
        {
            _iTodoRepo.Delete(item);
            await _context.SaveChangesAsync();
            return Ok(HttpStatusCode.OK);
        }
    }
}