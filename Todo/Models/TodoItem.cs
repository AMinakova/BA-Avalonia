﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models
{

    public class TodoItem
    {
        public string? Description { get; set; } = string.Empty;
        public bool IsChecked { get; set; }
        public string? Date { get; set; }
    }
}
