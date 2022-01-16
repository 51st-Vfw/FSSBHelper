// ********************************************************************************************
//
// PrefsUI.cs: FSSBHelper Preferences/Settings UI
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
// ********************************************************************************************

using System;
using System.Windows.Forms;

namespace FSSBHelper
{
    public partial class PrefsUI : Form
    {
        private JoystickMonitor _monitor;
        private bool _isUILoaded;
        private const int _SampleScale = 25;

        public PrefsUI(JoystickMonitor monitor)
        {
            _isUILoaded = false;
            _monitor = monitor;
            InitializeComponent();

            this.SizeChanged += new EventHandler(PrefsUI_SizeChanged);

            _monitor.DCSStatusChanged += _monitor_DCSStatusChanged;
        }

        private void _monitor_DCSStatusChanged(object sender, EventArgs e)
        {
            UpdateDCSStatus();
        }

        public void UpdateDCSStatus()
        {
            if (!_monitor.IsDeviceValid)
            {
                uxLabelStatus.Text = "FSSBHelper is inactive, invalid device";
                uxIconNotify.Text = "FSSBHelper: Inactive (Device Invalid)";
                uxComboDevice.SelectedIndex = -1; //reset!
            }
            else if (!_monitor.Settings.EnableMonitorDCS)
            {
                uxLabelStatus.Text = "FSSBHelper is active";
                uxIconNotify.Text = "FSSBHelper: Active";
            }
            else if (_monitor.IsDCSRunning)
            {
                uxLabelStatus.Text = "FSSBHelper is active, DCS is currently running";
                uxIconNotify.Text = "FSSBHelper: Active, DCS Running";
            }
            else
            {
                uxLabelStatus.Text = "FSSBHelper is idle, DCS is not currently running";
                uxIconNotify.Text = "FSSBHelper: Idle, DCS Not Running";
            }
        }

        private void PrefsUI_Load(object sender, EventArgs e)
        {
            string[] cues = new string[] { "250Hz Tone", "350Hz Tone", "450Hz Tone",
                                           "550Hz Tone", "Beep 1", "Beep 2", "Beep 3" };

            _isUILoaded = false;

            // ---- Device Widgets

            uxComboDevice.Items.Clear();
            foreach (String name in _monitor.Devices)
                uxComboDevice.Items.Add(name);

            if (uxComboDevice.Items.Contains(_monitor.Settings.DeviceName))
                uxComboDevice.SelectedIndex = uxComboDevice.Items.IndexOf(_monitor.Settings.DeviceName);
            else
                uxComboDevice.SelectedIndex = -1;

            uxCheckMonitorDCS.Checked = _monitor.Settings.EnableMonitorDCS;
            uxCheckDecouple.Checked = _monitor.Settings.EnableDecoupledMode;

            uxSliderSamplePeriod.Value = _monitor.Settings.SamplePeriodMs / _SampleScale;
            uxSliderSamplePeriod_Scroll(null, null);

            // ---- Threshold Widgets

            uxCheckThreshold.Checked = _monitor.Settings.EnableThreshold;
            SyncThresholdWidgetsEnableState();

            uxSliderThreshold.Value = _monitor.Settings.Threshold;
            uxSliderThreshold_Scroll(null, null);

            uxSliderThresholdVol.Value = _monitor.Settings.ThresholdVol;
            uxSliderThresholdVol_Scroll(null, null);

            uxComboThresholdCue.Items.Clear();
            uxComboThresholdCue.Items.AddRange(cues);
            if (uxComboThresholdCue.Items.Contains(_monitor.Settings.ThresholdCue))
                uxComboThresholdCue.SelectedIndex = uxComboThresholdCue.Items.IndexOf(_monitor.Settings.ThresholdCue);
            else
                uxComboThresholdCue.SelectedIndex = -1;

            // ---- Limit Widgets

            uxCheckLimit.Checked = _monitor.Settings.EnableLimit;
            SyncLimitWidgetsEnableState();

            uxSliderLimitVol.Value = _monitor.Settings.LimitVol;
            uxSliderLimitVol_Scroll(null, null);

            uxComboLimitCue.Items.Clear();
            uxComboLimitCue.Items.AddRange(cues);
            if (uxComboLimitCue.Items.Contains(_monitor.Settings.LimitCue))
                uxComboLimitCue.SelectedIndex = uxComboThresholdCue.Items.IndexOf(_monitor.Settings.LimitCue);
            else
                uxComboLimitCue.SelectedIndex = -1;

            // ---- Miscellaneous Widgets

            UpdateDCSStatus();

            _monitor.BeginTimedMonitors(); //we are ready to start!

            _isUILoaded = true;
        }

        // ---- Device Widgets

        private void uxComboDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isUILoaded && (uxComboDevice.SelectedIndex != -1))
            {
                var name = uxComboDevice.SelectedItem.ToString();
                _monitor.Settings.DeviceName = name;
                _monitor.Settings.Persist();
                _monitor.UpdatedDevice(name);
            }
            else if (_isUILoaded)
            {
                _monitor.UpdatedDevice(null);
            }
        }

        private void uxCheckMonitorDCS_CheckedChanged(object sender, EventArgs e)
        {
            if (_isUILoaded)
            {
                var isEnabled = uxCheckMonitorDCS.Checked;
                _monitor.Settings.EnableMonitorDCS = isEnabled;
                _monitor.Settings.Persist();
                _monitor.BeginTimedMonitors();
            }
        }

        private void uxSliderSamplePeriod_Scroll(object sender, EventArgs e)
        {
            var ms = uxSliderSamplePeriod.Value * _SampleScale;
            uxLabelSamplePeriodVal.Text = ms.ToString() + " ms";

            if (_isUILoaded)
            {
                _monitor.Settings.SamplePeriodMs = ms;
                _monitor.Settings.Persist();
                _monitor.UpdatedSampleRate();
            }
        }

        private void uxCheckDecouple_CheckedChanged(object sender, EventArgs e)
        {
            if (_isUILoaded)
            {
                var isEnabled = uxCheckDecouple.Checked;
                _monitor.Settings.EnableDecoupledMode = isEnabled;
                _monitor.Settings.Persist();

                // TODO: it would be nice if we didn't have to restart, consider dynamically
                // TODO: changing the JoystickMonitor that PrefsUI uses.

                string message = "FSSBHelper must be restarted for this change to take effect.\n"
                               + "Please quit and re-launch FSSBHelper to apply the change.";
                string title = "Restart FSSBHelper";
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ---- Threshold Widgets

        private bool SyncThresholdWidgetsEnableState()
        {
            var isEnabled = uxCheckThreshold.Checked;

            uxLabelThreshold.Enabled = isEnabled;
            uxLabelThresholdVal.Enabled = isEnabled;
            uxSliderThreshold.Enabled = isEnabled;

            uxLabelThresholdCue.Enabled = isEnabled;
            uxComboThresholdCue.Enabled = isEnabled;
            uxButtonThresholdCue.Enabled = isEnabled;

            uxLabelThresholdVol.Enabled = isEnabled;
            uxSliderThresholdVol.Enabled = isEnabled;

            return isEnabled;
        }

        private void uxCheckThreshold_CheckedChanged(object sender, EventArgs e)
        {
            var isEnabled = SyncThresholdWidgetsEnableState();

            if (_isUILoaded)
            {
                _monitor.Settings.EnableThreshold = isEnabled;
                _monitor.Settings.Persist();
                _monitor.UpdatedThresholdCues(isEnabled, JoystickMonitor.CueUpdates.All);
            }
        }

        private void uxSliderThreshold_Scroll(object sender, EventArgs e)
        {
            var pctg = uxSliderThreshold.Value;
            uxLabelThresholdVal.Text = pctg.ToString() + "%";

            if (_isUILoaded)
            {
                _monitor.Settings.Threshold = pctg;
                _monitor.Settings.Persist();
                _monitor.UpdatedThresholdCues(uxCheckThreshold.Checked, JoystickMonitor.CueUpdates.Threshold);
            }
        }

        private void uxComboThresholdCue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isUILoaded && (uxComboThresholdCue.SelectedIndex != -1))
            {
                var cue = uxComboThresholdCue.SelectedItem.ToString();
                _monitor.Settings.ThresholdCue = cue;
                _monitor.Settings.Persist();
                _monitor.UpdatedThresholdCues(uxCheckThreshold.Checked, JoystickMonitor.CueUpdates.Name);
            }
            else
            {
// TODO: bogus cue?
            }
        }

        private void uxSliderThresholdVol_Scroll(object sender, EventArgs e)
        {
            if (_isUILoaded)
            {
                var pctg = uxSliderThresholdVol.Value;
                _monitor.Settings.ThresholdVol = pctg;
                _monitor.Settings.Persist();
                _monitor.UpdatedThresholdCues(uxCheckThreshold.Checked, JoystickMonitor.CueUpdates.Volume);
            }
        }

        private void uxButtonThresholdCue_Click(object sender, EventArgs e)
        {
            var filename = _monitor.Settings.FilenameForCue(_monitor.Settings.ThresholdCue);
            var mp = new AudioHelper(filename, _monitor.Settings.ThresholdVol, true);
            mp.Play();
        }

        // ---- Limit Widgets

        private bool SyncLimitWidgetsEnableState()
        {
            var isEnabled = uxCheckLimit.Checked;

            uxLabelLimitCue.Enabled = isEnabled;
            uxComboLimitCue.Enabled = isEnabled;
            uxButtonLimitCue.Enabled = isEnabled;

            uxLabelLimitVol.Enabled = isEnabled;
            uxSliderLimitVol.Enabled = isEnabled;

            return isEnabled;
        }

        private void uxCheckLimit_CheckedChanged(object sender, EventArgs e)
        {
            var isEnabled = SyncLimitWidgetsEnableState();

            if (_isUILoaded)
            {
                _monitor.Settings.EnableLimit = isEnabled;
                _monitor.Settings.Persist();
                _monitor.UpdatedLimitCues(isEnabled, JoystickMonitor.CueUpdates.All);
            }
        }

        private void uxComboLimitCue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isUILoaded && (uxComboLimitCue.SelectedIndex != -1))
            {
                var cue = uxComboLimitCue.SelectedItem.ToString();
                _monitor.Settings.LimitCue = cue;
                _monitor.Settings.Persist();
                _monitor.UpdatedLimitCues(uxCheckLimit.Checked, JoystickMonitor.CueUpdates.Name);
            }
            else
            {
// TODO: handle bogus cue?
            }
        }

        private void uxSliderLimitVol_Scroll(object sender, EventArgs e)
        {
            if (_isUILoaded)
            {
                var pctg = uxSliderLimitVol.Value;
                _monitor.Settings.LimitVol = pctg;
                _monitor.Settings.Persist();
                _monitor.UpdatedLimitCues(uxCheckLimit.Checked, JoystickMonitor.CueUpdates.Volume);
            }
        }

        private void uxButtonLimitCue_Click(object sender, EventArgs e)
        {
            var filename = _monitor.Settings.FilenameForCue(_monitor.Settings.LimitCue);
            var mp = new AudioHelper(filename, _monitor.Settings.LimitVol, true);
            mp.Play();
        }

        private void uxIconNotify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void PrefsUI_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                uxIconNotify.Visible = true;
                this.ShowInTaskbar = false;
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                uxIconNotify.Visible = false;
                this.ShowInTaskbar = true;
            }
        }

        private void uxComboDevice_DropDown(object sender, EventArgs e)
        {
            if (uxComboDevice.SelectedIndex > -1)
                return;

            uxComboDevice.Items.Clear();
            foreach (String name in _monitor.Devices)
                uxComboDevice.Items.Add(name);

            if (uxComboDevice.Items.Contains(_monitor.Settings.DeviceName)) //re-init as it was now detected...
            {
                uxComboDevice.SelectedIndex = uxComboDevice.Items.IndexOf(_monitor.Settings.DeviceName);
                _monitor.UpdatedDevice(_monitor.Settings.DeviceName);
            }
        }
    }
}
