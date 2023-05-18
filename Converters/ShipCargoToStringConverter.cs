using System;
using System.Globalization;
using System.Windows.Data;

using SpaceTradersWPF.ApiModels;

namespace SpaceTradersWPF.Converters;

internal class ShipCargoToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ShipCargo shipCargo)
        {
            return $"{shipCargo.Units}/{shipCargo.Capacity}";
        }
        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
