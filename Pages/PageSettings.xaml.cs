using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System.Windows;
using System.Windows.Controls;
using TinyClient.Api;
partial class PageSettings : IContent
{
    public void OnFragmentNavigation(FragmentNavigationEventArgs e) { }
    public void OnNavigatedFrom(NavigationEventArgs e) {  }
    public void OnNavigatedTo(NavigationEventArgs e) { ThemeName.SelectedIndex = (int)TinyClient.Properties.Settings.Default.theme;}
    public void OnNavigatingFrom(NavigatingCancelEventArgs e) { }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        TinyClient.Properties.Settings.Default.theme = (Elysium.Theme)((ComboBox)sender).SelectedIndex;
        TinyClient.Properties.Settings.Default.Save();
        Elysium.Manager.Apply(Application.Current, TinyClient.Properties.Settings.Default.theme);
    }
}