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
        protected const string TitleApp = "Todo";

        protected ViewModelBase content;
        protected FileStorage fileStorage;

        public MainWindowViewModel(FileStorage storage)
        {
            fileStorage = storage;

            Content = new WelcomeViewModel();
            List = new TodoListViewModel();
        }

        private string todoListName;
        protected string TodoListName
        {
            get => todoListName;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    WinTitle = $"{TitleApp} : {value}";
                    todoListName = value;
                }
            }
        }

        private string winTitle = TitleApp;
        public string WinTitle
        {
            get => winTitle;
            set => this.RaiseAndSetIfChanged(ref winTitle, value);
        }

        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public TodoListViewModel List { get; }

        public Window AppMainWindow => ((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).MainWindow;

        protected List<FileDialogFilter> DialogFileFilters => new List<FileDialogFilter>
        {
            new FileDialogFilter
            {
                Name = ".csv Files", Extensions = new List<string> {"csv"}
            },
        };

        public void AddItem()
        {
            var vm = new AddItemViewModel();
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

        public async Task OpenFile()
        {
            var dialog = new OpenFileDialog()
            {
                Title = "Öffnen Sie .csv Datei",
                Filters = DialogFileFilters,
                InitialFileName = TodoListName,
            };

            var result = await dialog.ShowAsync(AppMainWindow);

            if (result != null && result.Length != 0)
            {
                // bug workaround: avalonia doesn't rerender lists correctly if many items added at once
                Content = null;
                string path = result[0];
                var loaded = fileStorage.Load(path);
                TodoListName = System.IO.Path.GetFileNameWithoutExtension(path);
                List.SetItems(loaded);
                Content = List;
            }
        }

        public async Task SaveFile(string listName)
        {
            TodoListName = listName;

            var dialog = new SaveFileDialog()
            {
                Title = "Speícher .csv Datei",
                Filters = DialogFileFilters,
                InitialFileName = TodoListName,
            };

            var path = await dialog.ShowAsync(AppMainWindow);

            if (!string.IsNullOrEmpty(path))
            {
                fileStorage.Save(path, List.Items);
            }
        }

        public void ShowDialog()
        {
            InputDialogView promptDialog = new InputDialogView
            {
                MinWidth = 250,
                Height = 220,
                MinHeight = 200,
                SizeToContent = SizeToContent.Width,
            };
            promptDialog.DataContext = new InputDialogViewModel
            {
                ListName = TodoListName,
                Close = promptDialog.Close,
                Saver = SaveFile,
            };

            promptDialog.ShowDialog(AppMainWindow);
        }

        public void FullScreen()
        {
            AppMainWindow.WindowState = WindowState.FullScreen;
        }

        public void SmallScreen()
        {
            AppMainWindow.WindowState = WindowState.Normal;
        }
    }
}
