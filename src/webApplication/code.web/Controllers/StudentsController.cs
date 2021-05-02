using System.Linq;
using System;
using System.Threading.Tasks;
using code.web.Services;
using code.web.Services.Dto;
using code.web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace code.web.Controllers
{
    public class StudentsController : Controller
    {

        private readonly IStudentService _studentSvc;
        public StudentsController(IStudentService studentSvc)
        {
            _studentSvc = studentSvc;
        }

        public async Task<IActionResult> Index([FromQuery]SortOrder sort)
        {
            ViewData["NameSortParm"] = sort.SortBy=="LastName" ? "LastName_desc" : "LastName";
            ViewData["DateSortParm"] = sort.SortBy=="EnrollmentDate" ?"EnrollmentDate_desc": "EnrollmentDate";
            if(sort!=null && sort.SortBy.Contains("desc"))
            {
                sort.SortBy=sort.SortBy.Replace("_desc","");
                sort.IsAscending = false;
            }
            else
                sort.IsAscending = true;
            var model =await _studentSvc.GetAllAsync(sort);
            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var student = await _studentSvc.FindAsync(id.ToString());
            if(student ==null)
                return NotFound();
            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("EnrollmentDate,FirstName,LastName")] StudentDto student)
        {
            if(student==null)
                return BadRequest();
            try
            {
                await _studentSvc.AddAsync(student);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View(student);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id==null)
                return NotFound();
            var model =await _studentSvc.FindAsync(id.ToString());
            if(model==null)
                return NotFound();
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,LastName,FirstName,EnrollmentDate")]Student student)
        {
            if(id!=student.Id)
                return NotFound();
            try
            {
                var model =await _studentSvc.UpdateAsync(student);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                // return BadRequest(ex);
                return View(student);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id==null)
                return NotFound();
            var model =await _studentSvc.FindAsync(id.ToString());
            if(model==null)
                return NotFound();
            return View(model);
        }

        
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentSvc.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}