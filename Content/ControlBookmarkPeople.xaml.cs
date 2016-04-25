using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System.Collections.ObjectModel;
using TinyClient.Api;

public partial class ControlBookmarkPeople : IContent
{
    private ObservableCollection<Types.profile> UserList = new ObservableCollection<Types.profile>();
    private bool isLocked;

    public async void OnFragmentNavigation(FragmentNavigationEventArgs e)
    {
        if (isLocked) return;
        isLocked = true;
        
        UserList = await Bookmark.Get();
        PeopleView.ItemsSource = UserList;

        isLocked = false;
    }

    public void OnNavigatedFrom(NavigationEventArgs e) { }
    public void OnNavigatedTo(NavigationEventArgs e) { }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e) { }
}
