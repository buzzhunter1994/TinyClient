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
                Common._mTimer.Interval = new TimeSpan(0, 0, 22);
                m_timer_Tick(null, null);
                Common._mTimer.Tick += m_timer_Tick;
                Common._mTimer.Start();
                Common._mWebClient.DownloadStringCompleted += _mWebClient_DownloadStringCompleted;
            }
            //Common.GrowlNotifiactions.AddNotification(new Notification { Content = new NotificationMessage { DataContext = new Types.Notifycation { title = "MicroVK", text = "Invisible_enabled" } } });
        }
        private static readonly string _connectRawString = "http://{0}?act=a_check&key={1}&ts={2}&wait=25&mode=66";
        private static string _connectString;
        private async void m_timer_Tick(object sender, EventArgs e)
        {
            Common.LongPollInfo = await Common.GetLongPollServer();
            if (Common.LongPollInfo == null)
            {
                Common.LongPollInfo = await Common.GetLongPollServer();
            }
            
            if (Common.LongPollInfo != null)
            {
                _connectString = string.Format(_connectRawString, Common.LongPollInfo.server, Common.LongPollInfo.key, Common.LongPollInfo.ts);
                TimeSpan diffResult = _lastResponseTime - DateTime.Now;
                if (diffResult.Seconds > 60)
                {
                    Common._mWebClient.CancelAsync();
                }
                ForceDownloadStringAsync();
            }
        }

        private static System.DateTime _lastResponseTime;
        public void _mWebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null && !e.Cancelled)
            {
                _lastResponseTime = DateTime.Now;
                Console.WriteLine(e.Result.ToString().Trim());
                m_timer_Tick(null, null);
            }
            else
            {
                //TrayIconManager.ChangeNetworkStatus(true, e.Error != null ? e.Error.Message : ");
            }
        }

        private void ForceDownloadStringAsync()
        {
            if (!Common._mWebClient.IsBusy)
            {
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
