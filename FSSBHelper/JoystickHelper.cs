// ********************************************************************************************
//
// JoystickHelper.cs: FSSBHelper Joystick Functionality
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

using SharpDX.DirectInput;
using System;
using System.Linq;
using System.Windows;

namespace FSSBHelper
{
    public sealed class JoystickHelper : IDisposable
    {
        private DirectInput _DInput = new DirectInput();
        private JoystickState _State = new JoystickState();
        private Vector _Vector = new Vector();
        private Joystick _Joystick;

        public Vector Axis
        {
            get
            {
                _Joystick.GetCurrentState(ref _State);
                _Vector.X = _State.X;
                _Vector.Y = _State.Y;

                return _Vector;
            }
        }

        public JoystickHelper(string deviceName)
        {
            var device = _DInput.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly)
                .FirstOrDefault(p => string.Equals(p.InstanceName, deviceName, StringComparison.OrdinalIgnoreCase));

// TODO: error alert here...
            if (device == null)
                throw new Exception($"Joystick ({deviceName}) NOT FOUND!!!");

            _Joystick = new Joystick(_DInput, device.InstanceGuid);
            _Joystick.Properties.AxisMode = DeviceAxisMode.Absolute; //align with axis range %
            _Joystick.Acquire();
        }

        public override string ToString() => $"{_Joystick.Information.InstanceName}: {_Joystick.Information.InstanceGuid}";

        /// <summary>
        /// Get the currently active Joysticks
        /// </summary>
        /// <returns></returns>
        public static string[] GetActiveJoysticks()
        {
            using (var dinput = new DirectInput())
            {
                var result = dinput
                    .GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly)
                    .Select(p => p.InstanceName)
                    .OrderBy(p => p)
                    .ToArray();

                return result;
            }
        }

        public void Dispose()
        {
            if (_Joystick != null)
                _Joystick.Dispose();

            _State = null;
            _DInput.Dispose();
        }
    }
}
