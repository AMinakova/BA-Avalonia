using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Threading.Tasks;

namespace Todo.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        Window GetWindow() => (Window)this.VisualRoot;
        public void OnMenuClicked(object sender, RoutedEventArgs args)
        {
            var dialog = new OpenFileDialog();
            //var result = await dialog.ShowAsync(this);

            //if (result != null)
            //{
            //    foreach (var path in result)
            //    {
            //        System.Diagnostics.Debug.WriteLine($"Opened: {path}");
            //    }
            //}
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }


    }
}
