using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TinyClient.Api;

partial class PageFriends : IContent
{
    private ObservableCollection<Types.profile> FriendsList = new ObservableCollection<Types.profile>();

    public async void OnFragmentNavigation(FragmentNavigationEventArgs e)
    {
        try
        {
            FriendsList = await Friends.Get();
            await Task.Factory.StartNew(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    FriendsView.ItemsSource = FriendsList;
                }));
            });
        }
        catch { }
    }
    public void OnNavigatedFrom(NavigationEventArgs e) { }
    public void OnNavigatedTo(NavigationEventArgs e) { }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e) { }
}