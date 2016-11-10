using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Threading.Tasks;
using TinyClient.CustomExtensions;
using System.Windows;
using System.Windows.Forms;
using TinyClient.Api;

partial class PageStart : IContent
{
    public async void Control_Loaded(object sender, RoutedEventArgs e)
    {
        //WBrowser.Navigated += WBrowser_LoadCompleted;
        await Task.Delay(1000);
        bool check = await Common.isValidToken();
        
        if (check) 
        {
            Common.TinyMainWindow.MainFrame.Source = new Uri(TinyClient.Properties.Settings.Default.lastSection, UriKind.Relative);
        }
        else 
        {
            WBrowser.Navigate("https://oauth.vk.com/authorize?client_id=3895061&scope=998431&display=mobile&revoke=1&redirect_uri=https://oauth.vk.com/blank.html&response_type=token");
            WFH.Visibility = Visibility.Visible;
        }
        //ce951bc1b4c6e4bc6f0b11148785da4d82c92659a007a41d1ea1416f162d96ff133c22743a286c29230d6
        //  WBrowser.Navigate("https://oauth.vk.com/authorize?client_id=3895061&scope=998431&display=mobile&revoke=1&redirect_uri=https://oauth.vk.com/blank.html&response_type=token");
    }

    void WBrowser_LoadCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
    {
        if (e.Url.AbsoluteUri.IndexOf("access_token=", StringComparison.OrdinalIgnoreCase) > 0)
        {
            if (e.Url.Fragment.GetParametr("access_token") != "") {
                TinyClient.Properties.Settings.Default.AccessToken = e.Url.Fragment.GetParametr("access_token");
                TinyClient.Properties.Settings.Default.Save();
                Common.TinyMainWindow.MainFrame.Source = new Uri("Pages/PageAudio.xaml#page=playlist", UriKind.Relative);
            }
        }
    }

    public void OnFragmentNavigation(FragmentNavigationEventArgs e) { }
    public void OnNavigatedFrom(NavigationEventArgs e) { }
    public void OnNavigatedTo(NavigationEventArgs e) { }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e) { }
}