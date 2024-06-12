using BoyumIT.TodoApi.Models.Enums;
using BoyumIT.TodoApi.Validation;

namespace BoyumIT.TodoApi.Models
{
    public class TodoItem
    {
        public virtual Guid Id { get; set; }  
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdateTime { get; set; }
        [ValidEnum]
        public Status Status { get; set; }
    }
}
