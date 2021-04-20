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

        /// <summary>
        ///  get all TodoItems
        ///
        /// </summary>
        /// <returns>IEnumerableTodoItem</returns>
        [Route("~/api/GetAllTodos")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<TodoItem>> GetAllTodos()
        {
            return await _iTodoRepo.GetAll();
        }

        /// <summary>
        /// 新增 TodoItem
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// POST /Todo
        /// {
        ///        "Id": 1,
        ///        "Name": "Item1",
        ///        "Event":"do something"
        /// }
        /// </remarks>
        /// <param name="item">TodoItem model</param>
        /// <returns>新增todo 結果</returns>
        /// <response code="201">item create successfully</response>
        /// <response code="400">if TodoItem model is null</response>
        [Route("~/api/AddTodo")]
        [HttpPost]
        [ProducesResponseType(typeof(TodoItem),201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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