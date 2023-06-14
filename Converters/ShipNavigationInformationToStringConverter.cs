using System;
using System.Globalization;
using System.Windows.Data;

using SpaceTradersWPF.ApiModels;

namespace SpaceTradersWPF.Converters;

internal class ShipNavigationInformationToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ShipNavigationInformation shipNavigationInformation)
        {
            if (shipNavigationInformation.Status == "DOCKED")
            {
                return "Docked";
            }
            else if (shipNavigationInformation.Status == "IN_TRANSIT")
            {
                return $"In transit to {shipNavigationInformation.Route.Destination.Symbol}, arrival at {shipNavigationInformation.Route.Arrival.Add(TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now))}";
            }
            else if (shipNavigationInformation.Status == "IN_ORBIT")
            {
                return $"In orbit of {shipNavigationInformation.WaypointSymbol}";
            }
        }
        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
