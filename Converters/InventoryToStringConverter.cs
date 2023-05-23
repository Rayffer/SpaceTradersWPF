using System;
using System.Globalization;
using System.Windows.Data;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.Extensions;

namespace SpaceTradersWPF.Converters;

internal class InventoryToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Inventory inventory)
        {
            return "";
        }

        return $"{inventory.Units} units of {inventory.Symbol.TradeSymbolsFromApiString()}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
