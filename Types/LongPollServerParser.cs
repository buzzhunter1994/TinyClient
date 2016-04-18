using System.Net;
using System.Text;
using System.Windows.Threading;

namespace Types
{
    internal class LongPollServerParser
    {
        public static int MaxMsgId;
        public static Types.LongPollServer LongPollInfo;
        private static readonly string _connectRawString = "http://{0}?act=a_check&key={1}&ts={2}&wait=25&mode=66";
        private static string _connectString;
        private static DispatcherTimer _mTimer;
        private static WebClient _mWebClient = new WebClient { Encoding = Encoding.UTF8 };


    }
}