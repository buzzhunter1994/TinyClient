using System;
using System.ComponentModel;
using System.Windows;

namespace TinyClient
{
    class ContentViewModel : INotifyPropertyChanged
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
    }
}
