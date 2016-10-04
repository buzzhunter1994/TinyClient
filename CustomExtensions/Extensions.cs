using System;
using System.Web;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace TinyClient.CustomExtensions
{
    public static class Extensions
    {
        public static ObservableCollection<T> Shuffle<T>(this ObservableCollection<T> input)
        {
            Random rand = new Random(15687);

            var n = input.Count;
            while (n > 1)
            {
                var k = rand.Next(n); 
                n--;
                var value = input[k]; 
                input[k] = input[n]; 
                input[n] = value;
            }
            return input;
        }
        public static string GetParametr(this String str, string name)
        {
            NameValueCollection nvc = new NameValueCollection();

            try
            {
                nvc = HttpUtility.ParseQueryString(str);
                return nvc[name];
            }
            catch
            {
                return "";
            }
            
            /*
            if (String.IsNullOrEmpty(str))
                return "";
            char s = str.IndexOf("&", StringComparison.Ordinal) >= 0 ? '&' : ',';
            if (String.Compare(str, name + "=") > 0)
            {
                try
                {
                    string a = str.Split(new string[] { name + "=" }, StringSplitOptions.None)[1];
                    return a.Split(s)[0];
                }
                catch
                {
                    return "";
                }
            }
            else
            {
                return "";
            }*/
        }
    }
}
