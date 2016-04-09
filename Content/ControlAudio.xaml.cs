using FirstFloor.ModernUI.Windows;
using System.Collections.ObjectModel;
using FirstFloor.ModernUI.Windows.Navigation;
using TinyClient.CustomExtensions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using TinyClient.Api;

public partial class ControlAudio : IContent
{
    private ObservableCollection<Types.audio> MusicList = new ObservableCollection<Types.audio>();
    string MyFragment;

    public async void OnFragmentNavigation(FragmentNavigationEventArgs e)
    {
        MyFragment = e.Fragment;
        string p = e.Fragment.GetParametr("page");
        switch (p)
        {
            case "my":
                MusicList = await Audio.Get();
                ListBox1.ItemsSource = MusicList;
                break;
        }
        /*
        isPageEnd = False
        Select Case p
            Case "my"
                MusicList1 = Await audio.Get
                ListBox1.ItemsSource = MusicList1
            Case "recommendations"
                MusicList1 = Await audio.GetRecommendations("", e.Fragment.GetParametr("target_audio"))
                ListBox1.ItemsSource = MusicList1
            Case "playlist"
                If Not IsNothing(OtherApi.BassPlayer1) Then
                    MusicList1 = OtherApi.BassPlayer1.Playlist1
                    ListBox1.ItemsSource = MusicList1
                    ListBox1.SelectedIndex = OtherApi.BassPlayer1.PlaylistTecIndex1
                    ListBox1.ScrollIntoView(ListBox1.SelectedItem)
                End If
            Case "search"
                MusicList1 = Await audio.Search(e.Fragment.GetParametr("q"))
                ListBox1.ItemsSource = MusicList1
            Case "popul"
                MusicList1 = Await audio.GetPopular(e.Fragment.GetParametr("q"), e.Fragment.GetParametr("only_eng"))
                ListBox1.ItemsSource = MusicList1
            Case "radio"
                if radio_list Is nothing
                    Dim s = File.ReadAllText("radio/stations.json")
                    Dim alist = JsonConvert.DeserializeObject (Of types.radio())(s)
                    radio_list = New ObservableCollection(Of types.audio)
                    For Each r In alist
                        radio_list.Add(r)
                        r.is_radio = true
                    Next
                End If
                dim mode = e.Fragment.GetParametr("mode")
                if mode = "last"
                    MusicList1 = New ObservableCollection(Of types.audio)
                    For Each aud In SettingSystem.LastRadio
                        MusicList1.Add(aud)
                    Next
                else If mode <> "all"
                    MusicList1 = New ObservableCollection(Of types.audio)
                    Dim rl = radio_list.Where(function(audio) audio.teg = mode)
                    For Each aud In rl
                        MusicList1.Add(aud)
                    Next
                else
                    MusicList1 = radio_list
                End If
                ListBox1.ItemsSource = MusicList1
        End Select*/
    }

    public void OnNavigatedFrom(NavigationEventArgs e){ }
    public void OnNavigatedTo(NavigationEventArgs e){ }
    public void OnNavigatingFrom(NavigatingCancelEventArgs e){ }

    private void ListBox1_MouseDoubleClick(Object sender, MouseButtonEventArgs e){
        /*If IsNothing(OtherApi.BassPlayer1) Then OtherApi.BassPlayer1 = New WindowPlayer
        OtherApi.BassPlayer1.Play(CType(ListBox1.SelectedItem, types.audio))
        OtherApi.BassPlayer1.Playlist1 = MusicList1
        OtherApi.BassPlayer1.PlaylistTecIndex1 = ListBox1.SelectedIndex*/
    }

    private bool isPageEnd;

    private async void ListBox1_ScrollChanged(Object sender, ScrollChangedEventArgs e)
	{
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
