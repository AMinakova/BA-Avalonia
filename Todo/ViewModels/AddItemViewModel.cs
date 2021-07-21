using ReactiveUI;
using System.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;
using Todo.Services;

namespace Todo.ViewModels
{
    class AddItemViewModel : ViewModelBase
    {
        string description;

        public AddItemViewModel(Database db)
        {
            var okEnabled = this.WhenAnyValue(
                x => x.Description,
                x => !string.IsNullOrWhiteSpace(x));

            Ok = ReactiveCommand.Create(
                () => db.SaveItem(new TodoItem {
                    Description = Description,
                    Date = DateTime.Now.Date.ToString()
                }),
                okEnabled);
            Cancel = ReactiveCommand.Create(() => { });
        }
        public string Description { 
            get => description; 
            set => this.RaiseAndSetIfChanged(ref description, value);
        }
        public ReactiveCommand<Unit, TodoItem> Ok { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}
