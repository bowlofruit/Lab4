using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Lab_2_3
{
    public class NameToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return new SolidColorBrush((Color)value);
            }
            catch (Exception)
            {
                return DependencyProperty.UnsetValue;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Brush input = value as Brush;
            var converter = new BrushConverter();
            return converter.ConvertToString(input);
        }
    }
}