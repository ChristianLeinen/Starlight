using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Starlight.Core.Messaging
{
    public struct GamePadButtonInfo
    {
        public string ButtonName;
        public bool IsPressed;

        #region Ctor/dtor
        public GamePadButtonInfo(string buttonName, ButtonState buttonState)
        {
            this.ButtonName = buttonName;
            this.IsPressed = (buttonState == ButtonState.Pressed);
        }
        #endregion

        public override string ToString() => $"Button {this.ButtonName}, Pressed: {this.IsPressed}";
    }

    public class GamePadButtonMessage : DeviceEventArgs<GamePadButtonInfo>
    {
        public GamePadButtonMessage(int deviceIndex, GamePadButtonInfo info)
            : base(MessageAction.GamePadButton, deviceIndex, info) { }

        public GamePadButtonMessage(int deviceIndex, string buttonName, ButtonState buttonState)
            : this(deviceIndex, new GamePadButtonInfo(buttonName, buttonState)) { }
    }

    public struct GamePadDPadInfo
    {
        public bool IsUp;
        public bool IsDown;
        public bool IsLeft;
        public bool IsRight;

        #region Ctor/dtor
        public GamePadDPadInfo(GamePadDPad value)
        {
            this.IsUp = (value.Up == ButtonState.Pressed);
            this.IsDown = (value.Down == ButtonState.Pressed);
            this.IsLeft = (value.Left == ButtonState.Pressed);
            this.IsRight = (value.Right == ButtonState.Pressed);
        }
        #endregion

        public override string ToString() => $"Up/Down/Left/Right: {this.IsUp}/{this.IsDown}/{this.IsLeft}/{this.IsRight}";
    }

    public class GamePadDPadMessage : DeviceEventArgs<GamePadDPadInfo>
    {
        public GamePadDPadMessage(int deviceIndex, GamePadDPadInfo info)
            : base(MessageAction.GamePadDPad, deviceIndex, info) { }

        public GamePadDPadMessage(int deviceIndex, GamePadDPad value)
            : this(deviceIndex, new GamePadDPadInfo(value)) { }
    }

    public class GamePadTriggerMessage : DeviceEventArgs<float>
    {
        public GamePadTriggerMessage(string action, int deviceIndex, float info)
            : base(action, deviceIndex, info) { }
    }

    public class GamePadThumbStickMessage : DeviceEventArgs<Vector2>
    {
        public GamePadThumbStickMessage(string action, int deviceIndex, Vector2 info)
            : base(action, deviceIndex, info) { }
    }
}
