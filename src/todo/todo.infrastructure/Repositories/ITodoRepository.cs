using System.Collections.Generic;
using System.Threading.Tasks;
using todo.domain.Models;

namespace todo.infrastructure.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAll();

        Task<TodoItem> FindByIdAsync(int id);

        TodoItem Add(TodoItem item);

        void Update(TodoItem item);

        void Delete(TodoItem item);
    }
}