using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Orchid.Converters
{
    // Converter to transform boolean values to colors
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue && parameter is string colors)
            {
                var colorParts = colors.Split(',');
                if (colorParts.Length == 2)
                {
                    return boolValue
                        ? Color.FromArgb(colorParts[0])
                        : Color.FromArgb(colorParts[1]);
                }
            }

            return Colors.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // Converter to invert boolean values
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }

            return false;
        }
    }

    // Converter to transform payment status to appropriate colors
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                if (status.Contains("successfully"))
                    return Colors.Green;
                if (status.Contains("Processing"))
                    return Colors.Blue;
                if (status.Contains("Invalid") || status.Contains("failed"))
                    return Colors.Red;
            }

            return Colors.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
