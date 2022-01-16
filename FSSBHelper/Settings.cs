// ********************************************************************************************
//
// Settings.cs: FSSBHelper Settings Abstraction
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
using System.Configuration;

namespace FSSBHelper
{
    public sealed class Settings
    {
        private const string keyDeviceName = "DeviceName";
        private const string keyEnableMonitorDCS = "EnableMonitorDCS";
        private const string keyEnableDecoupledMode = "EnableDecoupledMode";
        private const string keySamplePeriodMs = "SamplePeriodMs";
        private const string keyEnableThreshold = "EnableThreshold";
        private const string keyThreshold = "Threshold";
        private const string keyThresholdCue = "ThresholdCue";
        private const string keyThresholdVol = "ThresholdVol";
        private const string keyEnableLimit = "EnableLimit";
        private const string keyLimitCue = "LimitCue";
        private const string keyLimitVol = "LimitVol";

        public string DeviceName { get; set; }
        public bool EnableMonitorDCS { get; set; }
        public bool EnableDecoupledMode { get; set; }
        public int SamplePeriodMs { get; set; }

        public bool EnableThreshold { get; set; }
        public int Threshold { get; set; }
        public string ThresholdCue { get; set; }
        public int ThresholdVol { get; set; }
        
        public bool EnableLimit { get; set; }
        public string LimitCue { get; set; }
        public int LimitVol { get; set; }

        public Settings()
        {
            // Set up the default settings if no settings are available.
            //
            AddUpdateAppSettings(keyDeviceName, "FSSB R3L MJF SGRH", false);
            AddUpdateAppSettings(keyEnableMonitorDCS, "false", false);
            AddUpdateAppSettings(keyEnableDecoupledMode, "false", false);
            AddUpdateAppSettings(keySamplePeriodMs, "250", false);
            AddUpdateAppSettings(keyEnableThreshold, "true", false);
            AddUpdateAppSettings(keyThreshold, "80", false);
            AddUpdateAppSettings(keyThresholdCue, "350Hz Tone", false);
            AddUpdateAppSettings(keyThresholdVol, "50", false);
            AddUpdateAppSettings(keyEnableLimit, "true", false);
            AddUpdateAppSettings(keyLimitVol, "50", false);
            AddUpdateAppSettings(keyLimitCue, "350Hz Tone", false);

            // Set up instance properties with the settings values.
            //
            DeviceName = ConfigurationManager.AppSettings[keyDeviceName].ToString();
            EnableMonitorDCS = bool.Parse(ConfigurationManager.AppSettings[keyEnableMonitorDCS].ToString());
            EnableDecoupledMode = bool.Parse(ConfigurationManager.AppSettings[keyEnableDecoupledMode].ToString());
            SamplePeriodMs = int.Parse(ConfigurationManager.AppSettings[keySamplePeriodMs].ToString());
            EnableThreshold = bool.Parse(ConfigurationManager.AppSettings[keyEnableThreshold].ToString());
            Threshold = int.Parse(ConfigurationManager.AppSettings[keyThreshold].ToString());
            ThresholdCue = ConfigurationManager.AppSettings[keyThresholdCue].ToString();
            ThresholdVol = int.Parse(ConfigurationManager.AppSettings[keyThresholdVol].ToString());
            EnableLimit = bool.Parse(ConfigurationManager.AppSettings[keyEnableLimit].ToString());
            LimitCue = ConfigurationManager.AppSettings[keyLimitCue].ToString();
            LimitVol = int.Parse(ConfigurationManager.AppSettings[keyLimitVol].ToString());
        }

        // Persist the settings curently in the instance properties to the configuration manager.
        //
        public void Persist()
        {
            AddUpdateAppSettings(keyDeviceName, DeviceName, true);
            AddUpdateAppSettings(keyEnableMonitorDCS, EnableMonitorDCS.ToString(), true);
            AddUpdateAppSettings(keyEnableDecoupledMode, EnableDecoupledMode.ToString(), true);
            AddUpdateAppSettings(keySamplePeriodMs, SamplePeriodMs.ToString(), true);
            AddUpdateAppSettings(keyEnableThreshold, EnableThreshold.ToString(), true);
            AddUpdateAppSettings(keyThreshold, Threshold.ToString(), true);
            AddUpdateAppSettings(keyThresholdCue, ThresholdCue.ToString(), true);
            AddUpdateAppSettings(keyThresholdVol, ThresholdVol.ToString(), true);
            AddUpdateAppSettings(keyEnableLimit, EnableLimit.ToString(), true);
            AddUpdateAppSettings(keyLimitCue, LimitCue.ToString(), true);
            AddUpdateAppSettings(keyLimitVol, LimitVol.ToString(), true);
        }

        public string FilenameForCue(string cue)
        {
            return "Audio_" + cue.Replace(" ", "_") + ".wav";
        }

        private void AddUpdateAppSettings(string key, string value, bool isUpdate)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                    settings.Add(key, value);
                else if (isUpdate)
                    settings[key].Value = value;
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
// TODO: some kind of error UX here?
                Console.WriteLine("FSSBHelper: Error writing app settings");
            }
        }
    }
}
