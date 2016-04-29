using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System.Collections.ObjectModel;
using TinyClient.Api;

partial class PageFriends : IContent
{
    private ObservableCollection<Types.profile> FriendsList = new ObservableCollection<Types.profile>();

    public async void OnFragmentNavigation(FragmentNavigationEventArgs e)
    {
        FriendsList = await Friends.Get();
        FriendsView.ItemsSource = FriendsList;
    }
    public void OnNavigatedFrom(NavigationEventArgs e) { }
    public void OnNavigatedTo(NavigationEventArgs e) { }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e) { }
}