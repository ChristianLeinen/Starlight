using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Starlight.Core;
using Starlight.Core.Messaging;

namespace Starlight.Input
{
    public class KeyboardDevice : InputDevice
    {
        #region Fields
        private KeyboardState previousKeyboardState;
        #endregion

        #region Ctor/dtor
        public KeyboardDevice(Game game) : base(game)
        {
        }
        #endregion

        #region Overrides
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var currentKeyboardState = Keyboard.GetState();

            // any changes?
            if (this.previousKeyboardState == currentKeyboardState)
                return;

            // update modifier keys in session context before sending keyup/-down messages
            #region Local helper
            ModifierKeyState GetModifierKeyState(KeyboardState state, Keys left, Keys right) =>
                (state.IsKeyDown(left) ? Core.ModifierKeyState.LeftPressed : Core.ModifierKeyState.None) |
                (state.IsKeyDown(right) ? Core.ModifierKeyState.RightPressed : Core.ModifierKeyState.None);
            #endregion

            SessionContext.ShiftKeyState = GetModifierKeyState(currentKeyboardState, Keys.LeftShift, Keys.RightShift);
            SessionContext.AltKeyState = GetModifierKeyState(currentKeyboardState, Keys.LeftAlt, Keys.RightAlt);
            SessionContext.ControlKeyState = GetModifierKeyState(currentKeyboardState, Keys.LeftControl, Keys.RightControl);

            // process changes
            var prevKeys = this.previousKeyboardState.GetPressedKeys();
            var currKeys = currentKeyboardState.GetPressedKeys();

            // keys that have been released
            var released = from key in prevKeys
                           where !currKeys.Contains(key)
                           select key;
            // keys that have been pressed
            var pressed = from key in currKeys
                          where !prevKeys.Contains(key)
                          select key;

            // all key releases
            foreach (var item in released)
            {
                MessageQueue.Dispatch(this, new KeyboardKeyMessage(item, false));
                MessageQueue.Dispatch(this, new ActionEventArgs<Keys>(MessageAction.KeyUp, item));

                // fire all actions that are not toggle commands
                var trigger = item.ToString();
                var commands = from command in this.activeLayout.Commands
                               where command.Trigger == trigger
                               && !command.IsToggle
                               select command;
                foreach (var cmd in commands)
                {
                    MessageQueue.Dispatch(this, new ActionEventArgs<bool>(cmd.Action, false));
                }
            }

            // all key presses
            foreach (var item in pressed)
            {
                MessageQueue.Dispatch(this, new KeyboardKeyMessage(item, true));
                MessageQueue.Dispatch(this, new ActionEventArgs<Keys>(MessageAction.KeyDown, item));

                // fire all actions
                var trigger = item.ToString();
                var commands = from command in this.activeLayout.Commands
                               where command.Trigger == trigger
                               select command;
                foreach (var cmd in commands)
                {
                    if (cmd.IsToggle)
                    {
                        MessageQueue.Dispatch(this, new ActionEventArgs(cmd.Action));
                    }
                    else
                    {
                        MessageQueue.Dispatch(this, new ActionEventArgs<bool>(cmd.Action, true));
                    }
                }
            }

            // remember current state
            this.previousKeyboardState = currentKeyboardState;
        }
        #endregion
    }
}
