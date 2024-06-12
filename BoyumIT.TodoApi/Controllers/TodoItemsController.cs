using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var items = await _todoItemService.GetTodoItemsAsync();
            return Ok(items);
        }

        // GET: api/TodoItems/{id}
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

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            var createdTodoItem = await _todoItemService.CreateTodoItemAsync(todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = createdTodoItem.Id }, createdTodoItem);
        }

        // PUT: api/TodoItems/{id}
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
            return NoContent();
        }

        // DELETE: api/TodoItems/{id}
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

        // PUT: api/TodoItems/{id}/Title
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

        // PUT: api/TodoItems/{id}/Description
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

        // PUT: api/TodoItems/{id}/Status
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
