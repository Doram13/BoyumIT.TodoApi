using BoyumIT.TodoApi.Models;
using BoyumIT.TodoApi.Models.Enums;

namespace BoyumIT.TodoApi.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItem>> GetTodoItemsAsync();
        Task<TodoItem> GetTodoItemAsync(Guid id);
        Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem);
        Task<TodoItem> UpdateTodoItemAsync(Guid id, TodoItem todoItem);
        Task<bool> DeleteTodoItemAsync(Guid id);
        Task<TodoItem> UpdateDescriptionAsync(Guid id, string description);
        Task<TodoItem> UpdateStatusAsync(Guid id, Status status);
        Task<TodoItem> UpdateTitleAsync(Guid id, string title);
    }
}