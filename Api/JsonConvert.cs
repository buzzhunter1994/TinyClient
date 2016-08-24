using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyClient.Api
{
    class MyJsonConvert
    {
      /*  public static async Task<T> DeserializeObjectAsync<T>(string value, JsonSerializerSettings settings)
        {
            return await Task<T>.Factory.StartNew(() => JsonConvert.DeserializeObjectAsync<T>(value, settings));
        }
        public static async Task<T> DeserializeObjectAsync<T>(string value)
        {
            return await DeserializeObjectAsync<T>(value, null);
        }*/
        public static async Task<Object> DeserializeObjectAsync(string value, JsonSerializerSettings settings)
        {
            return await DeserializeObjectAsync(value, null, null);
        }
        public static async Task<Object> DeserializeObjectAsync(string value, Type type, JsonSerializerSettings settings)
        {
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject(value, type, settings));
        }
        public static async Task<String> SerializeObjectAsync(Object value)
        {
            return await Task.Factory.StartNew(() => JsonConvert.SerializeObject(value, Formatting.None));
        }
    }
}
