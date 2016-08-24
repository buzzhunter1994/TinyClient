using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using TinyClient.Api;
using System.Threading.Tasks;
using System.Windows.Forms;

partial class PageStart : IContent
{
    public void Control_Loaded(object sender, RoutedEventArgs e)
    {
        //WBrowser.Navigated += WBrowser_LoadCompleted;

      //  WBrowser.Navigate("https://oauth.vk.com/authorize?client_id=3895061&scope=2080255&display=mobile&revoke=1&redirect_uri=https://oauth.vk.com/blank.html&response_type=token");
    }

    void WBrowser_LoadCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
    {
        if (e.Url.AbsoluteUri.IndexOf("access_token=", StringComparison.OrdinalIgnoreCase) > 0)
        {
            System.Windows.MessageBox.Show(e.Url.Fragment);
        }
    }

    public void OnFragmentNavigation(FragmentNavigationEventArgs e) { }
    public void OnNavigatedFrom(NavigationEventArgs e) { }
    public void OnNavigatedTo(NavigationEventArgs e) { }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e) { }
}