using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TinyClient.Api;

partial class ControlBookmarkPeople : IContent
{
    private ObservableCollection<Types.profile> UserList = new ObservableCollection<Types.profile>();

    public async void OnFragmentNavigation(FragmentNavigationEventArgs e)
    {
        UserList = await Bookmark.Get();
        await Task.Factory.StartNew(() =>
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                PeopleView.ItemsSource = UserList;
            }));
        });
    }
    public void OnNavigatedFrom(NavigationEventArgs e) { }
    public void OnNavigatedTo(NavigationEventArgs e) { }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e) { }
}
