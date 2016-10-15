using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Linq;
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
            return Binding.DoNothing;
        }
    }

    [ValueConversion(typeof(int), typeof(string))]
    public class FriendlyTimeDescription : IValueConverter
    {
        public static int plural (int a) {
            if (a % 10 == 1 && a % 100 != 11) {
                return 0;
            } else if (a % 10 >= 2 && a % 10 <= 4 && (a % 100 < 10 || a % 100 >= 20)) {
                return 1;
            } else {
                return 2;
            }
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime time = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(System.Convert.ToDouble(value)),TimeZoneInfo.Local);
            TimeSpan span = DateTime.Now - time;

            if (span.Days < 1)
                return Describe(span);
            else
                return time.ToLongDateString() + " в " + time.ToShortTimeString();
        }
        static readonly string suffix = " назад";
        public static string Describe(TimeSpan span)
        {
            string named = "";
            if (span.Hours > 24)
            {
                return "вчера";
            }
            if (span.Hours > 0)
            {
                switch (plural(span.Hours)) 
                {
                    case 0:
                        named = "час";
                        break;
                    case 1:
                        named = "часа";
                        break;
                    case 2:
                        named = "часов";
                        break;
                }
                return String.Format("{0} {1} {2}", span.Hours, named, suffix);
            }
            if (span.Minutes > 0)
            {
                switch (plural(span.Minutes))
                {
                    case 0:
                        named = "минуту";
                        break;
                    case 1:
                        named = "минуты";
                        break;
                    case 2:
                        named = "минут";
                        break;
                }
                return String.Format("{0} {1} {2}", span.Minutes, named, suffix);
            }
            if (span.Seconds > 5)
                return String.Format("{0} секунд {1}", span.Seconds, suffix);
            if (span.Seconds <= 5)
                return "только что";
            return string.Empty;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
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
            return Binding.DoNothing;
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
            return Binding.DoNothing;
        }
    }
    public class StringIsNullVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String.IsNullOrEmpty((string)value)?Visibility.Collapsed:Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
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
            return Binding.DoNothing;
        }
    }
    public class MultiVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility res = (bool)values[0] && (bool)values[1]? Visibility.Visible : Visibility.Collapsed;
            if (parameter != null && parameter.ToString() == "inverse")
            {
                res = (bool)values[0] && !(bool)values[1] ? Visibility.Visible : Visibility.Collapsed;
            }
                /* if (parameter != null && parameter.ToString() == "inverse")
                 {
                     if (res == Visibility.Visible)
                     {
                         res = Visibility.Collapsed;
                     }
                     else {
                         res = Visibility.Visible;
                     }
                 }*/
                /*Visibility result = default(Visibility);
                bool[] value = new bool[] { false, false };
                if ((values[0] == null))
                {
                    result = Visibility.Collapsed;
                }
                else if (values[0].ToString().Length == 0)
                {
                    result = Visibility.Collapsed;
                }
                else if (values[0] is int && (int)(values[0]) == 0)
                {
                    result = Visibility.Collapsed;
                }
                else if (values[0] is bool && !(bool)(values[0]))
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
                }*/
                return res;
        }
        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
