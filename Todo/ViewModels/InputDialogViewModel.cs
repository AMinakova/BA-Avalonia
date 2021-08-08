using ReactiveUI;
using System;
using System.Threading.Tasks;

namespace Todo.ViewModels
{
    class InputDialogViewModel : ViewModelBase
    {
        string listName;
        public string ListName
        {
            get => listName;
            set => this.RaiseAndSetIfChanged(ref listName, value);
        }

        public Action Close { get; set; }
        public Func<string, Task> Saver { get; set; }

        public async Task Save()
        {
            await Saver?.Invoke(ListName);
            Close?.Invoke();
        }
    }
}
