using System;
using System.Windows.Media;

namespace FSSBHelper
{
    public sealed class AudioHelper : IDisposable
    {
        private MediaPlayer _audioPlayer;

        public AudioHelper(string filename, int volume, bool isOneShot)
        {
            _audioPlayer = new MediaPlayer();
            //
            // TODO: For some reason, pack URIs (which should handle this type of thing) don't work?
            // TODO: So, fix this by figuring out the .exe's directory and building the path by hand.
            //
            string exePath = System.AppDomain.CurrentDomain.BaseDirectory;
            Uri uriAudioFile = new Uri(exePath + "Audio\\" + filename, UriKind.Absolute);
            _audioPlayer.Open(uriAudioFile);
            _audioPlayer.Volume = (double) volume * 0.01;

            if (isOneShot)
                _audioPlayer.MediaEnded += new EventHandler(_audioPlayerOneShot_MediaEnded);
            else
                _audioPlayer.MediaEnded += new EventHandler(_audioPlayer_MediaEnded);
        }
        public void Dispose()
        {
            _audioPlayer.Stop();
            _audioPlayer.Close();
        }

        public int Volume { get { return (int) (_audioPlayer.Volume * 100.0); }
                            set { _audioPlayer.Volume = (double) value * 0.01; } }

        public void Play()
        {
            _audioPlayer.Play();
        }

        private void _audioPlayer_MediaEnded(object sender, EventArgs e)
        {
            _audioPlayer.Stop();
            _audioPlayer.Position = TimeSpan.Zero;
        }

        private void _audioPlayerOneShot_MediaEnded(object sender, EventArgs e)
        {
            _audioPlayer.Stop();
            _audioPlayer.Close();
        }

    }
}
