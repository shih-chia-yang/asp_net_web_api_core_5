

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo.domain.Models;

namespace todo.infrastructure
{
    public class TodoContextSeed
    {
        public async Task SeedAsync(TodoContext context)
        {
            var useCustomizationData = true;
            string contentPath = Environment.CurrentDirectory;
            try
            {
                if (!context.TodoItems.Any() && useCustomizationData)
                {
                    await context.TodoItems.AddRangeAsync(GetPreConfigurationTodoItems());
                    await context.SaveChangesAsync();
                }

            }
            catch
            {

            }
        }

        public IEnumerable<TodoItem> GetPreConfigurationTodoItems()
        {
            return new List<TodoItem>()
            {
                new TodoItem(){Id=1, Name="stone",Event="coding"},
                new TodoItem(){Id=2, Name="stone1",Event="check email"},
                new TodoItem(){Id=3, Name="stone2",Event="call boss"}
            };
        }
    }
}