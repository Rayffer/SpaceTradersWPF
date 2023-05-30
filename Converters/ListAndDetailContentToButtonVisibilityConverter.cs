using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SpaceTradersWPF.Converters;

internal class ListAndDetailContentToButtonVisibilityConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length == 2 &&
            values[0] is bool listHasContent &&
            !listHasContent &&
            values[1] is bool detailHasContent &&
            detailHasContent)
        {
            return Visibility.Visible;
        }
        return Visibility.Collapsed;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
