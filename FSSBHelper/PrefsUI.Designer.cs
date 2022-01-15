using System;

namespace FSSBHelper
{
    partial class PrefsUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrefsUI));
            this.uxIconNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.uxLabelDevice = new System.Windows.Forms.Label();
            this.uxComboDevice = new System.Windows.Forms.ComboBox();
            this.uxLabelStatus = new System.Windows.Forms.Label();
            this.uxLabelVersion = new System.Windows.Forms.Label();
            this.uxSliderSamplePeriod = new System.Windows.Forms.TrackBar();
            this.uxCheckThreshold = new System.Windows.Forms.CheckBox();
            this.uxCheckLimit = new System.Windows.Forms.CheckBox();
            this.uxGroupThreshold = new System.Windows.Forms.GroupBox();
            this.uxButtonThresholdCue = new System.Windows.Forms.Button();
            this.uxSliderThresholdVol = new System.Windows.Forms.TrackBar();
            this.uxComboThresholdCue = new System.Windows.Forms.ComboBox();
            this.uxSliderThreshold = new System.Windows.Forms.TrackBar();
            this.uxLabelThresholdCue = new System.Windows.Forms.Label();
            this.uxLabelThresholdVol = new System.Windows.Forms.Label();
            this.uxLabelThresholdVal = new System.Windows.Forms.Label();
            this.uxLabelThreshold = new System.Windows.Forms.Label();
            this.uxGroupLimit = new System.Windows.Forms.GroupBox();
            this.uxSliderLimitVol = new System.Windows.Forms.TrackBar();
            this.uxButtonLimitCue = new System.Windows.Forms.Button();
            this.uxComboLimitCue = new System.Windows.Forms.ComboBox();
            this.uxLabelLimitCue = new System.Windows.Forms.Label();
            this.uxLabelLimitVol = new System.Windows.Forms.Label();
            this.uxLabelSamplePeriod = new System.Windows.Forms.Label();
            this.uxLabelSamplePeriodVal = new System.Windows.Forms.Label();
            this.uxCheckMonitorDCS = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.uxSliderSamplePeriod)).BeginInit();
            this.uxGroupThreshold.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxSliderThresholdVol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxSliderThreshold)).BeginInit();
            this.uxGroupLimit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxSliderLimitVol)).BeginInit();
            this.SuspendLayout();
            // 
            // uxIconNotify
            // 
            this.uxIconNotify.BalloonTipText = "Beep... Beep... Beep...";
            this.uxIconNotify.BalloonTipTitle = "FSSBHelper";
            this.uxIconNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("uxIconNotify.Icon")));
            this.uxIconNotify.Text = "FSSBHelper";
            this.uxIconNotify.Visible = true;
            this.uxIconNotify.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.uxIconNotify_MouseDoubleClick);
            // 
            // uxLabelDevice
            // 
            this.uxLabelDevice.AutoSize = true;
            this.uxLabelDevice.Location = new System.Drawing.Point(19, 15);
            this.uxLabelDevice.Name = "uxLabelDevice";
            this.uxLabelDevice.Size = new System.Drawing.Size(71, 13);
            this.uxLabelDevice.TabIndex = 0;
            this.uxLabelDevice.Text = "FSSB Device";
            this.uxLabelDevice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uxComboDevice
            // 
            this.uxComboDevice.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uxComboDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uxComboDevice.FormattingEnabled = true;
            this.uxComboDevice.Location = new System.Drawing.Point(96, 12);
            this.uxComboDevice.Name = "uxComboDevice";
            this.uxComboDevice.Size = new System.Drawing.Size(293, 21);
            this.uxComboDevice.TabIndex = 1;
            this.uxComboDevice.DropDown += new System.EventHandler(this.uxComboDevice_DropDown);
            this.uxComboDevice.SelectedIndexChanged += new System.EventHandler(this.uxComboDevice_SelectedIndexChanged);
            // 
            // uxLabelStatus
            // 
            this.uxLabelStatus.AutoSize = true;
            this.uxLabelStatus.Location = new System.Drawing.Point(9, 377);
            this.uxLabelStatus.Name = "uxLabelStatus";
            this.uxLabelStatus.Size = new System.Drawing.Size(138, 13);
            this.uxLabelStatus.TabIndex = 0;
            this.uxLabelStatus.Text = "DCS is not currently running";
            this.uxLabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uxLabelVersion
            // 
            this.uxLabelVersion.AutoSize = true;
            this.uxLabelVersion.Location = new System.Drawing.Point(320, 377);
            this.uxLabelVersion.Name = "uxLabelVersion";
            this.uxLabelVersion.Size = new System.Drawing.Size(69, 13);
            this.uxLabelVersion.TabIndex = 0;
            this.uxLabelVersion.Text = "Version 2.0.2";
            this.uxLabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uxSliderSamplePeriod
            // 
            this.uxSliderSamplePeriod.LargeChange = 2;
            this.uxSliderSamplePeriod.Location = new System.Drawing.Point(96, 62);
            this.uxSliderSamplePeriod.Maximum = 40;
            this.uxSliderSamplePeriod.Minimum = 1;
            this.uxSliderSamplePeriod.Name = "uxSliderSamplePeriod";
            this.uxSliderSamplePeriod.Size = new System.Drawing.Size(240, 45);
            this.uxSliderSamplePeriod.TabIndex = 3;
            this.uxSliderSamplePeriod.Value = 2;
            this.uxSliderSamplePeriod.Scroll += new System.EventHandler(this.uxSliderSamplePeriod_Scroll);
            // 
            // uxCheckThreshold
            // 
            this.uxCheckThreshold.AutoSize = true;
            this.uxCheckThreshold.Location = new System.Drawing.Point(8, 0);
            this.uxCheckThreshold.Name = "uxCheckThreshold";
            this.uxCheckThreshold.Size = new System.Drawing.Size(215, 17);
            this.uxCheckThreshold.TabIndex = 4;
            this.uxCheckThreshold.Text = "Enable audio feedback above threshold";
            this.uxCheckThreshold.UseVisualStyleBackColor = true;
            this.uxCheckThreshold.CheckedChanged += new System.EventHandler(this.uxCheckThreshold_CheckedChanged);
            // 
            // uxCheckLimit
            // 
            this.uxCheckLimit.AutoSize = true;
            this.uxCheckLimit.Location = new System.Drawing.Point(8, -1);
            this.uxCheckLimit.Name = "uxCheckLimit";
            this.uxCheckLimit.Size = new System.Drawing.Size(168, 17);
            this.uxCheckLimit.TabIndex = 11;
            this.uxCheckLimit.Text = "Enable audio feedback at limit";
            this.uxCheckLimit.UseVisualStyleBackColor = true;
            this.uxCheckLimit.CheckedChanged += new System.EventHandler(this.uxCheckLimit_CheckedChanged);
            // 
            // uxGroupThreshold
            // 
            this.uxGroupThreshold.Controls.Add(this.uxButtonThresholdCue);
            this.uxGroupThreshold.Controls.Add(this.uxSliderThresholdVol);
            this.uxGroupThreshold.Controls.Add(this.uxComboThresholdCue);
            this.uxGroupThreshold.Controls.Add(this.uxSliderThreshold);
            this.uxGroupThreshold.Controls.Add(this.uxLabelThresholdCue);
            this.uxGroupThreshold.Controls.Add(this.uxLabelThresholdVol);
            this.uxGroupThreshold.Controls.Add(this.uxLabelThresholdVal);
            this.uxGroupThreshold.Controls.Add(this.uxLabelThreshold);
            this.uxGroupThreshold.Controls.Add(this.uxCheckThreshold);
            this.uxGroupThreshold.Location = new System.Drawing.Point(12, 117);
            this.uxGroupThreshold.Name = "uxGroupThreshold";
            this.uxGroupThreshold.Size = new System.Drawing.Size(377, 142);
            this.uxGroupThreshold.TabIndex = 8;
            this.uxGroupThreshold.TabStop = false;
            this.uxGroupThreshold.Text = "T";
            // 
            // uxButtonThresholdCue
            // 
            this.uxButtonThresholdCue.Location = new System.Drawing.Point(277, 62);
            this.uxButtonThresholdCue.Name = "uxButtonThresholdCue";
            this.uxButtonThresholdCue.Size = new System.Drawing.Size(92, 23);
            this.uxButtonThresholdCue.TabIndex = 9;
            this.uxButtonThresholdCue.Text = "Play Sample";
            this.uxButtonThresholdCue.UseVisualStyleBackColor = true;
            this.uxButtonThresholdCue.Click += new System.EventHandler(this.uxButtonThresholdCue_Click);
            // 
            // uxSliderThresholdVol
            // 
            this.uxSliderThresholdVol.Location = new System.Drawing.Point(66, 91);
            this.uxSliderThresholdVol.Maximum = 100;
            this.uxSliderThresholdVol.Name = "uxSliderThresholdVol";
            this.uxSliderThresholdVol.Size = new System.Drawing.Size(303, 45);
            this.uxSliderThresholdVol.TabIndex = 10;
            this.uxSliderThresholdVol.TickFrequency = 10;
            this.uxSliderThresholdVol.Scroll += new System.EventHandler(this.uxSliderThresholdVol_Scroll);
            // 
            // uxComboThresholdCue
            // 
            this.uxComboThresholdCue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uxComboThresholdCue.FormattingEnabled = true;
            this.uxComboThresholdCue.Location = new System.Drawing.Point(66, 64);
            this.uxComboThresholdCue.Name = "uxComboThresholdCue";
            this.uxComboThresholdCue.Size = new System.Drawing.Size(187, 21);
            this.uxComboThresholdCue.TabIndex = 8;
            this.uxComboThresholdCue.SelectedIndexChanged += new System.EventHandler(this.uxComboThresholdCue_SelectedIndexChanged);
            // 
            // uxSliderThreshold
            // 
            this.uxSliderThreshold.LargeChange = 10;
            this.uxSliderThreshold.Location = new System.Drawing.Point(66, 19);
            this.uxSliderThreshold.Maximum = 100;
            this.uxSliderThreshold.Name = "uxSliderThreshold";
            this.uxSliderThreshold.Size = new System.Drawing.Size(263, 45);
            this.uxSliderThreshold.TabIndex = 7;
            this.uxSliderThreshold.TickFrequency = 10;
            this.uxSliderThreshold.Scroll += new System.EventHandler(this.uxSliderThreshold_Scroll);
            // 
            // uxLabelThresholdCue
            // 
            this.uxLabelThresholdCue.AutoSize = true;
            this.uxLabelThresholdCue.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uxLabelThresholdCue.Location = new System.Drawing.Point(4, 67);
            this.uxLabelThresholdCue.Name = "uxLabelThresholdCue";
            this.uxLabelThresholdCue.Size = new System.Drawing.Size(56, 13);
            this.uxLabelThresholdCue.TabIndex = 0;
            this.uxLabelThresholdCue.Text = "Audio Cue";
            // 
            // uxLabelThresholdVol
            // 
            this.uxLabelThresholdVol.AutoSize = true;
            this.uxLabelThresholdVol.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uxLabelThresholdVol.Location = new System.Drawing.Point(18, 94);
            this.uxLabelThresholdVol.Name = "uxLabelThresholdVol";
            this.uxLabelThresholdVol.Size = new System.Drawing.Size(42, 13);
            this.uxLabelThresholdVol.TabIndex = 0;
            this.uxLabelThresholdVol.Text = "Volume";
            // 
            // uxLabelThresholdVal
            // 
            this.uxLabelThresholdVal.AutoSize = true;
            this.uxLabelThresholdVal.Location = new System.Drawing.Point(335, 23);
            this.uxLabelThresholdVal.Name = "uxLabelThresholdVal";
            this.uxLabelThresholdVal.Size = new System.Drawing.Size(33, 13);
            this.uxLabelThresholdVal.TabIndex = 0;
            this.uxLabelThresholdVal.Text = "100%";
            this.uxLabelThresholdVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uxLabelThreshold
            // 
            this.uxLabelThreshold.AutoSize = true;
            this.uxLabelThreshold.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uxLabelThreshold.Location = new System.Drawing.Point(6, 23);
            this.uxLabelThreshold.Name = "uxLabelThreshold";
            this.uxLabelThreshold.Size = new System.Drawing.Size(54, 13);
            this.uxLabelThreshold.TabIndex = 0;
            this.uxLabelThreshold.Text = "Threshold";
            this.uxLabelThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uxGroupLimit
            // 
            this.uxGroupLimit.Controls.Add(this.uxSliderLimitVol);
            this.uxGroupLimit.Controls.Add(this.uxButtonLimitCue);
            this.uxGroupLimit.Controls.Add(this.uxComboLimitCue);
            this.uxGroupLimit.Controls.Add(this.uxCheckLimit);
            this.uxGroupLimit.Controls.Add(this.uxLabelLimitCue);
            this.uxGroupLimit.Controls.Add(this.uxLabelLimitVol);
            this.uxGroupLimit.Location = new System.Drawing.Point(12, 265);
            this.uxGroupLimit.Name = "uxGroupLimit";
            this.uxGroupLimit.Size = new System.Drawing.Size(377, 101);
            this.uxGroupLimit.TabIndex = 9;
            this.uxGroupLimit.TabStop = false;
            this.uxGroupLimit.Text = "L";
            // 
            // uxSliderLimitVol
            // 
            this.uxSliderLimitVol.Location = new System.Drawing.Point(66, 50);
            this.uxSliderLimitVol.Maximum = 100;
            this.uxSliderLimitVol.Name = "uxSliderLimitVol";
            this.uxSliderLimitVol.Size = new System.Drawing.Size(302, 45);
            this.uxSliderLimitVol.TabIndex = 14;
            this.uxSliderLimitVol.TickFrequency = 10;
            this.uxSliderLimitVol.Scroll += new System.EventHandler(this.uxSliderLimitVol_Scroll);
            // 
            // uxButtonLimitCue
            // 
            this.uxButtonLimitCue.Location = new System.Drawing.Point(277, 21);
            this.uxButtonLimitCue.Name = "uxButtonLimitCue";
            this.uxButtonLimitCue.Size = new System.Drawing.Size(92, 23);
            this.uxButtonLimitCue.TabIndex = 13;
            this.uxButtonLimitCue.Text = "Play Sample";
            this.uxButtonLimitCue.UseVisualStyleBackColor = true;
            this.uxButtonLimitCue.Click += new System.EventHandler(this.uxButtonLimitCue_Click);
            // 
            // uxComboLimitCue
            // 
            this.uxComboLimitCue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uxComboLimitCue.FormattingEnabled = true;
            this.uxComboLimitCue.Location = new System.Drawing.Point(66, 23);
            this.uxComboLimitCue.Name = "uxComboLimitCue";
            this.uxComboLimitCue.Size = new System.Drawing.Size(185, 21);
            this.uxComboLimitCue.TabIndex = 12;
            this.uxComboLimitCue.SelectedIndexChanged += new System.EventHandler(this.uxComboLimitCue_SelectedIndexChanged);
            // 
            // uxLabelLimitCue
            // 
            this.uxLabelLimitCue.AutoSize = true;
            this.uxLabelLimitCue.Location = new System.Drawing.Point(4, 26);
            this.uxLabelLimitCue.Name = "uxLabelLimitCue";
            this.uxLabelLimitCue.Size = new System.Drawing.Size(56, 13);
            this.uxLabelLimitCue.TabIndex = 0;
            this.uxLabelLimitCue.Text = "Audio Cue";
            this.uxLabelLimitCue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uxLabelLimitVol
            // 
            this.uxLabelLimitVol.AutoSize = true;
            this.uxLabelLimitVol.Location = new System.Drawing.Point(18, 53);
            this.uxLabelLimitVol.Name = "uxLabelLimitVol";
            this.uxLabelLimitVol.Size = new System.Drawing.Size(42, 13);
            this.uxLabelLimitVol.TabIndex = 0;
            this.uxLabelLimitVol.Text = "Volume";
            this.uxLabelLimitVol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uxLabelSamplePeriod
            // 
            this.uxLabelSamplePeriod.AutoSize = true;
            this.uxLabelSamplePeriod.Location = new System.Drawing.Point(15, 62);
            this.uxLabelSamplePeriod.Name = "uxLabelSamplePeriod";
            this.uxLabelSamplePeriod.Size = new System.Drawing.Size(75, 13);
            this.uxLabelSamplePeriod.TabIndex = 0;
            this.uxLabelSamplePeriod.Text = "Sample Period";
            this.uxLabelSamplePeriod.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uxLabelSamplePeriodVal
            // 
            this.uxLabelSamplePeriodVal.AutoSize = true;
            this.uxLabelSamplePeriodVal.Location = new System.Drawing.Point(342, 62);
            this.uxLabelSamplePeriodVal.Name = "uxLabelSamplePeriodVal";
            this.uxLabelSamplePeriodVal.Size = new System.Drawing.Size(47, 13);
            this.uxLabelSamplePeriodVal.TabIndex = 0;
            this.uxLabelSamplePeriodVal.Text = "0000 ms";
            this.uxLabelSamplePeriodVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uxCheckMonitorDCS
            // 
            this.uxCheckMonitorDCS.AutoSize = true;
            this.uxCheckMonitorDCS.Location = new System.Drawing.Point(96, 39);
            this.uxCheckMonitorDCS.Name = "uxCheckMonitorDCS";
            this.uxCheckMonitorDCS.Size = new System.Drawing.Size(265, 17);
            this.uxCheckMonitorDCS.TabIndex = 2;
            this.uxCheckMonitorDCS.Text = "Device monitoring active only while DCS is running";
            this.uxCheckMonitorDCS.UseVisualStyleBackColor = true;
            this.uxCheckMonitorDCS.CheckedChanged += new System.EventHandler(this.uxCheckMonitorDCS_CheckedChanged);
            // 
            // PrefsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 399);
            this.Controls.Add(this.uxCheckMonitorDCS);
            this.Controls.Add(this.uxLabelSamplePeriodVal);
            this.Controls.Add(this.uxLabelSamplePeriod);
            this.Controls.Add(this.uxGroupLimit);
            this.Controls.Add(this.uxGroupThreshold);
            this.Controls.Add(this.uxSliderSamplePeriod);
            this.Controls.Add(this.uxLabelVersion);
            this.Controls.Add(this.uxLabelStatus);
            this.Controls.Add(this.uxComboDevice);
            this.Controls.Add(this.uxLabelDevice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(418, 438);
            this.MinimumSize = new System.Drawing.Size(418, 438);
            this.Name = "PrefsUI";
            this.ShowInTaskbar = false;
            this.Text = "FSSBHelper Settings";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.PrefsUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uxSliderSamplePeriod)).EndInit();
            this.uxGroupThreshold.ResumeLayout(false);
            this.uxGroupThreshold.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxSliderThresholdVol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxSliderThreshold)).EndInit();
            this.uxGroupLimit.ResumeLayout(false);
            this.uxGroupLimit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxSliderLimitVol)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon uxIconNotify;
        private System.Windows.Forms.Label uxLabelDevice;
        private System.Windows.Forms.ComboBox uxComboDevice;
        private System.Windows.Forms.Label uxLabelStatus;
        private System.Windows.Forms.Label uxLabelVersion;
        private System.Windows.Forms.TrackBar uxSliderSamplePeriod;
        private System.Windows.Forms.CheckBox uxCheckThreshold;
        private System.Windows.Forms.CheckBox uxCheckLimit;
        private System.Windows.Forms.GroupBox uxGroupThreshold;
        private System.Windows.Forms.GroupBox uxGroupLimit;
        private System.Windows.Forms.TrackBar uxSliderThresholdVol;
        private System.Windows.Forms.TrackBar uxSliderThreshold;
        private System.Windows.Forms.Label uxLabelThresholdVol;
        private System.Windows.Forms.Label uxLabelThresholdVal;
        private System.Windows.Forms.Label uxLabelThreshold;
        private System.Windows.Forms.Label uxLabelSamplePeriod;
        private System.Windows.Forms.Label uxLabelSamplePeriodVal;
        private System.Windows.Forms.Button uxButtonThresholdCue;
        private System.Windows.Forms.ComboBox uxComboThresholdCue;
        private System.Windows.Forms.Label uxLabelThresholdCue;
        private System.Windows.Forms.TrackBar uxSliderLimitVol;
        private System.Windows.Forms.Button uxButtonLimitCue;
        private System.Windows.Forms.ComboBox uxComboLimitCue;
        private System.Windows.Forms.Label uxLabelLimitCue;
        private System.Windows.Forms.Label uxLabelLimitVol;
        private System.Windows.Forms.CheckBox uxCheckMonitorDCS;
    }
}

