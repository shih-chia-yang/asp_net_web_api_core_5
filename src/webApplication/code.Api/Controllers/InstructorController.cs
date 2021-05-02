using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using code.Api.Application.Command;
using code.Api.Application.Queries;
using code.Domain.Event;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using code.Api.Registry;

namespace code.Api.Controllers
{
    [EnableCors(StartupExtensionMethods.CorsPolicy)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorQueries _instructorQueries;
        private readonly IRequestHandler<CreateInstructorCommand, bool> _createInstructorCommand;
        private readonly IRequestHandler<UpdateInstructorCommand, bool> _updateInstructorCommand;
        private readonly IRequestHandler<DeleteInstructorCommand,bool> _deleteInstructorCommand;
        public InstructorController(IInstructorQueries instructorQueries,
        IRequestHandler<CreateInstructorCommand, bool> createInstructCommand,
        IRequestHandler<DeleteInstructorCommand,bool> deleteInstructorCommand,
        IRequestHandler<UpdateInstructorCommand,bool> updateInstructorCommand)
        {
            _instructorQueries=instructorQueries;
            _createInstructorCommand = createInstructCommand;
            _deleteInstructorCommand = deleteInstructorCommand;
            _updateInstructorCommand = updateInstructorCommand;
        }

        /// <summary>
        /// 取得講師所有資料
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        [Route("~/api/Instructors")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var viewmodel = await _instructorQueries.GetAllAsync();
            return Ok(viewmodel);
        }

        /// <summary>
        /// 依照代碼取得指定講師資料
        /// </summary>
        /// <param name="id">講師代碼</param>
        /// <returns>講師資料</returns>
        /// <response code="200">查詢成功</response>
        /// <response code="204">查無些結果</response>
        /// <response code="400">something goes wrong</response>
        [Route("~/api/Instructor/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int? id)
        {
            if(id==null)
                return NotFound();
            var result = await _instructorQueries.FindAsync(id.Value);
            if(result==null)
                return NoContent();
            else
                return Ok(result);
        }

        /// <summary>
        /// 新增講師資料
        /// </summary>
        /// <param name="addNew"></param>
        /// <returns></returns>
        [Route("~/api/Instructor/Add")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody]CreateInstructorCommand addNew)
        {
            if(addNew==null)
                return BadRequest();
            var result = await _createInstructorCommand.Handle(addNew,CancellationToken.None);
            return CreatedAtAction(nameof(Add), result);
        }

        /// <summary>
        /// 依照講師編號更新講師資料
        /// </summary>
        /// <param name="updateItem"></param>
        /// <returns></returns>
        [Route("~/api/Instructor/Update")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody]UpdateInstructorCommand updateItem)
        {
            if(updateItem==null)
                return NotFound();
            var result = await _updateInstructorCommand.Handle(updateItem, CancellationToken.None);
            return Ok(result);
        }

        /// <summary>
        /// 依照講師編號刪除講師資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("~/api/Instructor/Delete/{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id==null)
                return NotFound();
            var deleteCommand = new DeleteInstructorCommand(id.Value);
            var result = await _deleteInstructorCommand.Handle(deleteCommand,CancellationToken.None);
            return Ok(result);
        }
    }
}