# FSSBHelper
RealSimulator FSSB R3 Axis Alerter.

## Introduction

This is a small helper program for the RealSimulator FSSB base that outputs the axis
alerts (i.e., stick at max deflection) through the Windows audio subsystem. This makes
it easier to hear in VR or other environments where the audio from the FSSB base may
not be loud enough to be heard.

The alerts can be programmed through the UI to occur above a threshold of the stick
throw.

## Installation

There are two packages available on the GitHub release page.

- `exe` includes the executable and all support files.
- `1clk` includes a Windows OneClick installer.

Both packages contain the program, `FSSBHelper.exe`.

### Executable Installation

The `exe` package includes a folder with the `FSSBHelper.exe` executable along with
support files. You can copy the folder wherever you want and launch by double clicking
on the executable. You can uninstall by dragging the folder to the Recycle Bin.

### OneClick Installation

The `1clk` package includes a OneClick installer, `setup.exe` that installs the
`FSSBHelper.application` application and registers it with Windows. You can uninstall
through the Add/Remove Programs Windows control panel.

## Using FSSBHelper

The UI is minimized to the system tray on launch and the application does not show up
in the Windows taskbar. To access the UI, double-click on the FSSBHelper icon in the
system tray. Minimizing the UI window will return it to the tray.

FSSBHelper can provide audio feedback in the form of a beep for two events independently,

- Stick is above a programmed threshold of its full throw.
- Stick is at limit.

To control this feedback, the FSSBHelper window provides three groups of controls.

- The first group at the top of the window sets the FSSB device to monitor and controls
  the behavior of the monitoring.
- The second group provides control over the feedback when above threshold.
- The third group provides control over the feedback when at limit.

Following sections will discuss each of these areas in more detail.

### Selecting and Setting Up Monitoring

There are four controls in this part of the user interface.

- *FSSB Device:* drop-down menu listing the gaming devices attached to your system.
  Select the FSSB device you want to monitor from this list.
- *Device monitoring...:* This checkbox selects the monitoring mode. When selected,
  FSSBHelper will only monitor when DCS is running. When not selected, FSSBHelper
  monitors at all times.
- *Sample Period:* This sets the interval at which FSSBHelper samples the joystick.
- *Decouple FSSB...:* This checkbox selects the coupling mode. When selected, the
  joystick sampling and audio feedback are decoupled and the audio feedback from
  the application is independent of the sample rate. When not selected, these are
  coupled and the audio feedback rate is set by the sample rate.

### Setting Up Threshold Feedsback

There are five controls in this part of the user interface. These controls are only
enabled if the "Enable audio feedback above threshold" checkbox is selected.

- *Threshold* defines the threshold above which feedback is generated. The threshold
  is specified as a percentage between 0% and 100%.
- *Audio Cue* provides a drop-down menu from which you can select the cue to be played
  when the joystick is beyond the defined threshold.
- *Play Sample* allows you to play a sample of the cue.
- *Volume* allows you to set the volume of the cue.

### Setting Up Limit Feedback

There are four controls in this part of the user interface. These controls are only
enabled if the "Enable audio feedback at limit" checkbox is selected. The controls
are identical to the threshold controls but do not provide a way to specify the
threshold (since it is, by definition, at joystick limits).