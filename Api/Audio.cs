using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace TinyClient.Api
{
    class Audio
    {
        public static async Task<ObservableCollection<Types.audio>> Get(string offset = "")
        {
            return await Common.getResponse<ObservableCollection<Types.audio>>("audio.get@items", new string[][] { new string[] { "offset", offset }, new string[] { "count", "100" } });
        }
    }
}