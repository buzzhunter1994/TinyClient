using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using TinyClient.Api;

namespace TinyClient
{
    class ContentViewModel : INotifyPropertyChanged
    {
        string _theme = "Light";
        public string Theme
        {
            get { return _theme; }
            set
            {
                if (value != _theme)
                {
                    _theme = value;
                    OnPropertyChanged("Theme");
                }
            }

        }

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
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
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
            contentData.Page = new Uri("Pages/PageFriends.xaml#page=Friends", UriKind.Relative);
        }
        private void BookmarksLoad(object sender, RoutedEventArgs e)
        {
            contentData.Page = new Uri("Pages/PageBookmarks.xaml#page=photos", UriKind.Relative);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (contentData.Theme == "Light")
                contentData.Theme = "Dark";
            else
                contentData.Theme = "Light";

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
