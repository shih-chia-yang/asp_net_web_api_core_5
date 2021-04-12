using System.Collections.Generic;
using System.Threading.Tasks;
using todo.domain.Models;

namespace todo.infrastructure.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAll();

        Task<TodoItem> FindByIdAsync(int id);
    }
}