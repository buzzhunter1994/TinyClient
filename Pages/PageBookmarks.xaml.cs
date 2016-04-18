using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

partial class PageBookmarks : IContent
{
    public void SelectType(object sender, RoutedEventArgs e)
    {
        Debug.WriteLine(((Button)sender).Tag.ToString());
        ModernFrame.Source = new Uri(((Button)sender).Tag.ToString(), UriKind.Relative);
    }
    public void OnFragmentNavigation(FragmentNavigationEventArgs e)
    {

    }
    public void OnNavigatedFrom(NavigationEventArgs e) { }
    public void OnNavigatedTo(NavigationEventArgs e) { }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e) { }

}