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

        public JoystickHelper(Settings settings) 
        {
            var device = _DInput.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly)
                .FirstOrDefault(p => string.Equals(p.InstanceName, settings.JoystickName, StringComparison.OrdinalIgnoreCase));

            if (device == null)
                throw new Exception($"Joystick ({settings.JoystickName}) NOT FOUND!!!");

            _Joystick = new Joystick(_DInput, device.InstanceGuid);
            _Joystick.Properties.AxisMode = DeviceAxisMode.Absolute; //align with axis range %
            _Joystick.Acquire();
        }

        public override string ToString() => $"{_Joystick.Information.InstanceName}: {_Joystick.Information.InstanceGuid}";

        public void Dispose()
        {
            if (_Joystick != null)
                _Joystick.Dispose();

            _State = null;
            _DInput.Dispose();
        }
    }
}
