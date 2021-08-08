using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Todo.Models;

namespace Todo.ViewModels
{
    public class TodoListViewModel : ViewModelBase
    {
        public ObservableCollection<TodoItem> Items { get; private set; } = new ObservableCollection<TodoItem>();

        public TodoItem SelectedItem { get; set; }

        public void SetItems(IEnumerable<TodoItem> replacement)
        {
            Items = new ObservableCollection<TodoItem>(replacement);
            this.RaisePropertyChanged(nameof(Items));
        }

        public void DeleteSelected()
        {
            Items.Remove(SelectedItem);
        }
    }
}
