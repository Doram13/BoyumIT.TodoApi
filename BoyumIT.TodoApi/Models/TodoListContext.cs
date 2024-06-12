using Microsoft.EntityFrameworkCore;

namespace BoyumIT.TodoApi.Models
{
    public class TodoListContext : DbContext
    {
        public TodoListContext(DbContextOptions<TodoListContext> options)
        : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;

    }
}
