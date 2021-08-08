using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.ViewModels
{
    public class TodoListViewModel : ViewModelBase
    {
        public ObservableCollection<TodoItem> Items { get; } = new ObservableCollection<TodoItem>();

        public void SetItems(IEnumerable<TodoItem> replacement)
        {
            Items.Clear();
            foreach(var item in replacement ?? throw new ArgumentNullException(nameof(replacement)))
            {
                Items.Add(item);
            }
        }
    }
}
