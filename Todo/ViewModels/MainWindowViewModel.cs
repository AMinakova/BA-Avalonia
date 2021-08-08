using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Todo.Models;
using Todo.Services;
using Todo.Views;

namespace Todo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ViewModelBase content;
        Database Database;
        InputDialogView Dialog;
        public MainWindowViewModel(Database db)
        {
            //Content = List = new TodoListViewModel(db.GetItems());
            Content = new WelcomeViewModel();
            List = new TodoListViewModel(db.GetItems());
            Dialog = new InputDialogView
            {
                DataContext = new InputDialogViewModel(),
                MinWidth = 250,
                Height = 220,
                MinHeight = 200,
                SizeToContent = SizeToContent.Width,
            };
            Database = new Database(); ;
        }

        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public TodoListViewModel List { get; }

        public void AddItem()
        {
            Content = new TodoListViewModel(Database.GetItems());
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

        public void ExitApp()
        {
            Environment.Exit(0);
        }

        public async Task Open()
        {
            List<FileDialogFilter> filters = new List<FileDialogFilter>
            {
                new FileDialogFilter
                {
                    Name = ".csv Files", Extensions = new List<string> {"csv"}
                },
            };
            var dialog = new OpenFileDialog()
            {
                Title = "Öffnen Sie .csv Datei",
                Filters = filters
            };

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

        public async Task Save()
        {
            var dialog = new SaveFileDialog();
            IClassicDesktopStyleApplicationLifetime desktop = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
            await dialog.ShowAsync(desktop.MainWindow);

        }

        public async Task ShowDialog()
        {
            IClassicDesktopStyleApplicationLifetime desktop = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
            await Dialog.ShowDialog(desktop.MainWindow);
        }

        public void CloseDialog()
        {
            Dialog.Close();
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
