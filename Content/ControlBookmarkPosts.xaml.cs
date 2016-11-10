using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using TinyClient.Api;

partial class ControlBookmarkPosts : IContent
{
    private ObservableCollection<Types.Post> PostsList = new ObservableCollection<Types.Post>();

    public async void OnFragmentNavigation(FragmentNavigationEventArgs e)
    {
        try
        {
            PostsList = await Bookmarks.getPosts();
            PostsView.ItemsSource = PostsList;
        }
        catch { }
    }
    public void OnNavigatedFrom(NavigationEventArgs e) { }
    public void OnNavigatedTo(NavigationEventArgs e) { }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e) { }

    private void PostsView_Initialized(object sender, System.EventArgs e)
    {
        VirtualizingPanel.SetScrollUnit(PostsView, ScrollUnit.Pixel);
    }
}
