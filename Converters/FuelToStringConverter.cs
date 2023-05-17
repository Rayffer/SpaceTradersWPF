using System;
using System.Globalization;
using System.Windows.Data;

using SpaceTradersWPF.Models;

namespace SpaceTradersWPF.Converters;

internal class FuelToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ShipFuel shipFuel)
        {
            return $"{shipFuel.Current}/{shipFuel.Capacity}";
        }
        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}