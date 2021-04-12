using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using todo.infrastructure;

namespace tests.todo.unittests
{
    public class InMemoryDbContextFixture : IDisposable
    {
        private TodoContext _context;

        public TodoContext Context => _context;

        public InMemoryDbContextFixture()
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase("IntellectualProperty")
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
            _context = new TodoContext(options);
            _context.Database.EnsureCreated();
            TodoContextSeed data = new TodoContextSeed();
            data.SeedAsync(_context).GetAwaiter();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
