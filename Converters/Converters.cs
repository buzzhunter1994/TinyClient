using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TinyClient
{
    public class MyVisibilityConverter : IValueConverter
    {       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility result;
            if (value != null)
            {
                result = Visibility.Collapsed;
            }
            else
            {
                if (value.ToString().Length == 0)
                {
                    result = Visibility.Collapsed;
                }
                else
                {
                    if (value is int && (int)value > 0)
                    {
                        result = Visibility.Collapsed;
                    }
                    else
                    {
                        if (value is bool && (bool)value == false)
                        {
                            result = Visibility.Collapsed;
                        }
                        else
                        {
                            result = Visibility.Visible;
                        }
                    }
                }
            }
            if (parameter != null && parameter.ToString() == "inverse")
                if (result == Visibility.Visible)
                {
                    result = Visibility.Collapsed;
                }
                else
                {
                    result = Visibility.Visible;
                }
            return result;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
