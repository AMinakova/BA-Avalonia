using System;

namespace Todo.Models
{

    public class TodoItem
    {
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
