using BoyumIT.TodoApi.Controllers;
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
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItem> UpdateTodoItemAsync(Guid id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return null;
            }

            _context.Entry(todoItem).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return null;
                }
                else
                {
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
                return null;
            }

            todoItem.Title = title;
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItem> UpdateDescriptionAsync(Guid id, string description)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return null;
            }

            todoItem.Description = description;
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItem> UpdateStatusAsync(Guid id, Status status)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return null;
            }

            todoItem.Status = status;
            await _context.SaveChangesAsync();
            return todoItem;
        }

        private bool TodoItemExists(Guid id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
