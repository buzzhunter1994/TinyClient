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
        public static Task<T> DeserializeObjectAsync<T>(string value, JsonSerializerSettings settings)
        {
            return Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(value, settings));
        }
        public static Task<T> DeserializeObjectAsync<T>(string value)
        {
            return JsonConvert.DeserializeObjectAsync<T>(value, null);
        }
        public static Task<Object> DeserializeObjectAsync(string value, JsonSerializerSettings settings)
        {
            return DeserializeObjectAsync(value, null, null);
        }
        public static Task<Object> DeserializeObjectAsync(string value, Type type, JsonSerializerSettings settings)
        {
            return Task.Factory.StartNew(() => JsonConvert.DeserializeObject(value, type, settings));
        }
        public static Task<String> SerializeObjectAsync(Object value)
        {
            return Task.Factory.StartNew(() => JsonConvert.SerializeObject(value, Formatting.None));
        }
    }
}
