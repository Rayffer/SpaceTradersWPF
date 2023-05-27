using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

using SpaceTradersWPF.ApiModels;

namespace SpaceTradersWPF.Converters;

internal class SystemFactionCollectionToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SystemFaction[] systemFactions && 
            systemFactions.Length > 0)
        {
            return string.Join(", ", systemFactions.Select(x => x.Symbol));
        }
        return "None";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
