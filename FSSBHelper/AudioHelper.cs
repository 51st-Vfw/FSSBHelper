// ********************************************************************************************
//
// AudioHelper.cs: FSSBHelper Audio Functionality
//
// Copyright (c) 2021 pch07 / Rage
// Copyright (C) 2021-22 twillis / ilominar / Raven
//
// This program is free software: you can redistribute it and/or modify it under the terms of
// the GNU General Public License as published by the Free Software Foundation, either version
// 3 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with this program.
// If not, see <https://www.gnu.org/licenses/>.
//
// ******************************************************************************************

using System;
using System.Windows.Media;

namespace FSSBHelper
{
    public sealed class AudioHelper : IDisposable
    {
        private MediaPlayer _audioPlayer;

        public int Volume
        {
            get { return (int)(_audioPlayer.Volume * 100.0); }
            set { _audioPlayer.Volume = (double)value * 0.01; }
        }

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
