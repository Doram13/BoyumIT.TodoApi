using BoyumIT.TodoApi.Controllers;
using BoyumIT.TodoApi.Models;
using BoyumIT.TodoApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests.Controllers
{
    public class TodoItemsControllerTests
    {
        private readonly Mock<ITodoItemService> _mockService;
        private readonly TodoItemsController _controller;

        public TodoItemsControllerTests()
        {
            _mockService = new Mock<ITodoItemService>();
            _controller = new TodoItemsController(_mockService.Object);
        }

        [Fact]
        public async Task GetTodoItems_ReturnsOkObjectResult_WithListOfTodoItems()
        {
            // Arrange
            var mockItems = new List<TodoItem> { new TodoItem(), new TodoItem() };
            _mockService.Setup(service => service.GetTodoItemsAsync()).ReturnsAsync(mockItems);

            // Act
            var result = await _controller.GetTodoItems();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<TodoItem>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetTodoItem_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetTodoItemAsync(It.IsAny<Guid>())).ReturnsAsync((TodoItem)null);

            // Act
            var result = await _controller.GetTodoItem(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostTodoItem_ReturnsCreatedAtActionResult_WithTodoItem()
        {
            // Arrange
            var todoItem = new TodoItem { Id = Guid.NewGuid(), Title = "Test" };
            _mockService.Setup(service => service.CreateTodoItemAsync(It.IsAny<TodoItem>())).ReturnsAsync(todoItem);

            // Act
            var result = await _controller.PostTodoItem(new TodoItem());

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<TodoItem>(actionResult.Value);
            Assert.Equal(todoItem.Id, returnValue.Id);
        }

        [Fact]
        public async Task PutTodoItem_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var todoItem = new TodoItem { Id = Guid.NewGuid(), Title = "Updated Test", Description = "description", Status = BoyumIT.TodoApi.Models.Enums.Status.New };
            _mockService.Setup(service => service.UpdateTodoItemAsync(todoItem.Id, todoItem)).ReturnsAsync(todoItem);

            // Act
            var result = await _controller.PutTodoItem(todoItem.Id, todoItem);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteTodoItem_ReturnsNoContent_WhenDeletionIsSuccessful()
        {
            // Arrange
            var todoItemId = Guid.NewGuid();
            _mockService.Setup(service => service.DeleteTodoItemAsync(todoItemId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteTodoItem(todoItemId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateTodoItemTitle_ReturnsNotFoundResult_WhenIdsDoNotMatch()
        {
            // Arrange
            var todoItemId = Guid.NewGuid();
            var updatedModel = new TodoItem { Title = "New Title" };

            // Act
            var result = await _controller.UpdateTodoItemTitle(Guid.NewGuid(), updatedModel.Title);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateTodoItemDescription_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            var todoItemId = Guid.NewGuid();
            var updatedModel = new TodoItem { Description = "New Description" };
            _mockService.Setup(service => service.UpdateDescriptionAsync(todoItemId, updatedModel.Description)).ReturnsAsync((TodoItem)null);

            // Act
            var result = await _controller.UpdateTodoItemDescription(todoItemId, updatedModel.Description);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateTodoItemStatus_ReturnsOk_WithNoContent()
        {
            // Arrange
            var todoItemId = Guid.NewGuid();
            var updatedModel = new TodoItem { Id = todoItemId, Status = BoyumIT.TodoApi.Models.Enums.Status.Completed };
            _mockService.Setup(service => service.UpdateStatusAsync(todoItemId, updatedModel.Status)).ReturnsAsync(updatedModel);

            // Act
            var result = await _controller.UpdateTodoItemStatus(todoItemId, updatedModel.Status);

            // Assert
            var actionResult = Assert.IsType<NoContentResult>(result);
        }
    }
}