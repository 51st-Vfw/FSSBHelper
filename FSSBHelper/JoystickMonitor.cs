// Define this macro to enable logging output.
//
#undef DEBUG_LOGGING

using System;
using System.Collections.Generic;
using System.Diagnostics;

using SharpDX.DirectInput;

namespace FSSBHelper
{
    public sealed class JoystickMonitor : IDisposable
    {
        [Flags]
        public enum CueUpdates
        {
            Name,
            Volume,
            Threshold,
            All = Name | Volume | Threshold
        };

        private const int _intervalCheckDCS = 15000;        // 15s between DCS checks
        private const int _maxInt = 65535;

        public PrefsUI PrefsUI { get; private set; }
        public Settings Settings { get; private set; }
        public System.Collections.Generic.IList<String> DeviceNames { get; private set; }

        private System.Windows.Forms.Timer _timerCheckDCS;
        private System.Windows.Forms.Timer _timerSample;
        private JoystickHelper _joystick;
        private AudioHelper _audioThreshold;
        private AudioHelper _audioLimit;
        private bool _isDCSRunning;
        private int _samplePeriodMs;
        private int _alertLowerMax;
        private int _alertUpperMin;
        
        public bool IsDeviceValid { get { return (_joystick != null); } }

        public JoystickMonitor()
        {
            _timerCheckDCS = null;

            _timerSample = new System.Windows.Forms.Timer();
            _timerSample.Tick += new EventHandler(_timerSample_Tick);

            Settings = new Settings();

            var dinput = new DirectInput();
            var devices = dinput.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly);
            DeviceNames = new List<String>();
            foreach (DeviceInstance device in devices)
                DeviceNames.Add(device.InstanceName);
            dinput.Dispose();

            PrefsUI = new PrefsUI(this);

            UpdatedDevice(Settings.DeviceName);
            UpdatedSampleRate();
            UpdatedMonitorDCS();
            UpdatedThresholdCues(Settings.EnableThreshold, CueUpdates.All);
            UpdatedLimitCues(Settings.EnableLimit, CueUpdates.All);
        }

        public void Dispose()
        {
            _timerCheckDCS?.Stop();
            _timerCheckDCS?.Dispose();

            _timerSample?.Stop();
            _timerSample?.Dispose();

            _joystick?.Dispose();

            _audioThreshold?.Dispose();
            _audioLimit?.Dispose();
        }

        // Device to monitor has been changed. Attempt to create a new joystick helper to
        // handle interactions with the joystick.
        //
        public void UpdatedDevice(string deviceName)
        {
            if (_joystick != null)
                _joystick.Dispose();
            if ((deviceName != null) && DeviceNames.Contains(deviceName))
                _joystick = new JoystickHelper(deviceName);
            else
                _joystick = null;
        }

        // TODO
        //
        public void UpdatedMonitorDCS()
        {
            if (Settings.EnableMonitorDCS && (_timerCheckDCS == null))
            {
                _isDCSRunning = false;

                _timerCheckDCS = new System.Windows.Forms.Timer();
                _timerCheckDCS.Interval = _intervalCheckDCS;
                _timerCheckDCS.Tick += new EventHandler(_timerCheckDCS_Tick);
                _timerCheckDCS.Start();

                _timerSample.Stop();
            }
            else if (!Settings.EnableMonitorDCS)
            {
                _isDCSRunning = false;

                if (_timerCheckDCS != null)
                {
                    _timerCheckDCS.Stop();
                    _timerCheckDCS.Dispose();
                    _timerCheckDCS = null;
                }

                _timerSample.Interval = _samplePeriodMs;
                _timerSample.Start();
            }
            PrefsUI.UpdateDCSStatus(_isDCSRunning);
        }

        // TODO
        //
        public void UpdatedSampleRate()
        {
            _samplePeriodMs = Settings.SamplePeriodMs;
        }

        // Threshold cue settings have changed. Release any previously allocated AudioHelper
        // that was handling the threshold audio and allocate a new helper with the new
        // configuration. Update the min/max bounds of the threshold.
        //
        public void UpdatedThresholdCues(bool isEnabled, CueUpdates updated)
        {
            if ((updated == CueUpdates.All) || ((updated & CueUpdates.Threshold) == CueUpdates.Threshold))
            {
                var steps = (int) Math.Round((Settings.Threshold * 0.01) * (_maxInt / 2));
                _alertLowerMax = (_maxInt / 2) - steps;
                _alertUpperMin = (_maxInt / 2) + steps;
            }

            if ((updated == CueUpdates.All) || ((updated & CueUpdates.Name) == CueUpdates.Name))
            {
                _audioThreshold?.Dispose();
                _audioThreshold = null;
            }

            if (isEnabled)
            {
                if ((updated == CueUpdates.All) || ((updated & CueUpdates.Name) == CueUpdates.Name))
                {
                    var filename = Settings.FilenameForCue(Settings.ThresholdCue);
                    _audioThreshold = new AudioHelper(filename, Settings.ThresholdVol, false);
                }
                else if ((updated & CueUpdates.Volume) == CueUpdates.Volume)
                {
                    _audioThreshold.Volume = Settings.ThresholdVol;
                }
            }

#if DEBUG_LOGGING
            Console.WriteLine($"UpdatedThresholdCues: alert [{_alertLowerMax}, {_alertUpperMin}], cue {Settings.ThresholdCue}, vol {Settings.ThresholdVol}");
#endif
        }

        // Limit cue settings have changed. Release any previously allocated AudioHelper that
        // was handling the limit audio and allocate a new helper with the new configuration.
        //
        public void UpdatedLimitCues(bool isEnabled, CueUpdates updated)
        {
            if ((updated == CueUpdates.All) || ((updated & CueUpdates.Name) == CueUpdates.Name))
            {
                _audioLimit?.Dispose();
                _audioLimit = null;
            }

            if (isEnabled)
            {
                if ((updated == CueUpdates.All) || ((updated & CueUpdates.Name) == CueUpdates.Name))
                {
                    var filename = Settings.FilenameForCue(Settings.LimitCue);
                    _audioLimit = new AudioHelper(filename, Settings.LimitVol, false);
                }
                else if ((updated & CueUpdates.Volume) == CueUpdates.Volume)
                {
                    _audioLimit.Volume = Settings.LimitVol;
                }
            }

#if DEBUG_LOGGING
            Console.WriteLine($"UpdatedLimitCues: cue {Settings.LimitCue}, vol {Settings.LimitVol}");
#endif
        }

        private bool IsAlert(double value) => ((value < _alertLowerMax) || (value > _alertUpperMin));
        private bool IsLimit(double value) => ((value == 0) || (value == _maxInt));

        // Check to see if DCS is running, starting or stopping the main sampling timer as
        // necessary.
        //
        private void _timerCheckDCS_Tick(object sender, EventArgs e)
        {
            Process[] processDCS = Process.GetProcessesByName("DCS");
            if (!_isDCSRunning && (processDCS.Length > 0))
            {
                // Console.WriteLine("Idle --> DCS Running");
                _isDCSRunning = true;
                _timerSample.Interval = _samplePeriodMs;
                _timerSample.Start();
                PrefsUI.UpdateDCSStatus(_isDCSRunning);
            }
            else if (_isDCSRunning && (processDCS.Length == 0))
            {
                // Console.WriteLine("DCS Running --> Idle");
                _isDCSRunning = false;
                _timerSample.Stop();
                PrefsUI.UpdateDCSStatus(_isDCSRunning);
            }
        }

        // TODO
        //
        private void _timerSample_Tick(object sender, EventArgs e)
        {
            if (_joystick != null)
            {
                _timerSample.Stop();

                var axis = _joystick.Axis;
                if ((_audioLimit != null) && (IsLimit(axis.X) || IsLimit(axis.Y)))
                {
                    _audioLimit.Play();
                    _timerSample.Interval = 300;
                }
                else if ((_audioThreshold != null) && (IsAlert(axis.X) || IsAlert(axis.Y)))
                {
                    _audioThreshold.Play();
                    _timerSample.Interval = 500;
                }
                else
                {
                    _timerSample.Interval = _samplePeriodMs;
                }

                _timerSample.Start();

#if DEBUG_LOGGING
                Console.WriteLine($"x {axis.X}, y {axis.Y} | al: v < {_alertLowerMax}, au: v > {_alertUpperMin}");
#endif
            }
        }
    }
}
