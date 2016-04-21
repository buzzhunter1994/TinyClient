using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

partial class PageProfile : IContent
{
    public void OnFragmentNavigation(FragmentNavigationEventArgs e) { }
    public void OnNavigatedFrom(NavigationEventArgs e) { }
    public void OnNavigatedTo(NavigationEventArgs e)
    {
        ModernFrame.Source = new Uri("/Content/ControlBookmarkPeople.xaml", UriKind.Relative);
    }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e) { }

}