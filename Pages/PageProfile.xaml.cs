using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using TinyClient.Api;

partial class PageProfile : IContent
{
    private List<Types.profile> UserProfile;
    private NameValueCollection MyFragment;
    public async void OnFragmentNavigation(FragmentNavigationEventArgs e)
    {
        try
        {
            MyFragment = HttpUtility.ParseQueryString(e.Fragment.Replace(',', '&'));
        }
        catch
        {

        }
        UserProfile = await Profile.Get(MyFragment["user_id"]);
        this.DataContext = UserProfile;
    }
    public void OnNavigatedFrom(NavigationEventArgs e) { }
    public void OnNavigatedTo(NavigationEventArgs e) { }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e) { }

}