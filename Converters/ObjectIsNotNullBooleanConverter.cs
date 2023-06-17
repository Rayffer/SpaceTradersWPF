﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace SpaceTradersWPF.Converters;

internal class ObjectIsNotNullBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is not null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
