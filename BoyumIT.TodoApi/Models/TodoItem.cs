using BoyumIT.TodoApi.Models.Enums;
using BoyumIT.TodoApi.Validation;
using System.ComponentModel.DataAnnotations;

namespace BoyumIT.TodoApi.Models
{
    public class TodoItem
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdateTime { get; set; }
        [ValidEnum]
        public Status Status { get; set; }
    }
}
