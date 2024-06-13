using BoyumIT.TodoApi.Controllers;
using BoyumIT.TodoApi.Models;
using BoyumIT.TodoApi.Models.Enums;
using BoyumIT.TodoApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
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
            var mockItems = new List<TodoItem> { new TodoItem() { Id = Guid.NewGuid(), Title = "todo" }, new TodoItem() { Id = Guid.NewGuid(), Title = "todo 2" } };
            _mockService.Setup(service => service.GetTodoItemsAsync()).ReturnsAsync(mockItems);

            // Act
            var result = await _controller.GetTodoItems();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<TodoItem>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
            Assert.Equal(mockItems, returnValue);
            _mockService.Verify(service => service.GetTodoItemsAsync(), Times.Once);
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
            _mockService.Verify(_mockService => _mockService.GetTodoItemAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task PostTodoItem_ReturnsCreatedAtActionResult_WithTodoItem()
        {
            // Arrange
            var todoItem = new TodoItem { Id = Guid.NewGuid(), Title = "Test" };
            _mockService.Setup(service => service.CreateTodoItemAsync(It.IsAny<TodoItem>())).ReturnsAsync(todoItem);

            // Act
            var result = await _controller.PostTodoItem(new TodoItem() { Id = Guid.NewGuid(), Title = "todo3" });

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<TodoItem>(actionResult.Value);
            Assert.Equal(todoItem.Id, returnValue.Id);
            _mockService.Verify(service => service.CreateTodoItemAsync(It.IsAny<TodoItem>()), Times.Once);
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
            _mockService.Verify(service => service.UpdateTodoItemAsync(todoItem.Id, todoItem), Times.Once);
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
            _mockService.Verify(service => service.DeleteTodoItemAsync(todoItemId), Times.Once);
        }

        [Fact]
        public async Task UpdateTodoItemTitle_ReturnsNotFoundResult_WhenIdsDoNotMatch()
        {
            // Arrange
            var todoItemId = Guid.NewGuid();
            var updatedModel = new TodoItem { Id = Guid.NewGuid(), Title = "New Title" };

            _mockService.Setup(service => service.UpdateTitleAsync(todoItemId, updatedModel.Title));


            // Act
            var result = await _controller.UpdateTodoItemTitle(Guid.NewGuid(), updatedModel.Title);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            _mockService.Verify(_mockService => _mockService.UpdateTitleAsync(todoItemId, updatedModel.Title), Times.Never);
        }

        [Fact]
        public async Task UpdateTodoItemDescription_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            var todoItemId = Guid.NewGuid();
            var updatedModel = new TodoItem { Id = Guid.NewGuid(), Title = "new toDo", Description = "New Description" };
            _mockService.Setup(service => service.UpdateDescriptionAsync(todoItemId, updatedModel.Description)).ReturnsAsync((TodoItem)null);

            // Act
            var result = await _controller.UpdateTodoItemDescription(todoItemId, updatedModel.Description);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            _mockService.Verify(service => service.UpdateDescriptionAsync(todoItemId, updatedModel.Description), Times.Once);
        }

        [Fact]
        public async Task UpdateTodoItemStatus_ReturnsOk_WithNoContent()
        {
            // Arrange
            var todoItemId = Guid.NewGuid();
            var updatedModel = new TodoItem { Id = todoItemId, Title = "todo4", Status = BoyumIT.TodoApi.Models.Enums.Status.Completed };
            _mockService.Setup(service => service.UpdateStatusAsync(todoItemId, updatedModel.Status)).ReturnsAsync(updatedModel);

            // Act
            var result = await _controller.UpdateTodoItemStatus(todoItemId, updatedModel.Status);

            // Assert
            var actionResult = Assert.IsType<NoContentResult>(result);
            _mockService.Verify(_mockService => _mockService.UpdateStatusAsync(todoItemId, updatedModel.Status), Times.Once);
        }
    }
}