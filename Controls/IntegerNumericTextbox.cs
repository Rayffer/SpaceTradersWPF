using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SpaceTradersWPF.Controls;

public partial class IntegerNumericTextbox : TextBox
{
    // Create the NumericValue dependency property
    public static readonly DependencyProperty NumericValueProperty =
        DependencyProperty.Register("NumericValue", typeof(int), typeof(IntegerNumericTextbox), new PropertyMetadata(0, OnNumericValueChanged));

    private static readonly Regex _regex = IntegerNumberRegex();

    public int NumericValue
    {
        get { return (int)this.GetValue(NumericValueProperty); }
        set { this.SetValue(NumericValueProperty, value); }
    }

    private static void OnNumericValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        var numericTextBox = sender as IntegerNumericTextbox;
        numericTextBox.Text = numericTextBox.NumericValue.ToString();
    }

    public IntegerNumericTextbox()
    {
        PreviewTextInput += this.NumericTextBox_PreviewTextInput;
    }

    private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        // Validate the input to ensure it's numeric
        if (sender is IntegerNumericTextbox integerNumericTextbox &&
            !_regex.IsMatch(integerNumericTextbox.Text.Insert(integerNumericTextbox.CaretIndex, e.Text)))
        {
            e.Handled = true;
            return;
        }
    }

    // Update the NumericValue property when the text changes
    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        base.OnTextChanged(e);
        if (int.TryParse(this.Text, out int numericValue))
        {
            this.NumericValue = numericValue;
        }
    }

    [GeneratedRegex("^[\\+\\-]{0,1}[0-9]{0,}$")]
    private static partial Regex IntegerNumberRegex();
}
