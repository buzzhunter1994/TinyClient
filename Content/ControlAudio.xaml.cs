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
        Progress.Visibility = Visibility.Visible;
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
                MusicList = await Audio.GetPopular(MyFragment["q"], MyFragment["only_eng"]);
                PlaylistView.ItemsSource = MusicList;
                break;
            case "search":
                MusicList = await Audio.Search(MyFragment["q"]);
                PlaylistView.ItemsSource = MusicList;
                break;
        }
        isLocked = false;
        Progress.Visibility = Visibility.Collapsed;
    }

    public void OnNavigatedFrom(NavigationEventArgs e){ }
    public void OnNavigatedTo(NavigationEventArgs e){ }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e){ }

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
        ScrollViewer p = (ScrollViewer)e.OriginalSource;
        p.ApplyTemplate();
        System.Windows.Controls.Primitives.ScrollBar b = (System.Windows.Controls.Primitives.ScrollBar)p.Template.FindName("PART_VerticalScrollBar", p);        
        ObservableCollection<Types.audio> audioList = null;
        if (!isPageEnd && MusicList != null)
        {
            if (Math.Abs(b.Value - b.Maximum) < 10 && MusicList.Count > 0)
            {
                Progress.Visibility = Visibility.Visible;
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
                        audioList = await Audio.GetPopular(MyFragment["q"], MyFragment["only_eng"], MusicList.Count.ToString());
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
                Progress.Visibility = Visibility.Collapsed;                
            }
        }
        isLocked = false;
      /*  Dim p = CType(e.OriginalSource, ScrollViewer)
        p.ApplyTemplate()
        Dim a = p.Template.FindName("PART_VerticalScrollBar", p)
        Dim b As Primitives.ScrollBar = CType(a, Primitives.ScrollBar)
        Dim audioList As ObservableCollection(Of types.audio) = Nothing
        If Not isPageEnd AndAlso Not IsNothing(MusicList1) Then
            If Math.Abs(b.Value - b.Maximum) < OtherApi.TOLERANCE And MusicList1.Count > 0 Then
                Select Case MyFragment.GetParametr("page")
                    Case "my"
                        audioList = Await audio.Get(MusicList1.Count.ToString)
                    Case "recommendations"
                        audioList =
                            Await _
                                audio.GetRecommendations(MusicList1.Count.ToString,
                                                         MyFragment.GetParametr("target_audio"))
                    Case "playlist"

                    Case "search"
                        audioList = Await audio.Search(MyFragment.GetParametr("q"), MusicList1.Count.ToString)
                    Case "popul"
                        audioList =
                            Await _
                                audio.GetPopular(MyFragment.GetParametr("q"),
                                                 MyFragment.GetParametr("only_eng"),
                                                 MusicList1.Count.ToString)
                End Select
                If Not IsNothing(audioList) AndAlso audioList.Count > 0 Then
                    For Each i In audioList
                        MusicList1.Add(i)
                    Next
                Else
                    isPageEnd = True
                End If
            End If
        End If*/
	}
}
