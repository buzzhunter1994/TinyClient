using FirstFloor.ModernUI.Presentation;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using TinyClient.Api;

namespace TinyClient
{
    class ContentViewModel : NotifyPropertyChanged
    {

        Uri _page;
        public Uri Page
        {
            get { return _page; }
            set
            {
                if (value != _page)
                {
                    _page = value;
                    OnPropertyChanged("Page");
                }
            }

        }
    }
    public partial class MainWindow
    {
        ContentViewModel contentData = new ContentViewModel();
        public MainWindow()
        {
            this.DataContext = contentData;
        }
        public void AudioLoad(object sender, RoutedEventArgs e)
        {
            contentData.Page = new Uri("Pages/PageAudio.xaml#page=audio", UriKind.Relative);
        }

        private void FriendsLoad(object sender, RoutedEventArgs e)
        {
            contentData.Page = new Uri("Pages/PageFriends.xaml#page=friends", UriKind.Relative);
        }

        private void BookmarksLoad(object sender, RoutedEventArgs e)
        {
            contentData.Page = new Uri("Pages/PageBookmarks.xaml#page=photos", UriKind.Relative);
        }

        private void SettingsLoad(object sender, RoutedEventArgs e)
        {
            contentData.Page = new Uri("Pages/PageSettings.xaml", UriKind.Relative);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(100);
            contentData.Page = new Uri("Pages/PageAudio.xaml#page=audio", UriKind.Relative);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Common.MusicPlayer.Close();
        }
    }
}
