using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace TinyClient.Api
{
    class Friends
    {
        public static async Task<ObservableCollection<Types.profile>> Get(string offset = "", string count = "50")
        {
            /*List<Types.user> uList = await Common.getResponse<List<Types.user>>("fave.getUsers@items", new string[,] { { "offset", offset }, { "count", count } });
            string user_ids = "";
            foreach (Types.user user in uList) {
                user_ids += user.id + ",";
            }*/
            return await Common.getResponse<ObservableCollection<Types.profile>>("friends.get@items", new string[,] { { "order", "hints" }, { "fields", "photo_100,can_write_private_message,education,online" } });
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