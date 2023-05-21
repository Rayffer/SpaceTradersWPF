using System;
using System.Globalization;
using System.Windows.Data;

using SpaceTradersWPF.ApiModels;

namespace SpaceTradersWPF.Converters;

internal class ShipCargoCapacityToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Ship ship)
        {
            return "";
        }
        return $"Cargo {ship.Cargo.Units}/{ship.Cargo.Capacity}";
        throw new NotImplementedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
