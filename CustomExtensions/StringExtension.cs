using System;
using System.Web;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace TinyClient.CustomExtensions
{
    public static class StringExtension
    {
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
