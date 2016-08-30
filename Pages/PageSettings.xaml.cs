using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Forms;
using System.Diagnostics;
using TinyClient.Api;
partial class PageSettings : IContent
{
    public void OnFragmentNavigation(FragmentNavigationEventArgs e) { }
    public void OnNavigatedFrom(NavigationEventArgs e) {  }
    public void OnNavigatedTo(NavigationEventArgs e) 
    {
        accentColors.ItemsSource = new SolidColorBrush[] { 
            Elysium.AccentBrushes.Blue,
            Elysium.AccentBrushes.Brown,
            Elysium.AccentBrushes.Green,
            Elysium.AccentBrushes.Lime,
            Elysium.AccentBrushes.Magenta,
            Elysium.AccentBrushes.Mango,
            Elysium.AccentBrushes.Orange,
            Elysium.AccentBrushes.Pink,
            Elysium.AccentBrushes.Purple,
            Elysium.AccentBrushes.Red,
            Elysium.AccentBrushes.Rose,
            Elysium.AccentBrushes.Sky,
            Elysium.AccentBrushes.Violet,
            Elysium.AccentBrushes.Viridian 
        };
    }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e) { }

    private async void ThemeNameChanged(object sender, SelectionChangedEventArgs e)
    {
        await Elysium.Manager.ApplyAsync(System.Windows.Application.Current, TinyClient.Properties.Settings.Default.theme);
        switch (TinyClient.Properties.Settings.Default.theme) {
            case Elysium.Theme.Dark:
                TinyClient.Properties.Settings.Default.contrastColor = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                break;
            case Elysium.Theme.Light:
                TinyClient.Properties.Settings.Default.contrastColor = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                break;
        }
        Debug.WriteLine(TinyClient.Properties.Settings.Default.contrastColor.ToString());
        TinyClient.Properties.Settings.Default.Save();
    }
    private async void AccentColorSelected(object sender, SelectionChangedEventArgs e)
    {
        await Elysium.Manager.ApplyAsync(System.Windows.Application.Current, TinyClient.Properties.Settings.Default.theme, TinyClient.Properties.Settings.Default.accentColor, TinyClient.Properties.Settings.Default.contrastColor);
        TinyClient.Properties.Settings.Default.Save();        
    }

    private void browseButton_Click(object sender, RoutedEventArgs e)
    {
        System.Windows.Forms.OpenFileDialog OPF = new System.Windows.Forms.OpenFileDialog();
        OPF.Title = "Browse";
        OPF.Filter = "Images (*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
        OPF.CheckFileExists = true;
        OPF.CheckPathExists = true;
        if (OPF.ShowDialog() == DialogResult.OK)
        {
            TinyClient.Properties.Settings.Default.backgroundImage = OPF.FileName;
            TinyClient.Properties.Settings.Default.Save();
        }        
    }

    private void transparencyValue_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        TinyClient.Properties.Settings.Default.Save();
    }

    private void backgroundEnabled_Click(object sender, RoutedEventArgs e)
    {
        TinyClient.Properties.Settings.Default.Save();
    }
}