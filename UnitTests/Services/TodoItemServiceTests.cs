using BoyumIT.TodoApi.Models;
using BoyumIT.TodoApi.Models.Enums;
using BoyumIT.TodoApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Services
{
    public class TodoItemServiceTests : IDisposable
    {
        private readonly TodoListContext _context; // This is actually an Integration test. As soon as we use a database and add a persistence layer (with UnitOfWork and Repository)
                                                   // we can mock the dependencies properly. This is the main reason we need Data Abstraction and Data Independence, that we don't have here yet.
        private readonly Mock<ILogger<TodoItemService>> _mockLogger;
        private readonly TodoItemService _service;

        public TodoItemServiceTests()
        {
            // Setup in-memory database
            var options = new DbContextOptionsBuilder<TodoListContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use unique name to avoid conflicts
                .Options;
            _context = new TodoListContext(options);

            _mockLogger = new Mock<ILogger<TodoItemService>>();
            _service = new TodoItemService(_context, _mockLogger.Object);
        }

        [Theory]
        [InlineData("Task 1", "Description for Task 1", Status.New)]
        [InlineData("Task 2", "Description for Task 2", Status.Completed)]
        [InlineData("Invalid Task", "Invalid Description", (Status)999)] //  999 is an invalid value
        public async Task CreateTodoItemAsync_ReturnsTodoItem_WithExpectedValuesOrThrows(string title, string description, Status status)
        {
            // Arrange
            var todoItem = new TodoItem { Title = title, Description = description, Status = status };

            // Act
            var result = await _service.CreateTodoItemAsync(todoItem);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(title, result.Title);
            Assert.Equal(description, result.Description);
            Assert.Equal(status, result.Status);
        }


        [Fact(DisplayName = "Check Id validation in TodoItemCreation")]
        public async Task CreateTodoItemAsync_ThrowsInvalidOperationException_WhenIdExists()
        {
            // Arrange
            var existingId = Guid.NewGuid();
            var todoItem = new TodoItem { Id = existingId, Title = "Existing Task", Description = "Existing Description" };
            await _context.TodoItems.AddAsync(todoItem);
            await _context.SaveChangesAsync();

            var newItem = new TodoItem { Id = existingId, Title = "New Task", Description = "New Description" };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreateTodoItemAsync(todoItem));
            Assert.Equal($"A todo item with ID {existingId} already exists.", exception.Message);
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreateTodoItemAsync(newItem));

        }


        public void Dispose()
        {
            _context.Database.EnsureDeleted(); // Clean up the in-memory database
        }
    }
}
