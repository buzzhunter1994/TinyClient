using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TinyClient
{
    public class IdToPhotoUrl : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Types.user m = (Types.user)value;
            string result = "https://vk.com/images/camera_100.png";
            
            if ((m == null))
            {
                return result;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    public class DurationToTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int duration = (int)value;
            TimeSpan ts = TimeSpan.FromSeconds(duration);
            return ts.TotalMinutes >= 60 ? ts.ToString("hh\\:mm\\:ss") : ts.ToString("mm\\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    public class StringIsNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String.IsNullOrEmpty((string)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class MyVisibilityConverter : IValueConverter
    {       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility result = default(Visibility);
            if ((value == null))
            {
                result = Visibility.Collapsed;
            }
            else if (value.ToString().Length == 0)
            {
                result = Visibility.Collapsed;
            }
            else if (value is Types.wall_post)
            {
                //Dim p = CType(value, types.wall_post)
                //If Not IsNothing(p.copy_history) Then
                //    If p.copy_history.text.Length = 0 Then
                //        result = Visibility.Collapsed
                //    Else
                //        result = Visibility.Visible
                //    End If
                //Else
                //    result = Visibility.Collapsed
                //End If
            }
            else if (value is int && (int)(value) == 0)
            {
                result = Visibility.Collapsed;
            }
            else if (value is bool && !(bool)(value))
            {
                result = Visibility.Collapsed;
            }
            else {
                result = Visibility.Visible;
            }
            if (parameter != null && parameter.ToString() == "inverse")
            {
                if (result == Visibility.Visible)
                {
                    result = Visibility.Collapsed;
                }
                else {
                    result = Visibility.Visible;
                }
            }
            return result;
            /*
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
            return result;*/
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
