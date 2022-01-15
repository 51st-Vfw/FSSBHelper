// ********************************************************************************************
//
// JoystickMonitor.cs: FSSBHelper Joystick Monitor Functionality
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

// Define this macro to enable logging output.
//
#undef DEBUG_LOGGING

using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

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

        public Settings Settings { get; private set; }
        public bool IsDCSRunning { get; private set; }
        public bool IsDeviceValid { get { return _joystick != null; } }
        public string[] Devices { get { return JoystickHelper.GetActiveJoysticks(); } }

        /// <summary>
        /// Will trigger when DCS has changed from Active to In-Active or vice-versa
        /// Use IsDCSRunning for actual current state
        /// </summary>
        public event EventHandler DCSStatusChanged;

        private readonly System.Windows.Forms.Timer _timerCheckDCS;
        private readonly System.Windows.Forms.Timer _timerSample;
        private JoystickHelper _joystick;
        private AudioHelper _audioThreshold;
        private AudioHelper _audioLimit;
        private int _alertLowerMax;
        private int _alertUpperMin;

        public JoystickMonitor()
        {
            Settings = new Settings(); //first!
            UpdatedThresholdCues(Settings.EnableThreshold, CueUpdates.All);
            UpdatedLimitCues(Settings.EnableLimit, CueUpdates.All);
            AssignDevice(Settings.DeviceName);

            _timerSample = new System.Windows.Forms.Timer
            {
                Interval = Settings.SamplePeriodMs
            };
            _timerSample.Tick += new EventHandler(_timerSample_Tick);

            _timerCheckDCS = new System.Windows.Forms.Timer
            {
                Interval = _intervalCheckDCS
            };
            _timerCheckDCS.Tick += new EventHandler(_timerCheckDCS_Tick);

        }

        public void Dispose()
        {
            _timerCheckDCS.Stop();
            _timerCheckDCS.Dispose();

            _timerSample.Stop();
            _timerSample.Dispose();

            _joystick?.Dispose();

            _audioThreshold?.Dispose();
            _audioLimit?.Dispose();
        }

        /// <summary>
        /// Device to monitor has been changed. Attempt to create a new joystick helper to handle interactions with the joystick.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="isInit"></param>
        public void UpdatedDevice(string deviceName)
        {
            AssignDevice(deviceName);
            BeginTimedMonitors(); //restart state 
        }

        /// <summary>
        /// Setup and process the appropriate timer(s) based on current preferences
        /// </summary>
        public void BeginTimedMonitors()
        {
            _timerCheckDCS.Stop();
            _timerSample.Stop();
            IsDCSRunning = false;

            if (DCSStatusChanged != null)
                DCSStatusChanged(this, new EventArgs());

            if (Settings.EnableMonitorDCS)
                _timerCheckDCS.Start();
            else
                _timerSample.Start();
        }

        /// <summary>
        /// Retrieve current preference and assign to timer
        /// </summary>
        public void UpdatedSampleRate()
        {
            _timerSample.Interval = Settings.SamplePeriodMs;
        }

        /// <summary>
        /// Threshold cue settings have changed. Release any previously allocated AudioHelper
        /// that was handling the threshold audio and allocate a new helper with the new
        /// configuration. Update the min/max bounds of the threshold.
        /// </summary>
        /// <param name="isEnabled"></param>
        /// <param name="updated"></param>
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

        /// <summary>
        /// Limit cue settings have changed. Release any previously allocated AudioHelper that
        /// was handling the limit audio and allocate a new helper with the new configuration.
        /// </summary>
        /// <param name="isEnabled"></param>
        /// <param name="updated"></param>
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

        private void AssignDevice(string deviceName)
        {
            if (_joystick != null)
            {
                _joystick.Dispose();
                _joystick = null;
            }

            if (deviceName != null && Devices.Contains(deviceName))
                _joystick = new JoystickHelper(deviceName);
        }

        /// <summary>
        /// Check to see if DCS is running, starting or stopping the main sampling timer as necessary.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timerCheckDCS_Tick(object sender, EventArgs e)
        {
            var processDCS = Process.GetProcessesByName("DCS");
            if (!IsDCSRunning && (processDCS.Length > 0))
            {
                IsDCSRunning = true;
                _timerSample.Start();

                DCSStatusChanged(this, new EventArgs());
            }
            else if (IsDCSRunning && (processDCS.Length == 0))
            {
                IsDCSRunning = false;
                _timerSample.Stop();

                DCSStatusChanged(this, new EventArgs());
            }

        }

        /// <summary>
        /// Process the actual audio alert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timerSample_Tick(object sender, EventArgs e)
        {
            var axis = new Vector();
            try
            {
                if (_audioLimit == null && _audioThreshold == null)
                    throw new NullReferenceException("Audio");

                axis = _joystick.Axis; //nre ok

#if DEBUG_LOGGING
                Console.WriteLine($"x {axis.X}, y {axis.Y} | al: v < {_alertLowerMax}, au: v > {_alertUpperMin}");
#endif
            }
            catch (Exception ex) //joystick is not currently reachable... (ie was unplugged)
            {

#if DEBUG_LOGGING
                Console.WriteLine(ex.Message);
#endif

                _timerSample.Stop();

                _joystick?.Dispose();
                _joystick = null; //reset entirely

                DCSStatusChanged(this, new EventArgs());

                return;
            }

            if ((_audioLimit != null) && (IsLimit(axis.X) || IsLimit(axis.Y)))
                _audioLimit.Play();
            else if ((_audioThreshold != null) && (IsAlert(axis.X) || IsAlert(axis.Y)))
                _audioThreshold.Play();

        }
    }
}
