using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PVcase.Services;

namespace PVcase.Views
{

    public partial class ShellView : Window
    {
        private TextParser _textParser = new TextParser();
        public ShellView()
        {
            InitializeComponent();
        }

        private void ValidateOnlyDoubleTypeNumbers(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _textParser.IsTextInt(e.Text);
        }
    }
}
