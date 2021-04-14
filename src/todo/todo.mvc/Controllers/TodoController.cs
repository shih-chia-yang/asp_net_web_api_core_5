using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using todo.infrastructure;
using todo.infrastructure.Repositories;
using todo.domain.Models;

namespace todo.mvc.Controllers
{
    public class TodoController : Controller
    {
        private ITodoRepository _iTodoRepository;

        private TodoContext _context;

        public TodoController(ITodoRepository iTodoRepository,TodoContext context)
        {
            _iTodoRepository = iTodoRepository;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var todoModel = await _iTodoRepository.GetAll();
            return View(todoModel);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var todo =await _iTodoRepository.FindByIdAsync(id);
            return View(todo);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]IFormCollection collection)
        {
            try
            {
                var item =new TodoItem();
                item.Name=collection["Name"];
                item.Event=collection["Event"];
                _iTodoRepository.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var todo =await _iTodoRepository.FindByIdAsync(id);
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[FromForm]IFormCollection collection)
        {
            try
            {
                var todo =await _iTodoRepository.FindByIdAsync(id);
                todo.Name=collection["Name"];
                todo.Event=collection["Event"];
                _iTodoRepository.Update(todo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var todo =await _iTodoRepository.FindByIdAsync(id);
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, [FromForm]IFormCollection collection)
        {
            try
            {
                var todo =await _iTodoRepository.FindByIdAsync(id);
                _iTodoRepository.Delete(todo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                
                return View(ex.Message);
            }
        }
    }
}