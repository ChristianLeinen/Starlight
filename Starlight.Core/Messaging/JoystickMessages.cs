using Microsoft.Xna.Framework.Input;

namespace Starlight.Core.Messaging
{
    public struct JoystickButtonInfo
    {
        public int ButtonIndex;
        public bool IsPressed;

        #region Ctor/dtor
        public JoystickButtonInfo(int buttonIndex, ButtonState buttonState)
        {
            this.ButtonIndex = buttonIndex;
            this.IsPressed = (buttonState == ButtonState.Pressed);
        }
        #endregion

        public override string ToString() => $"Button {this.ButtonIndex}, Pressed: {this.IsPressed}";
    }

    public class JoystickButtonMessage : DeviceEventArgs<JoystickButtonInfo>
    {
        public JoystickButtonMessage(int deviceIndex, JoystickButtonInfo info)
            : base(MessageAction.JoystickButton, deviceIndex, info) { }

        public JoystickButtonMessage(int deviceIndex, int buttonIndex, ButtonState buttonState)
            : this(deviceIndex, new JoystickButtonInfo(buttonIndex, buttonState)) { }
    }

    public struct JoystickAxisInfo
    {
        public int AxisIndex;
        public int Value;

        #region Ctor/dtor
        public JoystickAxisInfo(int axisIndex, int value)
        {
            this.AxisIndex = axisIndex;
            this.Value = value;
        }
        #endregion

        public override string ToString() => $"Axis {this.AxisIndex}, Value: {this.Value}";
    }

    public class JoystickAxisMessage : DeviceEventArgs<JoystickAxisInfo>
    {
        public JoystickAxisMessage(int deviceIndex, JoystickAxisInfo info)
            : base(MessageAction.JoystickAxis, deviceIndex, info) { }

        public JoystickAxisMessage(int deviceIndex, int AxisIndex, int value)
            : this(deviceIndex, new JoystickAxisInfo(AxisIndex, value)) { }
    }

    public struct JoystickHatInfo
    {
        public int HatIndex;
        public bool IsUp;
        public bool IsDown;
        public bool IsLeft;
        public bool IsRight;

        #region Ctor/dtor
        public JoystickHatInfo(int hatIndex, JoystickHat value)
        {
            this.HatIndex = hatIndex;
            this.IsUp = (value.Up == ButtonState.Pressed);
            this.IsDown = (value.Down == ButtonState.Pressed);
            this.IsLeft = (value.Left == ButtonState.Pressed);
            this.IsRight = (value.Right == ButtonState.Pressed);
        }
        #endregion

        public override string ToString() => $"Hat {this.HatIndex}, Up/Down/Left/Right: {this.IsUp}/{this.IsDown}/{this.IsLeft}/{this.IsRight}";
    }

    public class JoystickHatMessage : DeviceEventArgs<JoystickHatInfo>
    {
        public JoystickHatMessage(int deviceIndex, JoystickHatInfo info)
            : base(MessageAction.JoystickHat, deviceIndex, info) { }

        public JoystickHatMessage(int deviceIndex, int hatIndex, JoystickHat value)
            : this(deviceIndex, new JoystickHatInfo(hatIndex, value)) { }
    }
}
