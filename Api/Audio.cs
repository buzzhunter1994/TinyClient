using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace TinyClient.Api
{
    class Audio
    {
        public static async Task<ObservableCollection<Types.audio>> Get(string offset = "")
        {
            return await Common.getResponse<ObservableCollection<Types.audio>>("audio.get@items", new string[,] { { "offset", offset }, { "count", "100" } });
        }
        public static async Task<ObservableCollection<Types.audio>> GetPopular(string genre_id = "", string only_eng = "", string offset = "")
        {
            return await Common.getResponse<ObservableCollection<Types.audio>>("audio.getPopular", new string[,] { { "genre_id", genre_id }, { "only_eng", only_eng }, { "offset", offset } });
        }
        public static async Task<ObservableCollection<Types.audio>> GetRecommendations(string target_audio = "", string shuffle = "",string offset = "")
        {
            return await Common.getResponse<ObservableCollection<Types.audio>>("audio.getRecommendations@items", new string[,] { { "offset", offset }, { "target_audio", target_audio }, { "shuffle", shuffle } });
        }
        public static async Task<ObservableCollection<Types.audio>> Add(string audioId, string ownerId)
        {
            return await Common.getResponse<ObservableCollection<Types.audio>>("audio.add", new string[,] { { "audio_id", audioId }, { "owner_id", ownerId } });
        }
        public static async Task<ObservableCollection<Types.audio>> SetBroadcast(string audio)
        {
            return await Common.getResponse<ObservableCollection<Types.audio>>("audio.setBroadcast", new string[,] { { "audio", audio } });
        }
        public static async Task<ObservableCollection<Types.audio>> Search(string q, string offset = "")
        {
            return await Common.getResponse<ObservableCollection<Types.audio>>("audio.search@items", new string[,] { { "q", q }, { "auto_complete", "1" }, { "offset", offset } });
        }
        public static async Task<string> getLyrics(string lyrics_id)
        {
            JToken a = await Common.getResponse<JToken>("audio.getLyrics", new string[,] { { "lyrics_id", lyrics_id } });
            if (a == null)
            {
                return "";
            }
            else
            {
                return a["text"].ToString();
            }
        }
    }
}