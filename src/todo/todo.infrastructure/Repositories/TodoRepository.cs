using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todo.domain.Models;
using todo.infrastructure.Repositories;
using System.Linq;

namespace todo.infrastructure.Repositories
{
    public class TodoRepository:ITodoRepository
    {
        private readonly TodoContext _context;
        public TodoRepository(TodoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<TodoItem> FindByIdAsync(int id)
        {
            var selected = _context.TodoItems.Where(x => x.Id == id).FirstOrDefaultAsync();
            return selected;
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _context.TodoItems.ToListAsync();
        }

        
    }
}