using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using TinyClient.Api;

namespace TinyClient
{
    public partial class MainWindow
    {
        public MainWindow(){ }

        private void AudioLoad(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("Pages/PageAudio.xaml#page=audio", UriKind.Relative);
        }

        private void FriendsLoad(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("Pages/PageFriends.xaml#page=friends", UriKind.Relative);
        }

        private void BookmarksLoad(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("Pages/PageBookmarks.xaml#page=photos", UriKind.Relative);
        }

        private void SettingsLoad(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("Pages/PageSettings.xaml", UriKind.Relative);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(100);
            //MainFrame.Source = new Uri("Pages/PageAudio.xaml#page=audio", UriKind.Relative);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Common.MusicPlayer.Close();
        }
    }
}
