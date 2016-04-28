using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace TinyClient.Api
{
    class Profile
    {
        public static async Task<List<Types.profile>> Get(string id = "")
        {
            return await Common.getResponse<List<Types.profile>>("users.get", new string[,] { { "user_ids", id } ,{ "fields", "photo_200,can_write_private_message,education,online,last_seen" } });
        }
     /*   public static async Task<ObservableCollection<Types.audio>> GetPopular(string genre_id = "", bool foreing = false, string offset = "")
        {
            return await Common.getResponse<ObservableCollection<Types.audio>>("audio.getPopular", new string[,] { { "genre_id", genre_id }, { "only_eng", foreing ? "1":"0" }, { "offset", offset } });
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
        }*/
    }
}