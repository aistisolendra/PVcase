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

        private void ValidateOnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _textParser.IsTextInt(e.Text);
        }

        private void LimitTiltAngle(object sender, TextCompositionEventArgs e)
        {
            string fullText = $"{((TextBox) sender).Text}{e.Text}";

            e.Handled = _textParser.IsTextInt(e.Text);
            e.Handled = !_textParser.IsInAngleLimit(fullText);
        }
    }
}
