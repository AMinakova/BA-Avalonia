using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
//using System.Windows.Input;

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

        public async Task Save()
        {
            List<FileDialogFilter> filters = new List<FileDialogFilter>
            {
                new FileDialogFilter
                {
                    Name = ".csv Files", Extensions = new List<string> {"csv"}
                },
            };

            var dialog = new SaveFileDialog()
            {
                Title = "Speícher .csv Datei",
                Filters = filters,
                InitialFileName = ListName
            };
            IClassicDesktopStyleApplicationLifetime desktop = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
            await dialog.ShowAsync(desktop.MainWindow);
        }
    }
}
