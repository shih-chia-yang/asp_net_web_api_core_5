

using System.Linq;
using todo.domain.Models;
using todo.infrastructure.Repositories;
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

        [Fact]
        public void test_todoitemrepo_getall_should_be_return_data()
        {
            //Given
            ITodoRepository repo = new TodoRepository(_inMemory.Context);
            //When
            var data = repo.GetAll().Result;
            //Then
            Assert.NotNull(data);
            Assert.True(data.Count() == 3);
        }

        [Fact]
        public void test_todoitemrepo_findbyid_should_be_return_coorect_result()
        {
            //Given
            int expected = 1;
            ITodoRepository repo = new TodoRepository(_inMemory.Context);
            //When
            var selected = repo.FindByIdAsync(expected);
            //Then
            Assert.NotNull(selected);
            Assert.Equal(expected,selected.Id);
        }

        [Fact]
        public void Test_TodoItemsRepo_AddNew_Then_Should_Be_increasement()
        {
            //Given
            var newTodoItem = TodoItem.Create(4,"test-1","AddNew");
            ITodoRepository repo =new TodoRepository(_inMemory.Context);
            //When
            repo.Add(newTodoItem);
            _inMemory.Context.SaveChangesAsync();
            //Then
            var added = repo.FindByIdAsync(newTodoItem.Id).Result;
            Assert.Equal(newTodoItem.Id, added.Id);
            Assert.Equal(newTodoItem.Name,added.Name);
            Assert.Equal(newTodoItem.Event, added.Event);
        }
    }
}
