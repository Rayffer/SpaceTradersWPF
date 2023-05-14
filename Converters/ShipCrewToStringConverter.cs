using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using SpaceTradersWPF.Models;

namespace SpaceTradersWPF.Converters;

internal class ShipCrewToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ShipCrew shipCrew)
        {
            return $"{shipCrew.Current}/{shipCrew.Capacity} (Max)";
        }
        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}