using Microsoft.AspNetCore.Mvc;
using BoyumIT.TodoApi.Models;
using BoyumIT.TodoApi.Services;
using BoyumIT.TodoApi.Models.Enums;

namespace BoyumIT.TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemsController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        /// <summary>
        /// Retrieves all todo items.
        /// </summary>
        /// <returns>A list of todo items.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var items = await _todoItemService.GetTodoItemsAsync();
            return Ok(items);
        }

        /// <summary>
        /// Retrieves a specific todo item by its ID.
        /// </summary>
        /// <param name="id">The ID of the todo item.</param>
        /// <returns>The todo item with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(Guid id)
        {
            var todoItem = await _todoItemService.GetTodoItemAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }

        /// <summary>
        /// Creates a new todo item.
        /// </summary>
        /// <param name="todoItem">The todo item to create.</param>
        /// <returns>A newly created todo item.</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            var createdTodoItem = await _todoItemService.CreateTodoItemAsync(todoItem);
            if (createdTodoItem == null) // Todo: Return Conflict if id already exists
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetTodoItem), new { id = createdTodoItem.Id }, createdTodoItem);
        }

        /// <summary>
        /// Updates a specific todo item.
        /// </summary>
        /// <param name="id">The ID of the todo item to update.</param>
        /// <param name="todoItem">The updated todo item.</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the item is updated successfully</response>
        /// <response code="400">If the item ID does not match the ID of the todoItem parameter</response>
        /// <response code="404">If the item cannot be found</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest("ID mismatch");
            }

            var updatedTodoItem = await _todoItemService.UpdateTodoItemAsync(id, todoItem);
            if (updatedTodoItem == null)
            {
                return NotFound();
            }
            return NoContent(); // https://stackoverflow.com/questions/59861379/oknull-vs-nocontent-in-asp-net-core-which-is-more-efficient Can be changed to return created item, or Ok() with the item.
        }

        /// <summary>
        /// Deletes a specific todo item.
        /// </summary>
        /// <param name="id">The ID of the todo item to delete.</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the item is deleted successfully</response>
        /// <response code="404">If the item cannot be found</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            var success = await _todoItemService.DeleteTodoItemAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Updates the title of a specific todo item.
        /// </summary>
        /// <param name="id">The ID of the todo item to update.</param>
        /// <param name="title">The new title for the todo item.</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the title is updated successfully</response>
        /// <response code="404">If the item cannot be found</response>
        [HttpPut("{id}/Title")]
        public async Task<IActionResult> UpdateTodoItemTitle(Guid id, [FromBody] string title)
        {
            var updatedTodoItem = await _todoItemService.UpdateTitleAsync(id, title);
            if (updatedTodoItem == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Updates the description of a specific todo item.
        /// </summary>
        /// <param name="id">The ID of the todo item to update.</param>
        /// <param name="description">The new description for the todo item.</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the description is updated successfully</response>
        /// <response code="404">If the item cannot be found</response>
        [HttpPut("{id}/Description")]
        public async Task<IActionResult> UpdateTodoItemDescription(Guid id, [FromBody] string description)
        {
            var updatedTodoItem = await _todoItemService.UpdateDescriptionAsync(id, description);
            if (updatedTodoItem == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Updates the status of a specific todo item.
        /// </summary>
        /// <param name="id">The ID of the todo item to update.</param>
        /// <param name="status">The new status for the todo item.</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the status is updated successfully</response>
        /// <response code="404">If the item cannot be found</response>
        [HttpPut("{id}/Status")]
        public async Task<IActionResult> UpdateTodoItemStatus(Guid id, [FromBody] Status status)
        {
            var updatedTodoItem = await _todoItemService.UpdateStatusAsync(id, status);
            if (updatedTodoItem == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
