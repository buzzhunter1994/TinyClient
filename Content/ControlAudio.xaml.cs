using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TinyClient;
using TinyClient.Api;
using Un4seen.Bass;

public partial class ControlAudio : IContent, INotifyPropertyChanged
{

    #region Var
    private ObservableCollection<Types.audio> MusicList = new ObservableCollection<Types.audio>();
    private NameValueCollection MyFragment;
    private int tracks = 0;
    private int Channel;
    private bool isPageEnd;
    private bool isLocked;
    private DispatcherTimer timer = new DispatcherTimer();
    private BASSActive isActiveChannel;
    public event PropertyChangedEventHandler PropertyChanged;

    private ObservableCollection<Types.audio> _playlist;
    public ObservableCollection<Types.audio> Playlist
    {
        get { return _playlist; }
        set
        {
            if (_playlist != value)
            {
                _playlist = value;
                OnPropertyChanged("Playlist");
            }
        }
    }

    private Types.audio _song;
    public Types.audio Song
    {
        get { return _song; }
        set
        {
            if (_song != value)
            {
                _song = value;
                OnPropertyChanged("Song");
            }
        }
    }

    private int _currentIndex;
    public int CurrentIndex
    {
        get { return _currentIndex; }
        set
        {
            if (_currentIndex != value)
            {
                _currentIndex = value;
                OnPropertyChanged("CurrentIndex");
            }
        }
    }

    private bool _repeat;
    public bool Repeat
    {
        get { return _repeat; }
        set
        {
            if (_repeat != value)
            {
                _repeat = value;
                OnPropertyChanged("Repeat");
            }
        }
    }

    public float Volume
    {
        get
        {
            float vol = 0f;
            if (Bass.BASS_ChannelGetAttribute(Channel, BASSAttribute.BASS_ATTRIB_VOL, ref vol))
                return vol;
            return 0f;
        }
        set
        {
            Bass.BASS_ChannelSlideAttribute(Channel, BASSAttribute.BASS_ATTRIB_VOL, (float)(value / Common.TinyMainWindow.Volume.Maximum), 1000);
        }
    }

    public long Position
    {
        get
        {
            long pos = 0;
            if (Channel != 0)
                pos = Bass.BASS_ChannelGetPosition(Channel);
            return pos != 0 ? pos : 0;
        }
        set
        {
            Bass.BASS_ChannelSetPosition(Channel, value); Bass.BASS_ChannelSlideAttribute(Channel, BASSAttribute.BASS_ATTRIB_VOL, (float)(value / Common.TinyMainWindow.Volume.Maximum), 1000);
        }
    }

    #endregion

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
                if (Playlist != null)
                {
                    if (Playlist.Count != 0)
                    {
                        MusicList = Playlist;
                        PlaylistView.SelectedIndex = CurrentIndex;
                        PlaylistView.ScrollIntoView(PlaylistView.SelectedItem);
                    }

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
                MusicList = await Audio.GetPopular(MyFragment["q"], false);
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
       // if (Common.MusicPlayer == null)
       //     Common.MusicPlayer = new PlayerWindow();
        Playlist = MusicList;
        Play(PlaylistView.SelectedIndex);
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
                        audioList = await Audio.GetPopular(MyFragment["q"], false, tracks.ToString());
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
                Common.TinyMainWindow.IsBusy = false;
            }
        }
        isLocked = false;
    }

    protected void OnPropertyChanged(string name)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }

    

    public ControlAudio() {
        BassNet.Registration("mc-shura@yandex.ua", "2X2223183433");
        Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
        timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        timer.Tick += timer_Tick;
        Common.MusicPlayer = this;
    }

    void timer_Tick(object sender, EventArgs e)
    {
        BASSActive bassActive = Bass.BASS_ChannelIsActive(Channel);
        if ((bassActive != isActiveChannel))
        {
            isActiveChannel = bassActive;
            if ((bassActive != BASSActive.BASS_ACTIVE_PLAYING))
            {
                Common.TinyMainWindow.PlayPauseButton.Tag = "";
            }
            else
            {
                Common.TinyMainWindow.PlayPauseButton.Tag = "Pause";
            }
        }

        if ((bassActive != BASSActive.BASS_ACTIVE_PLAYING))
        {
            timer.Stop();
        }

        float progress;
        if (Channel != 0)
        {
            long len = Bass.BASS_StreamGetFilePosition(Channel, BASSStreamFilePosition.BASS_FILEPOS_END);
            long down = Bass.BASS_StreamGetFilePosition(Channel, BASSStreamFilePosition.BASS_FILEPOS_DOWNLOAD);
            BASS_CHANNELINFO info = Bass.BASS_ChannelGetInfo(Channel);
            if (info != null)
            {
                if ((info.flags & BASSFlag.BASS_STREAM_BLOCK) != BASSFlag.BASS_DEFAULT)
                {
                    long dec = Bass.BASS_StreamGetFilePosition(Channel, BASSStreamFilePosition.BASS_FILEPOS_CURRENT);
                    progress = (down - dec) * 100f / len;
                    if (progress > 100)
                        progress = 100;
                }
                else
                {
                    progress = down * 100f / len;
                }
                Common.TinyMainWindow.Timeline.SelectionEnd = ((double)progress * Common.TinyMainWindow.Timeline.Maximum) / 100;
            }
        }

        if ((Bass.BASS_ChannelIsActive(Channel) != BASSActive.BASS_ACTIVE_PLAYING))
        {
            Common.TinyMainWindow.PlayPauseButton.Tag = "";
        }
        
        if ((Mouse.LeftButton == MouseButtonState.Released))
        {
            Common.TinyMainWindow.Timeline.Maximum = Bass.BASS_ChannelGetLength(Channel);
            Common.TinyMainWindow.Timeline.Value = Position;
            if ((Bass.BASS_ChannelIsActive(Channel) == BASSActive.BASS_ACTIVE_STOPPED))
            {
                if (!(Playlist == null) && (Playlist.Count > 0))
                {
                    if (!TinyClient.Properties.Settings.Default.repeat)
                    {
                        PlayNext();
                    }
                    else
                    {
                        Bass.BASS_ChannelPlay(Channel, true);
                        timer.Start();
                        /*   if (CheckBox2.IsChecked)
                           {
                               object a1 = ((Types.audio)(DataContext));
                               Audio.SetBroadcast((a1.owner_id + ("_" + a1.id)));
                           }*/
                    }
                }
            }
        }
    }

    bool locked = false;
    public async void Play(int CurrentIndexView)
    {
        if (locked) return;
        locked = true;
        if (Song != null)
            Song.State = false;
        //Playlist[CurrentIndex].State = false;
        CurrentIndex = CurrentIndexView;
        Playlist[CurrentIndex].State = true;
        if (CurrentIndex > 0 && CurrentIndex < Playlist.Count)
            Song = Playlist[CurrentIndex];
        timer.Stop();
        Common.TinyMainWindow.IsBusy = true;
        int Channel1 = 0;
        if (Song != null)
            if (Song.url != null)
                Channel1 = await Task.Factory.StartNew(() =>
                {
                    return Bass.BASS_StreamCreateURL(Song.url, 0, BASSFlag.BASS_DEFAULT, null, IntPtr.Zero);
                });
        Bass.BASS_StreamFree(Channel);

        Common.TinyMainWindow.Timeline.Value = 0;
        Common.TinyMainWindow.Timeline.SelectionEnd = 0;
        Common.TinyMainWindow.PlayerGrid.DataContext = this;
        Common.TinyMainWindow.mainGrid.RowDefinitions[1].Height = new GridLength(55);
        Channel = Channel1;
        Bass.BASS_ChannelSetAttribute(Channel, BASSAttribute.BASS_ATTRIB_VOL, (float)(TinyClient.Properties.Settings.Default.volume / Common.TinyMainWindow.Volume.Maximum));
        Bass.BASS_ChannelPlay(Channel, true);
        timer.Start();
        Common.TinyMainWindow.IsBusy = false;

        locked = false;
    }

    public void PlayPause()
    {
        if (Channel != 0)
        {
            if (Bass.BASS_ChannelIsActive(Channel) == BASSActive.BASS_ACTIVE_PAUSED)
            {
                Bass.BASS_ChannelPlay(Channel, false);
                timer.Start();
            }
            else if (Bass.BASS_ChannelIsActive(Channel) == BASSActive.BASS_ACTIVE_PLAYING)
                Bass.BASS_ChannelPause(Channel);
        }
    }

    public void PlayNext()
    {
        int newIndex = CurrentIndex;
        if (!(Playlist == null) && (Playlist.Count > 0))
        {
            if (++newIndex >= Playlist.Count) newIndex = 0;
            Play(newIndex);
        }
    }

    public void PlayPrev()
    {
        int newIndex = CurrentIndex;
        if (!(Playlist == null) && (Playlist.Count > 0))
        {
            if (--newIndex <= 0) newIndex = Playlist.Count - 1;
            Play(newIndex);
        }
    }

    public void TimelineChange(double value)
    {
        Position = (long)value;
    }

    private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
    {

    }
}
