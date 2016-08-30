using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using TinyClient.Api;

namespace TinyClient
{
    public partial class MainWindow
    {
        public MainWindow(){

        }

        private async void AudioLoad(object sender, RoutedEventArgs e)
        {

            await Task.Delay(100);
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
            MainFrame.Source = new Uri("Pages/PageAudio.xaml#page=audio", UriKind.Relative);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (Common.MusicPlayer != null)
                Common.MusicPlayer.Dispose();
            if (Common.GrowlNotifiactions1 != null)
                Common.GrowlNotifiactions1.Close();
        }

        private void Timeline_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Common.MusicPlayer.TimelineChange(Timeline.Value);
        }

        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Properties.Settings.Default.Save();
        }       
    }
}
