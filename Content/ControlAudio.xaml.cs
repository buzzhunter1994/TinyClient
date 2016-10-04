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

public partial class ControlAudio : IContent
{
    private ObservableCollection<Types.audio> MusicList = new ObservableCollection<Types.audio>();
    private int selectedSong;
    private NameValueCollection MyFragment;
    private bool isPageEnd;
    private bool isLocked;
    private bool isForeing;
    private int tracks = 0;
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
        tracks = 0;
        Common.TinyMainWindow.IsBusy = true;

        await Task.Delay(1000);

        switch (MyFragment["page"])
        {
            case "playlist":
                if (Common.MusicPlayer.Playlist.Count != 0)
                {
                    MusicList = Common.MusicPlayer.Playlist;
                    PlaylistView.SelectedIndex = Common.MusicPlayer.CurrentIndex;
                    PlaylistView.ScrollIntoView(PlaylistView.SelectedItem);
                }
                else
                {
                    MusicList = await Audio.Get();
                }
                break;
            case "audio":
                MusicList = await Audio.Get();
                break;
            case "recommendations":
                MusicList = await Audio.GetRecommendations();
                break;
            case "popular":
                MusicList = await Audio.GetPopular(MyFragment["q"], isForeing);
                break;
            case "search":
                MusicList = await Audio.Search(MyFragment["q"]);
                break;
        }

        PlaylistView.ItemsSource = MusicList;
        /*   await Task.Factory.StartNew(() => {
               Dispatcher.BeginInvoke(new Action(() =>
               {

               }));
           });*/
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
        Common.MusicPlayer.Playlist = MusicList;
        Common.MusicPlayer.Play(PlaylistView.SelectedIndex);
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
            if (Math.Abs(b.Value - b.Maximum) < 30 && MusicList.Count > 0)
            {
                Common.TinyMainWindow.IsBusy = true;
                
                switch (MyFragment["page"])
                {
                    case "audio":
                        tracks += 500;
                        audioList = await Audio.Get(tracks.ToString());
                        break;
                    case "recommendations":
                        tracks = MusicList.Count;
                        audioList = await Audio.GetRecommendations(MyFragment["target_audio"], "", tracks.ToString());
                        break;
                    case "playlist":
                        break;
                    case "search":
                        tracks = MusicList.Count;
                        audioList = await Audio.Search(MyFragment["q"], tracks.ToString());
                        break;
                    case "popular":
                        tracks = MusicList.Count;
                        audioList = await Audio.GetPopular(MyFragment["q"], isForeing, tracks.ToString());
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
                    Debug.WriteLine(MusicList.Count);
                }
                Common.TinyMainWindow.IsBusy = false;
            }
        }
        isLocked = false;
    }
    private void SetPlaylistView(ref ListBox View) {
        Common.PlayListV = View;
    }
    private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
    {
        SetPlaylistView(ref PlaylistView);
    }
}
