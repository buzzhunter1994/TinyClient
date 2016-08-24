using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TinyClient.Api;
using Un4seen.Bass;

namespace TinyClient
{
    public class PlayerWindow : IDisposable
    {
        public int Channel;
        public int CurrentIndex;
        public bool repeat;
        public ObservableCollection<Types.audio> Playlist = new ObservableCollection<Types.audio>();
        public DispatcherTimer timer = new DispatcherTimer();
        public String CurrentTitle
        {
            get { return Playlist.Count!=0?Playlist[CurrentIndex].full_title:"Text"; }
            set { }
        }

        public PlayerWindow()
        {
            BassNet.Registration("mc-shura@yandex.ua", "2X2223183433");
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timer.Tick += timer_Tick;
        }

        BASSActive isActiveChannel;

        void timer_Tick(object sender, EventArgs e)
        {
           /* object er = Bass.BASS_ErrorGetCode;
            if ((er != BASSError.BASS_OK))
            {
                OtherApi.ShowMicroVkNot("Bass.dll error", er.ToString());
                TimerPlayer1.Stop();
                return;
            }
            */
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

            /*if ((Bass.BASS_ChannelIsActive(Channel) != BASSActive.BASS_ACTIVE_PLAYING))
            {
                OtherApi.MyWindow1.SetButtonData(true);
            }
            */
            Bass.BASS_ChannelSetAttribute(Channel, BASSAttribute.BASS_ATTRIB_VOL, (float)(Properties.Settings.Default.volume / Common.TinyMainWindow.Volume.Maximum));
            if ((Mouse.LeftButton == MouseButtonState.Released))
            {
                Common.TinyMainWindow.Timeline.Maximum = Bass.BASS_ChannelGetLength(Channel);
                Common.TinyMainWindow.Timeline.Value = Bass.BASS_ChannelGetPosition(Channel);
                if ((Bass.BASS_ChannelIsActive(Channel) == BASSActive.BASS_ACTIVE_STOPPED))
                {
                    if (!(Playlist == null) && (Playlist.Count > 0))
                    {
                        if (!repeat)
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

                            Play(Playlist[CurrentIndex]);
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
            Common.TinyMainWindow.Volume.SelectionEnd = Math.Abs((Un4seen.Bass.Utils.HighWord(Bass.BASS_ChannelGetLevel(Channel)) / 4));
        }

        public async void Play(Types.audio audio)
        {
            timer.Stop();
            Common.TinyMainWindow.Timeline.Value = 0;
            Common.TinyMainWindow.Timeline.SelectionEnd = 0;
            Common.TinyMainWindow.Timeline.Value = 0;
            Common.TinyMainWindow.IsBusy = true;
            Bass.BASS_ChannelSlideAttribute(Channel, BASSAttribute.BASS_ATTRIB_VOL, 0, 500);
            int Channel1 = 0;
            if (audio != null)
                if (audio.url != null)
                Channel1 = await Task.Factory.StartNew(() => { 
                    return Bass.BASS_StreamCreateURL(audio.url, 0, BASSFlag.BASS_DEFAULT, null, IntPtr.Zero); 
                });
            Bass.BASS_StreamFree(Channel);
            Channel = Channel1;

            Bass.BASS_ChannelSetAttribute(Channel, BASSAttribute.BASS_ATTRIB_VOL, (float)(Properties.Settings.Default.volume / Common.TinyMainWindow.Volume.Maximum));
            Bass.BASS_ChannelPlay(Channel, true);
            timer.Start();
            Common.TinyMainWindow.IsBusy = false;
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
            if (!(Playlist == null) && (Playlist.Count > 0))
            {
                if (++CurrentIndex >= Playlist.Count) CurrentIndex = 0;
                Play(Playlist[CurrentIndex]);
            }
        }
        public void PlayPrev()
        {
            if (!(Playlist == null) && (Playlist.Count > 0))
            {
                if (--CurrentIndex <= 0) CurrentIndex = Playlist.Count - 1;
                Play(Playlist[CurrentIndex]);
            }
        }
        private void EndSync(int syncHandle, int Channel, int data, IntPtr user)
        {
            PlayNext();
        }     

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
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
            // GC.SuppressFinalize(this);
        }
        #endregion

        public void TimelineChange(double value)
        {
            Bass.BASS_ChannelSetPosition(Channel, (long)value);
        }

        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //Bass.BASS_ChannelSlideAttribute(Channel, BASSAttribute.BASS_ATTRIB_VOL, (float)(e.NewValue / Volume.Maximum),1000);
        }
    }
}
