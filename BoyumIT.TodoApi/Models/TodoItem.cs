using BoyumIT.TodoApi.Models.Enums;
using BoyumIT.TodoApi.Validation;

namespace BoyumIT.TodoApi.Models
{
    public class TodoItem
    {
        public virtual Guid Id { get; set; }  
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        [ValidEnum]
        public Status Status { get; set; }
    }
}
