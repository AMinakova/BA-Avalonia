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

            var firstInput = this.FindControl<TextBox>("ListNameInput");
            if (firstInput != null)
            {
                firstInput.AttachedToVisualTree += (s, e) => firstInput.Focus();
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
