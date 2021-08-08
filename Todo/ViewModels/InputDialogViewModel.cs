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

        public async Task Save(Window dialogWindow)
        {
            var desktop = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
            MainWindowViewModel mainVM = (MainWindowViewModel)desktop.MainWindow.DataContext;

            await mainVM.SaveFile(ListName);

            if (dialogWindow != null)
            {
                dialogWindow.Close();
            }
        }
    }
}
