using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System.Windows;
using System;
using System.Windows.Controls;

partial class PageAudio : IContent
{
    private void AudioContent_SelectionChanged(Object sender, SelectionChangedEventArgs e)
    {
        if (AudioContent.SelectedIndex >= 0)
            ModernFrame1.Source = new Uri(((ListBoxItem)AudioContent.SelectedItem).Tag.ToString(), UriKind.RelativeOrAbsolute);
    }
    public void OnFragmentNavigation(FragmentNavigationEventArgs e)
    {
        ModernFrame1.Source = new Uri("/Content/ControlAudio.xaml#"+e.Fragment, UriKind.RelativeOrAbsolute);
    }
    public void OnNavigatedFrom(NavigationEventArgs e){ }
    public void OnNavigatedTo(NavigationEventArgs e){ }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e){ }
}