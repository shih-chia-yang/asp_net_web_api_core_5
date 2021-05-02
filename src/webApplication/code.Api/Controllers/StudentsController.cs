using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using code.Api.Application.Command;
using code.Api.Application.Queries;
using code.Api.Registry;
using code.Domain.Event;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace code.Api.Controllers
{
    [EnableCors(StartupExtensionMethods.CorsPolicy)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentQueries _studentQueries;

        private readonly IRequestHandler<CreateStudentCommand, bool> _createStudentCommand;

        private readonly IRequestHandler<UpdateStudentCommand, bool> _updateStudentCommand;

        private readonly IRequestHandler<DeleteStudentCommand, bool> _deleteStudentCommand;
        public StudentsController(IStudentQueries studentQueries,
        IRequestHandler<CreateStudentCommand, bool> createStudentCommand,
        IRequestHandler<UpdateStudentCommand,bool> updateStudentCommand,
        IRequestHandler<DeleteStudentCommand,bool> deleteStudentCommand
        )
        {
            _studentQueries = studentQueries;
            _createStudentCommand = createStudentCommand;
            _updateStudentCommand = updateStudentCommand;
            _deleteStudentCommand = deleteStudentCommand;
        }

        /// <summary>
        ///  取得所有學生資料
        /// </summary>
        /// <remarks>
        /// sample Result
        /// 
        /// Get/Students
        /// {
        ///     [
        ///          {
        ///              "enrollmentDate": "2005-09-01T00:00:00",
        ///              "enrollments": null,
        ///              "id": 1,
        ///              "lastName": "Carson",
        ///              "firstName": "Alexander",
        ///              "fullName": "Carson,Alexander"
        ///          },
        ///          {
        ///              "enrollmentDate": "2002-09-01T00:00:00",
        ///              "enrollments": null,
        ///              "id": 2,
        ///              "lastName": "Meredith",
        ///              "firstName": "Alonso",
        ///              "fullName": "Meredith,Alonso"
        ///          },
        ///     ]
        /// }
        /// </remarks>
        /// <returns>Student List</returns>
        /// <response code="200">查詢成功</response>
        /// <response code="204">查無此結果</response>
        /// <response code="400">something goes wrong</response>
        [Route("~/api/Student")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery]SortingParams sorting)
        {
            IEnumerable<Student> viewmodel = Enumerable.Empty<Student>();
            if(sorting !=null && !string.IsNullOrEmpty(sorting.SortBy))
                viewmodel = await _studentQueries.GetAllAsync(sorting);
            else
                viewmodel = await _studentQueries.GetAllAsync();
            
            return Ok(viewmodel);
        }

        /// <summary>
        ///  依照學號取得指定學生資料
        /// </summary>
        /// <remarks>
        /// sample Result:
        /// 
        /// Get/Student/1
        /// {
        ///     "Id":1,
        ///     "LastName":"stone",
        ///     "FirstName":"eric",
        ///     "EnrollmentDate":"2021-09-03",
        ///     "Enrollment:[
        ///         "Id":1,
        ///         "StudentId":1,
        ///         "CourseId":4022,
        ///         "Grade":0,
        ///         "Course":{
        ///             "Id":4022,
        ///             "Title":"Microeconomics",
        ///             "Grade":3
        ///             }
        ///      ]
        /// }
        /// </remarks>
        /// <param name="id">學號</param>
        /// <returns>學生資料</returns>
        /// <response code="200">查詢成功</response>
        /// <response code="204">查無此結果</response>
        /// <response code="400">something goes wrong</response>
        [Route("~/api/Student/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int? id)
        {
            if(id==null)
                return NotFound();
            var result = await _studentQueries.FindAsync(id.Value);
            if(result==null)
                return NoContent();
            else
                return Ok(result);
        }

        /// <summary>
        /// 新增學生資料
        /// </summary>
        /// <param name="addNew">lastname,firstname,enrollmentDate</param>
        /// <returns>if add success will return true</returns>
        [Route("~/api/Student")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody]CreateStudentCommand addNew)
        {
            if(addNew==null)
                return BadRequest();
            var result =await _createStudentCommand.Handle(addNew,CancellationToken.None);
            return CreatedAtAction(nameof(Add), result);
        }

        /// <summary>
        /// 更新學生資料
        /// </summary>
        /// <param name="updateItem">lastname,firstname,enrollmentDate</param>
        /// <returns>if update success will return true</returns>
        [Route("~/api/Student")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody]UpdateStudentCommand updateItem)
        {
            if(updateItem ==null)
                return BadRequest();
            var result=await _updateStudentCommand.Handle(updateItem, CancellationToken.None);
            return Ok(result);

        }

        /// <summary>
        /// 刪除學生資料
        /// </summary>
        /// <param name="id">欲刪除的學號</param>
        /// <returns>if delete success will return true</returns>
        [Route("~/api/Student/{id:int}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute]int? id)
        {
            if(id ==0)
                return BadRequest();
            var deleteItem = new DeleteStudentCommand() { StudentId = id.Value };
            var result=await _deleteStudentCommand.Handle(deleteItem, CancellationToken.None);
            return Ok(result);
        }
    }
}