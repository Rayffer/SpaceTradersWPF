using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

using SpaceTradersWPF.Converters;

namespace SpaceTradersWPF.Controls
{
    public partial class IntegerNumericTextbox : TextBox
    {
        public static readonly DependencyPropertyKey IntegerValueProperty =
            DependencyProperty.RegisterReadOnly(nameof(IntegerValue), typeof(double), typeof(IntegerNumericTextbox), new FrameworkPropertyMetadata());

        private static readonly Regex _regex = IntegerNumberRegex();
        public int IntegerValue => int.TryParse(this.Text, out int result) ? result : 0;

        public IntegerNumericTextbox()
        {
            PreviewTextInput += this.NumericTextbox_PreviewTextInput;
        }

        ~IntegerNumericTextbox()
        {
            PreviewTextInput -= this.NumericTextbox_PreviewTextInput;
        }

        private void NumericTextbox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (sender is IntegerNumericTextbox integerNumericTextbox &&
                !_regex.IsMatch(integerNumericTextbox.Text.Insert(integerNumericTextbox.CaretIndex, e.Text)))
            {
                e.Handled = true;
                return;
            }
        }

        [GeneratedRegex("^[\\+\\-]{0,1}[0-9]{0,}$")]
        private static partial Regex IntegerNumberRegex();
    }
}
