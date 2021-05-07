using System.Threading;
using System.Threading.Tasks;
using code.Api.Application.Command;
using code.Api.Application.Queries;
using code.Api.Registry;
using code.Domain.Event;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using code.Api.Extensions;
using code.Api.Application.Dto;
using code.Api.Extensions.Pagination;
using code.Api.Application.ViewModels;

namespace code.Api.Controllers
{
    [ApiVersion("1.0")]
    [EnableCors(StartupExtensionMethods.CorsPolicy)]
    [Produces("application/json")]
    [Route("api/v{ver:apiVersion}/")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentQueries _studentQueries;

        private readonly IRequestHandler<CreateStudentCommand, bool> _createStudentCommand;

        private readonly IRequestHandler<UpdateStudentCommand, bool> _updateStudentCommand;

        private readonly IRequestHandler<DeleteStudentCommand, bool> _deleteStudentCommand;

        public record UrlQueryParameters(int Limit = 50, int Page = 1);
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
        /// 取得所有學生資料 
        /// GET api/v1/Student/[?Limit=3&amp;Page=10]
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
        [Route("Student",Name=nameof(GetAll))]
        [HttpGet]
        [ProducesResponseType(typeof(PaginationViewModel<StudentDto>),(int)StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll(
        [FromQuery] UrlQueryParameters urlQueryParameters,
        [FromQuery]SortingParams sorting)
        {
            PaginationViewModel<StudentDto> viewmodel;
            if(sorting !=null && !string.IsNullOrEmpty(sorting.SortBy))
                viewmodel = await _studentQueries.PaginationAsync(urlQueryParameters.Limit,urlQueryParameters.Page,CancellationToken.None,sorting);
            else
                viewmodel = await _studentQueries.PaginationAsync(urlQueryParameters.Limit,urlQueryParameters.Page,CancellationToken.None);
            // viewmodel=GeneratePageLinks(urlQueryParameters, viewmodel);
            return Ok(GeneratePageLinks(urlQueryParameters, viewmodel));
        }

        private PaginationViewModel<StudentDto> GeneratePageLinks(UrlQueryParameters queryParameters,PaginationViewModel<StudentDto> response)
        {
            if(response.CurrentPage>1)
            {
                var prevRoute = Url.RouteUrl(nameof(GetAll), new { limit=queryParameters.Limit,page =queryParameters.Page-1 });
                response.AddResourceLink(LinkedResourceType.Prev, prevRoute);
            }
            if(response.CurrentPage<response.TotalPages)
            {
                var nextRoute = Url.RouteUrl(nameof(GetAll), new {limit= queryParameters.Limit, page = queryParameters.Page + 1});
                response.AddResourceLink(LinkedResourceType.Next, nextRoute);
            }
            return response;
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
        [Route("Student/{id}")]
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
                return NotFound();
            else
                return Ok(result);
        }

        /// <summary>
        /// 新增學生資料
        /// </summary>
        /// <param name="addNew">lastname,firstname,enrollmentDate</param>
        /// <returns>if add success will return true</returns>
        [Route("Student")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody]CreateStudentCommand addNew)
        {
            if(addNew==null)
                return BadRequest();
            var result =await _createStudentCommand.Handle(addNew,CancellationToken.None);
            return CreatedAtAction(nameof(Add), addNew,result);
        }

        /// <summary>
        /// 更新學生資料
        /// </summary>
        /// <param name="id">學生代碼</param>
        /// <param name="updateItem">lastname,firstname,enrollmentDate</param>
        /// <returns>if update success will return true</returns>
        [Route("Student/{id}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id,[FromBody]UpdateStudentCommand updateItem)
        {
            if(id!= updateItem.Id)
            {
                return BadRequest();
            }
            if(updateItem ==null)
                return BadRequest();
            //驗證不通常，回應badrequest
            var result=await _updateStudentCommand.Handle(updateItem, CancellationToken.None);
            return NoContent();

        }

        /// <summary>
        /// 刪除學生資料
        /// </summary>
        /// <param name="id">欲刪除的學號</param>
        /// <returns>if delete success will return true</returns>
        [Route("Student/{id:int}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute]int? id)
        {
            if(id ==0)
                return BadRequest();
            var deleteItem = new DeleteStudentCommand() { StudentId = id.Value };
            var result=await _deleteStudentCommand.Handle(deleteItem, CancellationToken.None);
            return NoContent();
        }
    }
}