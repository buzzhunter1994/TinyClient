using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using TinyClient.Api;
using System.Threading.Tasks;

partial class PageFriends : IContent
{
    private ObservableCollection<Types.profile> FriendsList = new ObservableCollection<Types.profile>();

    public async void OnFragmentNavigation(FragmentNavigationEventArgs e)
    {
        await Task.Delay(100);
        FriendsList = await Friends.Get();
        FriendsView.ItemsSource = FriendsList;
    }
    public void OnNavigatedFrom(NavigationEventArgs e) { }
    public void OnNavigatedTo(NavigationEventArgs e) { }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e) { }

}