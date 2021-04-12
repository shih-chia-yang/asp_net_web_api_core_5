using Microsoft.EntityFrameworkCore;
using todo.domain.Models;
using todo.infrastructure.EntityConfigurations;

namespace todo.infrastructure
{
    public class TodoContext:DbContext
    {
        public const string DEFAULT_SCHEMA = "todo";

        public DbSet<TodoItem> TodoItems{ get; set; }

        public TodoContext(DbContextOptions<TodoContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoEntityTypeConfiguration());
        }
    }
}