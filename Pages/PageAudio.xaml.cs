using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System.Windows;
using System;
using System.Windows.Controls;
using TinyClient.Api;

partial class PageAudio : IContent
{
    private void AudioContent_Switch(object sender, RoutedEventArgs e)
    {
        Common.TinyMainWindow.ShuffleToggle.IsChecked = false;
        Genres.SelectedIndex = 0;
        SearchBox.Text = "";
        ModernFrame1.Source = new Uri(((Button)sender).Tag.ToString(), UriKind.Relative);        
    }
    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        Common.TinyMainWindow.ShuffleToggle.IsChecked = false;
        Genres.SelectedIndex = 0;
        ModernFrame1.Source = new Uri("/Content/ControlAudio.xaml#page=search&q=" + SearchBox.Text, UriKind.Relative);
    }
    private void Genres_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Common.TinyMainWindow.ShuffleToggle.IsChecked = false;
        SearchBox.Text = "";
        ModernFrame1.Source = new Uri(((ComboBoxItem)Genres.SelectedItem).Tag.ToString(), UriKind.Relative);
    }

    public void OnFragmentNavigation(FragmentNavigationEventArgs e)
    {
        Common.TinyMainWindow.ShuffleToggle.IsChecked = false;
        ModernFrame1.Source = new Uri("/Content/ControlAudio.xaml#" + e.Fragment, UriKind.Relative);
    }
    public void OnNavigatedFrom(NavigationEventArgs e) { }
    public void OnNavigatedTo(NavigationEventArgs e) { }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e) { }
}