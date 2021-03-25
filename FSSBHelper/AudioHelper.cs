using System;

namespace FSSBHelper
{
    public sealed class AudioHelper : IDisposable
    {
        private readonly Settings Settings;

        public AudioHelper(Settings settings) 
        {
            Settings = settings;
        }

        public void Play() => Console.Beep(Settings.AlertHz, Settings.AlertDuration); //todo: something better...tone/volume/left/right control...?

        public void Dispose() { }
    }
}
