using System.Net.Http;
using System.Threading.Tasks;
using code.web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using code.web.Services.Dto;
using code.web.Services;

namespace code.web.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly IInstructorService _instructorSvc;
        public InstructorsController(IInstructorService instructorService)
        {
            _instructorSvc = instructorService;
        }

        [Route("~/Instructors")]
        public async Task<IActionResult> Index()
        {
            var model = await _instructorSvc.GetAllAsync();
            return View(model);
        }

        [Route("~/Instructors/{id}")]
        public async Task<IActionResult> DetailAsync([FromRoute]int id)
        {
            var instructor = await _instructorSvc.FindAsync(id);
            if(instructor==null)
                return NotFound();
            return View(instructor);
        }

        [Route("~/Instructors/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("~/Instructors/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastName,FirstName,HireDate")]InstructorDto addnew)
        {
            if(addnew==null)
                return BadRequest();
            try
            {
                await _instructorSvc.AddAsync(addnew);
                return RedirectToAction(nameof(Index));
            }
            catch(HttpRequestException ex)
            {
                return View(addnew);
            }
        }

        [Route("~/Instructors/Edit/{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int? id)
        {
            if(id==null)
                return NotFound();
            var model = await _instructorSvc.FindAsync(id.Value);
            if (model==null)
                return NotFound();
            return View(model);
        }

        [Route("~/Instructors/Edit/{id}")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]int id,[Bind("Id,LastName,FirstName,HireDate")]Instructor update)
        {
            if(id!=update.Id)
                return NotFound();
            try
            {
                var model = await _instructorSvc.UpdateAsync(update);
                return RedirectToAction(nameof(Index));
            }
            catch(HttpRequestException ex)
            {
                return View(update);
            }
        }

        [Route("~/Instructors/Delete/{id}")]
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute]int? id)
        {
            if(id==null)
                return NotFound();
            var model = await _instructorSvc.FindAsync(id.Value);
            if(model==null)
                return NotFound();
            return View(model);
        }

        [Route("~/Instructors/Delete/{id}")]
        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed([FromRoute]int id)
        {
            await _instructorSvc.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}