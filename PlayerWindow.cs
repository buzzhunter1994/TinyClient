using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using TinyClient.Api;
using System.Linq;
using Un4seen.Bass;

namespace TinyClient
{
    public class PlayerWindow : IDisposable, INotifyPropertyChanged
    {
        public int Channel;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
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


        public DispatcherTimer timer = new DispatcherTimer();

        public PlayerWindow()
        {
            BassNet.Registration("mc-shura@yandex.ua", "2X2223183433");
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += timer_Tick;

        }

        BASSActive isActiveChannel;

        void timer_Tick(object sender, EventArgs e)
        {
            BASSActive bassActive = Bass.BASS_ChannelIsActive(Channel);
            if ((bassActive != isActiveChannel))
            {
                isActiveChannel = bassActive;
                /* if ((bassActive != BASSActive.BASS_ACTIVE_PLAYING))
                 {
                     OtherApi.MyWindow1.SetButtonData(true);
                 }
                 else
                 {
                     OtherApi.MyWindow1.SetButtonData(false);
                 }*/
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

            /*if ((Bass.BASS_ChannelIsActive(Channel) != BASSActive.BASS_ACTIVE_PLAYING))
            {
                OtherApi.MyWindow1.SetButtonData(true);
            }
            */
            //Bass.BASS_ChannelSetAttribute(Channel, BASSAttribute.BASS_ATTRIB_VOL, (float)(Properties.Settings.Default.volume / Common.TinyMainWindow.Volume.Maximum));
            if ((Mouse.LeftButton == MouseButtonState.Released))
            {
                Common.TinyMainWindow.Timeline.Maximum = Bass.BASS_ChannelGetLength(Channel);
                Common.TinyMainWindow.Timeline.Value = Position;
                if ((Bass.BASS_ChannelIsActive(Channel) == BASSActive.BASS_ACTIVE_STOPPED))
                {
                    if (!(Playlist == null) && (Playlist.Count > 0))
                    {
                        if (!Repeat)
                        {
                            /*  if (RandomCheckBox.isChecked)
                              {
                                  //Playlist1.Count;
                              }
                              else */
                            if ((CurrentIndex == (Playlist.Count - 1)))
                            {
                                CurrentIndex = 0;
                            }
                            else
                            {
                                CurrentIndex++;
                            }

                            Play(CurrentIndex);
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
           /* var selectedTeams = Playlist.Where(t=>t.State);
            foreach (var s in selectedTeams)
            {

            }*/
            Common.MusicPlayer.Playlist[CurrentIndex].State = false;
            //((Types.audio)Common.PlayListV.Items[CurrentIndex]).State = false;
            CurrentIndex = CurrentIndexView;

            Common.MusicPlayer.Playlist[CurrentIndex].State = true;
            //((Types.audio)Common.PlayListV.Items[CurrentIndex]).State = true;
            if (CurrentIndex > 0 && CurrentIndex < Playlist.Count)
                Song = Playlist[CurrentIndex];
            timer.Stop();
            Common.TinyMainWindow.IsBusy = true;
            Bass.BASS_ChannelSlideAttribute(Channel, BASSAttribute.BASS_ATTRIB_VOL, 0, 500);
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
            Common.TinyMainWindow.mainGrid.RowDefinitions[1].Height = new GridLength(52);
            Channel = Channel1;
            Bass.BASS_ChannelSetAttribute(Channel, BASSAttribute.BASS_ATTRIB_VOL, (float)(Properties.Settings.Default.volume / Common.TinyMainWindow.Volume.Maximum));
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

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }
            Common.TinyMainWindow.mainGrid.RowDefinitions[1].Height = new GridLength(0);
            Playlist = null;
            Bass.BASS_StreamFree(Channel);
            GC.Collect();
            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PlayerWindow() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion


    }

}