using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TinyClient;
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
