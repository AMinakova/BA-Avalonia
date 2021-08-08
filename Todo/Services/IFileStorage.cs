using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services
{
    public interface IFileStorage
    {
        public IList<TodoItem> Load(string filePath);
        public void Save(string filePath, IEnumerable<TodoItem> content);
    }
}
