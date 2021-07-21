using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;
using Todo.Services;

namespace Todo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ViewModelBase content;
        Database Database;

        public MainWindowViewModel(Database db)
        {
            Content = List = new TodoListViewModel(db.GetItems());
            Database = db;
        }

        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public TodoListViewModel List { get; }

        public void AddItem()
        {
            var vm = new AddItemViewModel(Database);
            Observable.Merge(
                vm.Ok,
                vm.Cancel.Select(_ => (TodoItem)null))
                .Take(1)
                .Subscribe(model =>
                {
                    if (model != null)
                    {
                        List.Items.Add(model);
                    }

                    Content = List;
                });

            Content = vm;
        }

        public void ExitApp() { Environment.Exit(0); }
        public async Task Open()
        {
            var dialog = new OpenFileDialog();
            IClassicDesktopStyleApplicationLifetime desktop = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
            var result = await dialog.ShowAsync(desktop.MainWindow);
            

            if (result != null)
            {
                foreach (var path in result)
                {
                    System.Diagnostics.Debug.WriteLine($"Opened: {path}");
                    System.Diagnostics.Debug.WriteLine($"Opened: {result}");
                }
            }
        }

        public void FullScreen()
        {
            IClassicDesktopStyleApplicationLifetime desktop = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
            desktop.MainWindow.WindowState = WindowState.FullScreen;
        }
        public void SmallScreen()
        {
            IClassicDesktopStyleApplicationLifetime desktop = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
            desktop.MainWindow.WindowState = WindowState.Normal;
        }
    }
}
