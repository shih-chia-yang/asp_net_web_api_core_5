

using System.Linq;
using Xunit;

namespace tests.todo.unittests
{
    

    public class TodoItemRepositoryTests
    {
        InMemoryDbContextFixture _inMemory;

        public TodoItemRepositoryTests()
        {
            _inMemory = new InMemoryDbContextFixture();
        }

        [Fact]
        public void test_TodoItems_table_should_be_created()
        {
            //Given
            var todoItems = _inMemory.Context.TodoItems;
            //When
            //Then
            Assert.NotNull(todoItems);
            Assert.True(todoItems.Count() == 3);

        }
    }
}
