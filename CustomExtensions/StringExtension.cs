using System;

namespace TinyClient.CustomExtensions
{
    public static class StringExtension
    {
        public static string GetParametr(this String str, string name)
        {
            if (String.IsNullOrEmpty(str))
                return "";
            char s = str.IndexOf("&", StringComparison.Ordinal) >= 0 ? '&' : ',';
            if (String.Compare(str, name + "=") > 0)
            {
                string a = str.Split(new string[] { name + "=" }, StringSplitOptions.None)[1];
                return a.Split(s)[0];
            }
            else
            {
                return "";
            }
        }
    }
}
