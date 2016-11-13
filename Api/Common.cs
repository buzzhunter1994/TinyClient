using FirstFloor.ModernUI.Windows.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using TinyClient.UserControls;
using WPFGrowlNotification;

namespace TinyClient.Api
{
    class Common
    {
        public static class Wav
        {
            [DllImport("winmm.dll", SetLastError = true)]
            static extern bool PlaySound(string pszSound, UIntPtr hmod, uint fdwSound);
            public static void Play(string strFileName)
            {
                PlaySound(strFileName, UIntPtr.Zero, (uint)(0x00020000 | 0x0001));
            }
        }
        //public static PlayerWindow MusicPlayer;
        public static ControlAudio MusicPlayer;
        public static MainWindow TinyMainWindow = new MainWindow();
        public static double TopOffset = 5;
        public static double LeftOffset = 300;
        //public static GrowlNotifiactions GrowlNotifiactions1 = new GrowlNotifiactions();
        public static GrowlNotifiactions GrowlNotifiactions = new GrowlNotifiactions { Top = 0,
            Left = true ? 
            SystemParameters.WorkArea.Left + SystemParameters.WorkArea.Width - LeftOffset : 
            SystemParameters.WorkArea.Left + TopOffset };
        public static string ApiVersion = "5.58";

        public FileInfo fi = new FileInfo("msg.wav");
        public FileInfo fi_out = new FileInfo("out.wav");

        public class photoResponse
        {
            public string name { get; set; }
            public string photo { get; set; }
            public int sex { get; set; }
        }
        public class userPhoto
        {
            public List<photoResponse> response { get; set; }
        }

        public static Types.LongPollServer LongPollInfo;
        public static DispatcherTimer _mTimer;
        public static WebClient _mWebClient = new WebClient { Encoding = Encoding.UTF8 };
        Types.LongPoolServerUpdates s = null;
        string urlContents = String.Empty;
        string temp = String.Empty;
        Task<string> getStringTask = null;
        //userPhoto ph = null;
        Types.profile ph = null;
        public static Task<Types.LongPollServer> GetLongPollServer()
        {
            return Common.getResponse<Types.LongPollServer>("messages.getLongPollServer", new string[,] {{ "need_pts", "1" }});
        }
        public async Task LongPool(Types.LongPollServer serv)
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            try
            {
                getStringTask = client.GetStringAsync("http://" + serv.server + "?act=a_check&key=" + serv.key + "&ts=" + serv.ts + "&wait=25&mode=2");
                urlContents = await getStringTask;
                s = await JsonConvert.DeserializeObjectAsync<Types.LongPoolServerUpdates>(urlContents);
                if (s.updates.Count > 0)
                {
                    foreach (var f in s.updates)
                    {

                        switch (f[0].ToString())
                        {
                            case "4":
                                if (f[2].ToString() == "49" || f[2].ToString() == "17" || f[2].ToString() == "16" || f[2].ToString() == "48" || f[2].ToString() == "32" || f[2].ToString() == "33")
                                {
                                    try
                                    {
                                       // ph = await vkUser.Get(f[3].ToString(), Properties.Settings.Default.token);
                                        Wav.Play(fi.FullName);
                                        Common.GrowlNotifiactions.AddNotification(new Notification
                                        {
                                            Title = "Новое сообщение",
                                            //ImageUrl = ph.response[0].photo,
                                            Message = f[6].ToString().Replace("<br>", "\n"),
                                            //User = ph.response[0].name
                                        });
                                        ph = null;
                                    }
                                    catch
                                    {

                                    }
                                }
                                break;
                           
                        }
                    }
                    serv.ts = s.ts;
                    s = null;
                    await LongPool(serv);
                }
                else
                {
                    serv.ts = s.ts;
                    s = null;
                    await LongPool(serv);
                }
            }
            finally
            {
                ph = null;
                urlContents = null;
                s = null;
            }
        }
        //public static ListBox PlayListV;
        public static int[] PhotoSizes = { 0, 75, 130, 604, 807, 1280, 2560 };
        public static Dock[][] AttachmentDock = {
            new Dock[]{ Dock.Left },
            new Dock[]{ Dock.Left, Dock.Right },
            new Dock[]{ Dock.Left, Dock.Top, Dock.Bottom },
            new Dock[]{ Dock.Left, Dock.Top, Dock.Top, Dock.Top },
            new Dock[]{ Dock.Left, Dock.Top, Dock.Top, Dock.Top, Dock.Top },
            new Dock[]{ Dock.Top, Dock.Left, Dock.Left, Dock.Left, Dock.Left, Dock.Left },
            new Dock[]{ Dock.Top, Dock.Left, Dock.Left, Dock.Left, Dock.Left, Dock.Left, Dock.Left },
            new Dock[]{ Dock.Top, Dock.Left, Dock.Left, Dock.Left, Dock.Left, Dock.Left, Dock.Left, Dock.Left },
            new Dock[]{ Dock.Top, Dock.Left, Dock.Left, Dock.Left, Dock.Left, Dock.Left, Dock.Left, Dock.Left, Dock.Left },
            new Dock[]{ Dock.Top, Dock.Left, Dock.Left, Dock.Left, Dock.Left, Dock.Left, Dock.Left, Dock.Left, Dock.Left, Dock.Left }
                                                };
        public static int[][] AttachmentWidthAndHeight = {
            new int[]{ 400, 300 },
            new int[]{ 200, 300, 200, 300 },
            new int[]{ 200, 300, 200, 150, 200, 150 },
            new int[]{ 200, 300, 200, 100, 200, 100, 200, 100 },
            new int[]{ 300, 300, 100, 74, 100, 74, 100, 74, 100, 74 },
            new int[]{ 400, 200, 79, 99, 79, 99, 79, 99, 79, 99, 79, 99 },
            new int[]{ 400, 233, 65, 65, 65, 65, 65, 65, 65, 65, 65, 65, 65, 65 },
            new int[]{ 400, 242, 56, 56, 56, 56, 56, 56, 56, 56, 56, 56, 56, 56, 56, 56 },
            new int[]{ 400, 250, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48 },
            new int[]{ 400, 255, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43 }
                                                         };

        public static string AttachmentToString(string type)
        {
            string t = "";
            switch (type)
            {
                case "sticker":
                    t = "Стикер";
                    break;
                case "photo":
                    t = "Фото";
                    break;
                case "video":
                    t = "Видео";
                    break;
                case "audio":
                    t = "Аудиозапись";
                    break;
                case "doc":
                    t = "Документ";
                    break;
                case "wall":
                    t = "Запись на стене";
                    break;
                case "wall_reply":
                    t = "Комментарий к записи";
                    break;
                case "link":
                    t = "Ссылка";
                    break;
                default:
                    t = "Неизвестно";
                    break;
            }
            return t;
        }
        public static async Task<bool> isValidToken() { 
            WebClient webClient1 = new WebClient();
            webClient1.Encoding = Encoding.UTF8;
            webClient1.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");

            string temp;
            try
            {
                temp = await webClient1.UploadStringTaskAsync("https://api.vk.com/method/account.getProfileInfo", String.Format("&v={0}&access_token={1}", ApiVersion, Properties.Settings.Default.AccessToken)).ConfigureAwait(false);

                JObject a = JObject.Parse(temp);

                if (a["error"] != null)
                {
                    return false;
                }
                else
                {
                    temp = await webClient1.UploadStringTaskAsync("https://api.vk.com/method/stats.trackVisitor", String.Format("&v={0}&access_token={1}", ApiVersion, Properties.Settings.Default.AccessToken)).ConfigureAwait(false);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static async Task<JToken> SendRequest(string method, string parameters = "", bool getRaw = false, string customToken = "", string Lang = "")
        {
            string temp;

        Request:
            WebClient webClient1 = new WebClient();
            webClient1.Encoding = Encoding.UTF8;
            webClient1.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");

            try
            {
                temp = await webClient1.UploadStringTaskAsync("https://api.vk.com/method/" + method, String.Format("{0}&lang={1}&v={2}&access_token={3}", parameters, Lang, ApiVersion, customToken != "" ? customToken : Properties.Settings.Default.AccessToken)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return null;
            }

            JObject a = JObject.Parse(temp);    
        
            if (getRaw)
                return a;
            if (a["error"] != null)
            {
                MessageBox.Show(a["error"].ToString(), "TinyClient", MessageBoxButton.OK);
                return null;
            }
            else
            {
                return a["response"];
            }

        }
        /*
         If Not IsNothing(a("error")) Then
             Dim b = Await MyJsonConvert.DeserializeObjectAsync(Of types.VKError)(a("error").ToString)
              Select Case b.error_code
                  Case 1
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError1 & " (" & method & ")")
                  Case 2
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError2 & " (" & method & ")")
                  Case 3
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError3 & " (" & method & ")")
                  Case 4
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError4 & " (" & method & ")")
                  Case 5
                      Forms.Application.Restart()
                      My.Settings.AccessToken = ""
                      My.Settings.Save()
                      My.Application.Shutdown()
                  Case 6
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError6 & " (" & method & ")")
                  Case 7
                      If(method = "messages.send") Then
                         ModernDialog.ShowMessage(My.Resources.ApiError_messageSend,
                                                   "MicroVK - VKAPI",
                                                  MessageBoxButton.OK)
                      Else
                          If _
                              ModernDialog.ShowMessage(
                                  My.Resources.ApiError7 & " (" & method & ")" & vbNewLine & My.Resources.ApiError7_1,
                                  "MicroVK - VKAPI",
                                  MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                              Forms.Application.Restart()
                              My.Settings.AccessToken = ""
                              My.Settings.Save()
                              My.Application.Shutdown()
                          End If
                      End If
                  Case 8
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError8 & " (" & method & ")")
                  Case 9
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError9 & " (" & method & ")")
                  Case 10
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError10 & " (" & method & ")")
                  Case 11
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError11 & " (" & method & ")")
                  Case 14
                      If Not IsCaptchaShow Then
                          IsCaptchaShow = True
                          Dim stackPanel1 As New StackPanel
                          Dim textCaptcha1 As New TextBox
                          stackPanel1.Children.Add(New Image With {
                                                      .Source =
                                                      New BitmapImage(New Uri(a("error")("captcha_img").ToString)),
                                                      .Stretch = Stretch.Uniform,
                                                      .Width = 130,
                                                      .Height = 50,
                                                      .HorizontalAlignment = HorizontalAlignment.Left})
                          stackPanel1.Children.Add(textCaptcha1)
                          Call New ModernDialog() With {.Title = My.Resources.ApiError14,
                              .Content = stackPanel1
      }.ShowDialog()
                          IsCaptchaShow = False
                          parameters = parameters & "&captcha_sid=" & a("error")("captcha_sid").ToString & "&captcha_key=" &
                                       textCaptcha1.Text
                          GoTo Label1
                      End If
                  Case 15
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError15 & " (" & method & ")")
                  Case 16
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError16 & " (" & method & ")")
                  Case 17
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError17 & " (" & method & ")")
                  Case 20
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError20 & " (" & method & ")")
                  Case 21
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError21 & " (" & method & ")")
                  Case 23
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError23 & " (" & method & ")")
                  Case 100
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError100 & " (" & method & ")")
                  Case 101
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError101 & " (" & method & ")")
                  Case 113
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError113 & " (" & method & ")")
                  Case 150
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError150 & " (" & method & ")")
                  Case 200
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError200 & " (" & method & ")")
                  Case 201
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError201 & " (" & method & ")")
                  Case 203
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError203 & " (" & method & ")")
                  Case 300
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError300 & " (" & method & ")")
                  Case 500
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError500 & " (" & method & ")")
                  Case 600
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError600 & " (" & method & ")")
                  Case 603
                      ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError603 & " (" & method & ")")
                  Case Else
                      MsgMui(
                          "error_code: " & b.error_code & vbNewLine & "error_msg: " & b.error_msg & vbNewLine &
                          "request_params: " & Newtonsoft.Json.JsonConvert.SerializeObject(b.request_params))
              End Select
              Return Nothing
          Else
              Return a("response")
          End If
      End Function*/
        public static async Task<T> getResponse<T>(string method, string[,] args = null)
        {
            string p = "";
            string q = "";
            string path = "";

            if (args != null)
            {
                for (int i = 0; i < args.GetLength(0); i++)
                {
                    if (!String.IsNullOrEmpty(args[i, 1]))
                    {
                        p = String.Format("{0}={1}&", p + args[i, 0], args[i, 1]);
                    }
                    else
                    {
                        p = String.Format("{0}&", p + args[i, 0]);
                    }
                }
            }
            Debug.WriteLine(p);

            if (method.IndexOf('@') > 0)
            {
                q = method.Split('@')[0];
                path = method.Split('@')[1];
            }
            else
            {
                q = method;
            }

            JToken response = await SendRequest(q, p);

            if (response != null && response.ToString() != "")
            {
                if (path == "")
                {
                    return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(response.ToString()));
                }
                else
                {
                    return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(response[path].ToString()));
                }
            }
            else
            {
                return default(T);
            }
        }
        public static void ShowNotify(List<Notification> a, bool isIgnoreSetting)
        {
            DateTime d = DateTime.Now;
            // Warning!!! Optional parameters not supported
            /* if (IsBellOff) {
                 return;
             }
             else if (IsBellSleep) {
                 if ((BellSleepTime > Now)) {
                     return;
                 }
                 else {
                     MyWindow1.BeelChangeStatus(0);
                 }
            
             }
             */
            foreach (Notification i in a)
            {
                i.Index = GrowlNotifiactions.IndexCounter;
                GrowlNotifiactions.IndexCounter++;
                if ((GrowlNotifiactions.IndexCounter == int.MaxValue))
                {
                    GrowlNotifiactions.IndexCounter = 0;
                }

                i.date = d.ToString("HH:mm:ss dd.MM.yy");
                GrowlNotifiactions.AddNotification(i);
                bool isShow = true; //(bool.Parse(My.Settings.PropertyValues(("Notification" + i.type)).PropertyValue) || isIgnoreSetting);
                UserControl content = null;
                if (isShow)
                {
                    switch (i.type)
                    {
                        case 3:
                            //content = new ControlNotificationMessageDeleteFlags();
                            break;
                        case 4:
                        case 40:
                           // SoundPlayer1.Stop();
                           // SoundPlayer1.Play();
                            /*  object tempBool = true;
                              foreach (w in My.Application.Windows.OfType(Of, Window)) {
                                  if (w) {
                                      IsNot;
                                      (null && w.IsActive);
                                      tempBool = false;
                                      break;
                                  }
                            
                              }
                        
                              if (tempBool) {
                                  MyWindow1.TaskbarItemInfo.ProgressValue = 100;
                                  TrayIconManager.SetUnreadTaskBarEvent((TrayIconManager.UnreadLocalEvent + 1));
                              }
                              */
                            content = new NotificationMessage();
                            break;
                        case 41:
                            // content = new ControlMessageSendSuccessfully();
                            break;
                        case 8:
                            //content = new ControlNotificationFriendsOnline();
                            break;
                        case 9:
                            // content = new ControlNotificationFriendsOnline();
                            break;
                        case 61:/*
                        foreach (j in GrowlNotifiactions1.Notifications) {
                            if ((j.Content.GetType() == ControlNotificationFriendsOnline)) {
                                object cfo = ((types.Notifycation)(((ControlNotificationFriendsOnline)(j.Content)).DataContext));
                                if (((cfo.user_id == i.user_id) 
                                            && (cfo.type == 61))) {
                                    GrowlNotifiactions1.Notifications.Remove(j);
                                    break;
                                }
                                
                            }
                            
                        }
                        
                        content = new ControlNotificationFriendsOnline();*/
                            break;
                        case 62:
                            /* foreach (j in GrowlNotifiactions1.Notifications) {
                                 if ((j.Content.GetType() == ControlNotificationFriendsOnline)) {
                                     object cfo = ((types.Notifycation)(((ControlNotificationFriendsOnline)(j.Content)).DataContext));
                                     if ((cfo.user_id == i.user_id)) {
                                         GrowlNotifiactions1.Notifications.Remove(j);
                                         break;
                                     }
                                
                                 }
                            
                             }
                        
                             content = new ControlNotificationFriendsOnline();*/
                            break;
                    }
                    if (content != null && isShow)
                    {
                        content.DataContext = i;
                        GrowlNotifiactions.AddNotification(new Notification { Content = content, type = i.type });
                    }
                }
            }
        }
    }
}
