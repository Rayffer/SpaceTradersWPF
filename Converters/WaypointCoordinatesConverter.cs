using System;
using System.Globalization;
using System.Windows.Data;

using SpaceTradersWPF.ApiModels;

namespace SpaceTradersWPF.Converters;

internal class WaypointCoordinatesConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Waypoint waypoint)
        {
            return $"X: {waypoint.X}, Y: {waypoint.Y}";
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
