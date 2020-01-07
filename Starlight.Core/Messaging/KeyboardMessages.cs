using Microsoft.Xna.Framework.Input;

namespace Starlight.Core.Messaging
{
    #region Keyboard stuff
    public struct KeyboardKeyInfo
    {
        public Keys Key;
        public bool IsPressed;

        #region Ctor/dtor
        public KeyboardKeyInfo(Keys key, bool isPressed)
        {
            this.Key = key;
            this.IsPressed = isPressed;
        }
        #endregion

        public override string ToString() => $"Key {this.Key}, Pressed: {this.IsPressed}";
    }

    public class KeyboardKeyMessage : ActionEventArgs<KeyboardKeyInfo>
    {
        public KeyboardKeyMessage(KeyboardKeyInfo info)
            : base(MessageAction.KeyboardKey, info) { }

        public KeyboardKeyMessage(Keys key, bool isPressed)
            : this(new KeyboardKeyInfo(key, isPressed)) { }
    }
    #endregion
}
