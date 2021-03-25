using System;
using System.Timers;

namespace FSSBHelper
{
    public sealed class JoystickMonitor : IDisposable
    {
        private const int _Max = 65535;

        private Settings _Settings;
        private Timer _timer;
        private JoystickHelper _JoystickHelper;
        private AudioHelper _AudioHelper;
        private int _AlertMin;
        private int _AlertMax;

        public JoystickMonitor(JoystickHelper joystickHelper, AudioHelper audioHelper, Settings settings) 
        {
            _JoystickHelper = joystickHelper;
            _AudioHelper = audioHelper;
            _Settings = settings;

            _AlertMax = (int)(Math.Round((_Settings.MaxPercent * 0.01) * _Max));
            _AlertMin = _Max - _AlertMax;
            
            _timer = new Timer(_Settings.IntervalMS);
            _timer.Elapsed += _timer_Elapsed;
        }

        public void Start() => _timer.Start();

        private bool IsAlert(double value) => value >= _AlertMax || value <= _AlertMin;

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var axis = _JoystickHelper.Axis;
            
            if (_Settings.Debug)
                Console.WriteLine($"x: {axis.X} y: {axis.Y}"); 

            if (IsAlert(axis.X) || IsAlert(axis.Y))
                _AudioHelper.Play();
        }

        public void Dispose() => _timer.Dispose();

    }
}
