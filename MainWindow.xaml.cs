using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using TinyClient.Api;
using TinyClient.UserControls;
using TinyClient.CustomExtensions;
using WPFGrowlNotification;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace TinyClient
{
    public partial class MainWindow
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern AsyncKeyState GetAsyncKeyState(Keys key);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public enum AsyncKeyState : short
        {
            CurrentlyPressed = short.MinValue,
            HasNotBeenPressed = 0,
            HasBeenPressed
        }

        public NotifyIcon m_notifyIcon;
        public System.Windows.Forms.ContextMenu contextMenu;
        public System.Windows.Forms.MenuItem menuItem1;
        public HwndSource source;
        private IntPtr SourceHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            /*switch (msg)
            {
                case 0x0047: 
                      switch (wParam.ToInt32() & 0xfff0)
                      {
                          case 0xF010: 
                              this.ResizeMode = System.Windows.ResizeMode.NoResize;
                             break;
                          default:
                              
                              break;
                      }
                      break;
            }
            */
            return IntPtr.Zero;
        }

        public MainWindow(){
            SourceInitialized += delegate
            {
                source = HwndSource.FromVisual(this) as HwndSource;
                source.AddHook(SourceHook);
            };
            m_notifyIcon = new System.Windows.Forms.NotifyIcon();
            contextMenu = new System.Windows.Forms.ContextMenu();
            menuItem1 = new System.Windows.Forms.MenuItem();
            menuItem1.Index = 0;
            menuItem1.Text = "Выход";
            menuItem1.Click += new EventHandler(menuItem1_Click);
            contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem1 });
            m_notifyIcon.Visible = true;
            m_notifyIcon.Text = "TinyClient";
            m_notifyIcon.BalloonTipTitle = "TinyClient";
            m_notifyIcon.BalloonTipIcon = ToolTipIcon.None;
            m_notifyIcon.BalloonTipText = "Все еще запущен в фоновом режиме";
            m_notifyIcon.Icon = Properties.Resources.icon16;
            m_notifyIcon.Click += notifyIconClick;
            m_notifyIcon.ContextMenu = contextMenu;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                //this.Hide();
                //if (m_notifyIcon != null) m_notifyIcon.ShowBalloonTip(2000);
                //ShowInTaskbar = false;
            }
            else
            {
                //ShowInTaskbar = true;
            }
        }
        private void menuItem1_Click(object Sender, EventArgs e)
        {
            this.Close();
        }

        private void notifyIconClick(object sender, EventArgs e)
        {
            //if (WindowState == WindowState.Minimized)
            //{
                this.Show();
                SetForegroundWindow(source.Handle);
                WindowState = WindowState.Normal;

            //}
            /*
            else
            {
                WindowState = WindowState.Minimized;
            }*/
        }

        private void AudioLoad(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.lastSection = "Pages/PageAudio.xaml#page=playlist";
            Properties.Settings.Default.Save();
            MainFrame.Source = new Uri(Properties.Settings.Default.lastSection, UriKind.Relative);
        }

        private void FriendsLoad(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.lastSection = "Pages/PageFriends.xaml#page=friends";
            Properties.Settings.Default.Save();
            MainFrame.Source = new Uri(Properties.Settings.Default.lastSection, UriKind.Relative);
        }

        private void BookmarksLoad(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.lastSection = "Pages/PageBookmarks.xaml#page=photos";
            Properties.Settings.Default.Save();
            MainFrame.Source = new Uri(Properties.Settings.Default.lastSection, UriKind.Relative);
        }

        private void SettingsLoad(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.lastSection = "Pages/PageSettings.xaml";
            Properties.Settings.Default.Save();
            MainFrame.Source = new Uri(Properties.Settings.Default.lastSection, UriKind.Relative);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Common._mTimer == null)
            {
                Common._mTimer = new DispatcherTimer();
                Common._mTimer.Interval = new TimeSpan(0, 0, 25);
                Common._mTimer.Tick += m_timer_Tick;
                Common._mTimer.Start();
                Common._mWebClient.DownloadStringCompleted += _mWebClient_DownloadStringCompleted;
                m_timer_Tick(null, null);
            }
            //Common.GrowlNotifiactions.AddNotification(new Notification { Content = new NotificationMessage { DataContext = new Types.Notifycation { title = "MicroVK", text = "Invisible_enabled" } } });
        }
        private static readonly string _connectRawString = "http://{0}?act=a_check&key={1}&ts={2}&wait=25&mode=66";
        private static string _connectString;
        private async void m_timer_Tick(object sender, EventArgs e)
        {
            if (Common.LongPollInfo == null)
            {
                Common.LongPollInfo = await Common.GetLongPollServer();
                ForceDownloadStringAsync(Common.LongPollInfo.ts);
            }
            if (Common.LongPollInfo != null)
            {
                ForceDownloadStringAsync(Common.LongPollInfo.ts);
                _connectString = string.Format(_connectRawString, Common.LongPollInfo.server, Common.LongPollInfo.key, Common.LongPollInfo.ts);
                TimeSpan diffResult = _lastResponseTime - DateTime.Now;
                if (diffResult.Seconds > 40)
                {
                    Common.LongPollInfo = await Common.GetLongPollServer();
                    Common._mWebClient.CancelAsync();
                    ForceDownloadStringAsync(Common.LongPollInfo.ts);
                }
            }
        }
        List<Types.profile> ph = null;
        
        string temp = String.Empty;
        public FileInfo fi = new FileInfo("msg.wav");
        public FileInfo fi_out = new FileInfo("out.wav");
        private static System.DateTime _lastResponseTime;
        public async void _mWebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null && !e.Cancelled)
            {
                _lastResponseTime = DateTime.Now;
                Console.WriteLine(e.Result.ToString().Trim());
                //await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Types.LongPoolServerUpdates>(e.Result.ToString().Trim()));
                //Types.LongPoolServerUpdates s = await JsonConvert.DeserializeObjectAsync<Types.LongPoolServerUpdates>(e.Result.ToString().Trim());
                Types.LongPoolServerUpdates s = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Types.LongPoolServerUpdates>(e.Result.ToString().Trim()));
                //Console.WriteLine(s);
                if (s.failed != 0)
                {
                    switch (s.failed)
                    {
                        case 1:
                            ForceDownloadStringAsync(s.ts);
                            break;
                        case 2:
                        case 3:
                            Common.LongPollInfo = await Common.GetLongPollServer();
                            ForceDownloadStringAsync(Common.LongPollInfo.ts);                            
                            break;
                    }
                    return;
                }
                else
                {
                    if (s.updates != null && s.updates.Count > 0)
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
                                            ph = await Profile.GetInfo(f[3].ToString());
                                            TinyClient.Api.Common.Wav.Play(fi.FullName);
                                            Common.GrowlNotifiactions.AddNotification(new Notification
                                            {
                                                Title = "Новое сообщение",
                                                ImageUrl = ph[0].photo_50,
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
                                /* case "9":

                                     //ph = await vkUser.Get(f[1].ToString().Substring(1), Properties.Settings.Default.token); //Обрезка -({id})
                                     switch (ph.response[0].sex)
                                     {
                                         case 1: temp = "вышла"; break;
                                         case 2: temp = "вышел"; break;
                                         case 0: temp = "вышло"; break;
                                     }
                                     Common.GrowlNotifiactions.AddNotification(new Notification
                                     {
                                         Title = "Уведомление",
                                     //    ImageUrl = ph.response[0].photo,
                                         Message = temp + " из сети",
                                         // User = ph.response[0].name
                                     });
                                     temp = null;
                                     ph = null;
                                     break;*/
                                /* case "8":
                                     //ph = await vkUser.Get(f[1].ToString().Substring(1), Properties.Settings.Default.token);
                                     switch (ph.response[0].sex)
                                     {
                                         case 1: temp = "появилась"; break;
                                         case 2: temp = "появился"; break;
                                         case 0: temp = "появилось"; break;
                                     }
                                     Common.GrowlNotifiactions.AddNotification(new Notification
                                     {
                                         Title = "Уведомление",
                                       //  ImageUrl = ph.response[0].photo,
                                         Message = temp + " в сети",
                                         //  User = ph.response[0].name
                                     });
                                     temp = null;
                                     ph = null;
                                     break;*/
                                /*   case "61":
                                       // ph = await vkUser.Get(f[1].ToString(), Properties.Settings.Default.token);
                                       Common.GrowlNotifiactions.AddNotification(new Notification
                                       {
                                           Title = "Уведомление",
                                       //    ImageUrl = ph.response[0].photo,
                                           Message = "сейчас набирает сообщение",
                                           // User = ph.response[0].name
                                       });
                                       ph = null;
                                       break;
                                   case "62":
                                       // ph = await vkUser.Get(f[1].ToString(), Properties.Settings.Default.token);
                                       Common.GrowlNotifiactions.AddNotification(new Notification
                                       {
                                           Title = "Уведомление",
                                         //  ImageUrl = ph.response[0].photo,
                                           Message = "сейчас набирает сообщение",
                                           //     User = ph.response[0].name
                                       });
                                       ph = null;
                                       break;*/
                            }
                        }
                        ForceDownloadStringAsync(s.ts);
                    }
                }
                //m_timer_Tick(null, null);
            }
            else
            {
                //TrayIconManager.ChangeNetworkStatus(true, e.Error != null ? e.Error.Message : ");
            }
        }

        private void ForceDownloadStringAsync(string ts)
        {
            if (!Common._mWebClient.IsBusy)
            {
                _connectString = string.Format(_connectRawString, Common.LongPollInfo.server, Common.LongPollInfo.key, ts);
                Common._mWebClient.DownloadStringAsync(new Uri(_connectString));
            }
        }
        
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (m_notifyIcon != null)
                m_notifyIcon.Dispose();
            if (Common.GrowlNotifiactions != null)
                Common.GrowlNotifiactions.Close();
        }

        private void Timeline_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Common.MusicPlayer.Position = (long)Timeline.Value;
        }

        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Common.MusicPlayer != null) 
                Common.MusicPlayer.Volume = (float)e.NewValue;
        }

        private void ShuffleToggle_Click(object sender, RoutedEventArgs e)
        {
            if (Common.MusicPlayer != null)
            {
                Common.MusicPlayer.Playlist = Common.MusicPlayer.Playlist.Shuffle();
            }
        }

        private void Volume_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void RepeatToggle_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    
    }
}
