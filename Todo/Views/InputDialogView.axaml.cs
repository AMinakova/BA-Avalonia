using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
namespace Todo.Views
{
    public partial class InputDialogView : Window
    {
        public InputDialogView()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
