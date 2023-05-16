using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SpaceTradersWPF.Converters
{
    internal class ElementWidthToFontSizeConverter : IValueConverter
    {
        public static readonly DependencyProperty SmallWidthFontSizeProperty =
            DependencyProperty.Register(nameof(SmallWidthFontSize), typeof(double), typeof(ElementWidthToFontSizeConverter));

        public static readonly DependencyProperty NormalWidthFontSizeProperty =
            DependencyProperty.Register(nameof(NormalWidthFontSize), typeof(double), typeof(ElementWidthToFontSizeConverter));

        public static readonly DependencyProperty LargeWidthFontSizeProperty =
            DependencyProperty.Register(nameof(LargeWidthFontSize), typeof(double), typeof(ElementWidthToFontSizeConverter));

        public double SmallWidthFontSize { get; set; }
        public double NormalWidthFontSize { get; set; }
        public double LargeWidthFontSize { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double elementWidth)
            {
                return elementWidth switch
                {
                    < 500 => SmallWidthFontSize,
                    < 900 => NormalWidthFontSize,
                    _ => LargeWidthFontSize
                };
            }
            return 10;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}