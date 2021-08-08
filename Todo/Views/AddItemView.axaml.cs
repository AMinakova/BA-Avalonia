using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Todo.Views
{
    public partial class AddItemView : UserControl
    {
        public AddItemView()
        {
            InitializeComponent();

            var firstInput = this.FindControl<TextBox>("TodoDescriptionInput");
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
