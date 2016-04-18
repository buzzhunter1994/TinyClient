using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TinyClient;
using TinyClient.Api;

public partial class ControlAudio : IContent
{
    private ObservableCollection<Types.audio> MusicList = new ObservableCollection<Types.audio>();
    private NameValueCollection MyFragment;
    private bool isPageEnd;
    private bool isLocked;
    private bool isForeing;

    public async void OnFragmentNavigation(FragmentNavigationEventArgs e)
    {
        if (isLocked) return;
        isLocked = true;
        try
        {
            MyFragment = HttpUtility.ParseQueryString(e.Fragment.Replace(',', '&'));
        }
        catch
        {

        }
        isPageEnd = false;
        Common.TinyMainWindow.IsBusy = true;
        switch (MyFragment["page"])
        {
            case "playlist":
                if (Common.MusicPlayer != null)
                {
                    MusicList = Common.MusicPlayer.Playlist;
                    PlaylistView.ItemsSource = MusicList;
                    PlaylistView.SelectedIndex = Common.MusicPlayer.CurrentIndex;
                    PlaylistView.ScrollIntoView(PlaylistView.SelectedItem);
                }
                else
                {
                    MusicList = await Audio.Get();
                    PlaylistView.ItemsSource = MusicList;
                }
                break;
            case "audio":
                MusicList = await Audio.Get();
                PlaylistView.ItemsSource = MusicList;
                break;
            case "recommendations":
                MusicList = await Audio.GetRecommendations();
                PlaylistView.ItemsSource = MusicList;
                break;
            case "popular":
                MusicList = await Audio.GetPopular(MyFragment["q"], isForeing);
                PlaylistView.ItemsSource = MusicList;
                break;
            case "search":
                MusicList = await Audio.Search(MyFragment["q"]);
                PlaylistView.ItemsSource = MusicList;
                break;
        }
        isLocked = false;
        Common.TinyMainWindow.IsBusy = false;
    }

    public void OnNavigatedFrom(NavigationEventArgs e) { }
    public void OnNavigatedTo(NavigationEventArgs e) { }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e) { }

    private void PlaylistView_MouseDoubleClick(Object sender, MouseButtonEventArgs e)
    {
        if (Common.MusicPlayer == null)
            Common.MusicPlayer = new PlayerWindow();
        Common.MusicPlayer.Play((Types.audio)PlaylistView.SelectedItem);
        Common.MusicPlayer.Playlist = MusicList;
        Common.MusicPlayer.CurrentIndex = PlaylistView.SelectedIndex;
    }

    private async void PlaylistView_ScrollChanged(Object sender, ScrollChangedEventArgs e)
    {
        if (isLocked) return;
        isLocked = true;
        ObservableCollection<Types.audio> audioList = null;
        ScrollViewer p = (ScrollViewer)e.OriginalSource;
        p.ApplyTemplate();
        System.Windows.Controls.Primitives.ScrollBar b = (System.Windows.Controls.Primitives.ScrollBar)p.Template.FindName("PART_VerticalScrollBar", p);

        if (!isPageEnd && MusicList != null)
        {
            if (Math.Abs(b.Value - b.Maximum) < 10 && MusicList.Count > 0)
            {
                Common.TinyMainWindow.IsBusy = true;
                switch (MyFragment["page"])
                {
                    case "audio":
                        audioList = await Audio.Get(MusicList.Count.ToString());
                        break;
                    case "recommendations":
                        audioList = await Audio.GetRecommendations(MyFragment["target_audio"], "", MusicList.Count.ToString());
                        break;
                    case "playlist":
                        break;
                    case "search":
                        audioList = await Audio.Search(MyFragment["q"], MusicList.Count.ToString());
                        break;
                    case "popular":
                        audioList = await Audio.GetPopular(MyFragment["q"], isForeing, MusicList.Count.ToString());
                        break;
                }
                if (audioList != null && audioList.Count > 0)
                {
                    foreach (Types.audio item in audioList)
                        if (!MusicList.Contains(item))
                            MusicList.Add(item);
                }
                else
                {
                    isPageEnd = true;
                }
                await Task.Delay(1000);
                Common.TinyMainWindow.IsBusy = false;
            }
        }
        isLocked = false;
    }
}
