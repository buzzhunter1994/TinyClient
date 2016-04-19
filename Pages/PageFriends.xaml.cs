using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using TinyClient.Api;

partial class PageFriends : IContent
{
    private ObservableCollection<Types.profile> FriendsList = new ObservableCollection<Types.profile>();

    public void OnFragmentNavigation(FragmentNavigationEventArgs e)
    {
        
    }
    public void OnNavigatedFrom(NavigationEventArgs e) { }
    public void OnNavigatedTo(NavigationEventArgs e) { }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e) { }

    private async void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        FriendsList = await Friends.Get();
        FriendsView.ItemsSource = FriendsList;
    }
}