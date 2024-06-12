using BoyumIT.TodoApi.Models;
using BoyumIT.TodoApi.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BoyumIT.TodoApi.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly TodoListContext _context;
        private readonly ILogger<TodoItemService> _logger;

        public TodoItemService(TodoListContext context, ILogger<TodoItemService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetTodoItemAsync(Guid id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem)
        {
            try
            {
                // Set the creation dateTime to now
                todoItem.CreationTime = DateTime.UtcNow;

                _context.TodoItems.Add(todoItem);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Todo item created successfully with ID {TodoItemId}", todoItem.Id);
                return todoItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a todo item.");
                throw;
            }
        }

        public async Task<TodoItem> UpdateTodoItemAsync(Guid id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                _logger.LogWarning("Attempted to update a todo item with mismatched ID {TodoItemId}", id);
                return null;
            }

            todoItem.UpdateTime = DateTime.UtcNow;

            _context.Entry(todoItem).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Todo item with ID {TodoItemId} updated successfully", todoItem.Id);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!TodoItemExists(id))
                {
                    _logger.LogWarning("Attempted to update a non-existing todo item with ID {TodoItemId}", id);
                    return null;
                }
                else
                {
                    LogErrorWithTodoItemContext(ex, "updating the values of", id);
                    throw;
                }
            }

            return todoItem;
        }

        public async Task<bool> DeleteTodoItemAsync(Guid id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return false;
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TodoItem> UpdateTitleAsync(Guid id, string title)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                _logger.LogWarning("Failed to update title. Todo item with ID {TodoItemId} not found.", id);
                return null;
            }

            todoItem.Title = title;
            todoItem.UpdateTime = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Todo item with ID {TodoItemId} updated successfully", todoItem.Id);
            }
            catch (Exception ex)
            {
                LogErrorWithTodoItemContext(ex, "updating the Status", id);
                throw;
            }
            return todoItem;
        }

        public async Task<TodoItem> UpdateDescriptionAsync(Guid id, string description)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                _logger.LogWarning("Failed to update title. Todo item with ID {TodoItemId} not found.", id);
                return null;
            }

            todoItem.Description = description;
            todoItem.UpdateTime = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Todo item with ID {TodoItemId} updated successfully", id);
            }
            catch (Exception ex)
            {
                LogErrorWithTodoItemContext(ex, "updating the Description", id);
                throw;
            }
            return todoItem;
        }

        public async Task<TodoItem> UpdateStatusAsync(Guid id, Status status)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                _logger.LogWarning("Failed to update title. Todo item with ID {TodoItemId} not found.", id);
                return null;
            }

            todoItem.Status = status;
            todoItem.UpdateTime = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Todo item with ID {TodoItemId} updated successfully", id);

            }
            catch (Exception ex)
            {
                LogErrorWithTodoItemContext(ex, "updating the Status", id);
                throw;
            }
            return todoItem;
        }

        private bool TodoItemExists(Guid id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        private void LogErrorWithTodoItemContext(Exception ex, string action, Guid todoItemId)
        {
            _logger.LogError(ex, "An error occurred while {Action} of todo item with ID {TodoItemId}.", action, todoItemId);
        }
    }
}
