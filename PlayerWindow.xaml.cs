using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Un4seen.Bass;

namespace TinyClient
{
    public partial class PlayerWindow : Window, IDisposable
    {
        public int Channel;
        public int CurrentIndex;
        public ObservableCollection<Types.audio> Playlist = new ObservableCollection<Types.audio>();
        private static int _endSync = 0;
        private static SYNCPROC _endSyncCallback;
        
        public PlayerWindow()
        {
            InitializeComponent();
            BassNet.Registration("mc-shura@yandex.ua", "2X2223183433");
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
        }

        public void Play(Types.audio audio)
        {
            if (Channel != 0) Bass.BASS_ChannelStop(Channel);
            Channel = Bass.BASS_StreamCreateURL(audio.url, 0, BASSFlag.BASS_STREAM_AUTOFREE, null, IntPtr.Zero);
            _endSyncCallback = new SYNCPROC(EndSync);
            _endSync = Bass.BASS_ChannelSetSync(Channel, BASSSync.BASS_SYNC_END | BASSSync.BASS_SYNC_MIXTIME, 0, _endSyncCallback, IntPtr.Zero);
            Bass.BASS_ChannelPlay(Channel, true);
        }
        public void PlayPause()
        {
            if (Channel != 0)
            {
                if (Bass.BASS_ChannelIsActive(Channel) == BASSActive.BASS_ACTIVE_PAUSED)
                    Bass.BASS_ChannelPlay(Channel, false);
                else if (Bass.BASS_ChannelIsActive(Channel) == BASSActive.BASS_ACTIVE_PLAYING)
                    Bass.BASS_ChannelPause(Channel);
            }
        }
        public void PlayNext()
        {
            if (++CurrentIndex >= Playlist.Count) CurrentIndex = 0;
            Play(Playlist[CurrentIndex]);
        }
        public void PlayPrev()
        {
            if (--CurrentIndex <= 0) CurrentIndex = Playlist.Count - 1;
            Play(Playlist[CurrentIndex]);
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
    }
}
