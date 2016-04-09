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
            ModernFrame1.Source = ((FirstFloor.ModernUI.Presentation.Link)AudioContent.SelectedItem).Source;
    }
    public void OnFragmentNavigation(FragmentNavigationEventArgs e)
    {
        //ModernFrame1.Source = new Uri("/Content/ControlAudio.xaml#page=recommendations,target_audio=" + e.Fragment, UriKind.RelativeOrAbsolute);
        //MessageBox.Show(e.Fragment);
    }
    public void OnNavigatedFrom(NavigationEventArgs e){ }
    public void OnNavigatedTo(NavigationEventArgs e){ }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e){ }
}