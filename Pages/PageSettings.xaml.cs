using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TinyClient.Api;
partial class PageSettings : IContent
{

    public void OnFragmentNavigation(FragmentNavigationEventArgs e) { }
    public void OnNavigatedFrom(NavigationEventArgs e) {  }
    public void OnNavigatedTo(NavigationEventArgs e) 
    {
        accentColors.ItemsSource = new SolidColorBrush[] { Elysium.AccentBrushes.Blue, Elysium.AccentBrushes.Brown, Elysium.AccentBrushes.Green, Elysium.AccentBrushes.Lime, Elysium.AccentBrushes.Magenta, Elysium.AccentBrushes.Mango, Elysium.AccentBrushes.Orange, Elysium.AccentBrushes.Pink, Elysium.AccentBrushes.Purple, Elysium.AccentBrushes.Red, Elysium.AccentBrushes.Rose, Elysium.AccentBrushes.Sky, Elysium.AccentBrushes.Violet, Elysium.AccentBrushes.Viridian };
        accentColors.SelectedItem = TinyClient.Properties.Settings.Default.accentColor;
        ThemeName.SelectedIndex = (int)TinyClient.Properties.Settings.Default.theme;
    }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e) { }

    private async void ThemeNameChanged(object sender, SelectionChangedEventArgs e)
    {
        await Elysium.Manager.ApplyAsync(Application.Current, TinyClient.Properties.Settings.Default.theme);
        TinyClient.Properties.Settings.Default.Save();
    }
    private async void AccentColorSelected(object sender, SelectionChangedEventArgs e)
    {
        await Elysium.Manager.ApplyAsync(Application.Current, TinyClient.Properties.Settings.Default.theme, TinyClient.Properties.Settings.Default.accentColor, TinyClient.Properties.Settings.Default.contrastColor);
        TinyClient.Properties.Settings.Default.Save();        
    }
}