using System;
using System.Globalization;
using System.Windows.Data;

namespace SpaceTradersWPF.Converters;

internal class ApiFlightModeToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string flightMode)
        {
            return flightMode switch
            {
                "DRIFT" => "Drift",
                "STEALTH" => "Stealth",
                "CRUISE" => "Cruise",
                "BURN" => "Burn",
                _ => "Invalid flight mode"
            };
        }
        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}