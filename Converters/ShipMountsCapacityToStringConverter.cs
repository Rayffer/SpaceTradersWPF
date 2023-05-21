﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ApiModels.Requests;

namespace SpaceTradersWPF.Converters;

internal class ShipMountsCapacityToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Ship ship)
        {
            return "";
        }
        return $"Mounts {ship.Mounts.Length}/{ship.Frame.MountingPoints}";
        throw new NotImplementedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
