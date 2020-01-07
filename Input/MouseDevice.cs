using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Starlight.Core;
using Starlight.Core.Messaging;

namespace Starlight.Input
{
    public class MouseDevice : InputDevice
    {
        #region Constants
        private const string XAxis = "X";
        private const string YAxis = "Y";
        private const string Scroll = "Scroll";
        private const string HorzScroll = "HorzScroll";
        #endregion

        #region Fields
        private MouseState previousMouseState;
        #endregion

        #region Ctor/dtor
        public MouseDevice(Game game) : base(game)
        {
        }
        #endregion

        private void ReCenterMouse()
        {
            var pos = this.Game.Window.ClientBounds.Center;
            Mouse.SetPosition(pos.X >> 1, pos.Y >> 1);
            this.previousMouseState = Mouse.GetState();
            SessionContext.CursorPosition = this.previousMouseState.Position;
        }

        #region Overrides
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var currentMouseState = Mouse.GetState();

            // any changes?
            if (this.previousMouseState == currentMouseState)
                return;

            // process all buttons of mousestate
            var properties = typeof(MouseState).GetProperties();
            foreach (var prop in properties.Where(prop => prop.PropertyType == typeof(ButtonState)))
            {
                // 1. see if value has changed
                var previous = (ButtonState)prop.GetValue(this.previousMouseState);
                var current = (ButtonState)prop.GetValue(currentMouseState);
                if (current == previous)
                    continue;

                var trigger = $"Mouse.{prop.Name}";
                // 2. dispatch it
                MessageQueue.Dispatch(this, new ActionEventArgs<bool>(trigger, current == ButtonState.Pressed));

                // 3. check for mapping
                var commands = from command in this.activeLayout.Commands
                               where command.Trigger == prop.Name
                               select command;
                foreach (var cmd in commands)
                {
                    if (!cmd.IsToggle)
                    {
                        // TODO: always map to float?
                        MessageQueue.Dispatch(this, new ActionEventArgs<bool>(cmd.Action, current == ButtonState.Pressed));
                    }
                    else if (current == ButtonState.Pressed)
                    {
                        MessageQueue.Dispatch(this, new ActionEventArgs(cmd.Action));
                    }
                }
            }

            #region Local helper
            void TranslateAction<T>(string trigger, T value)
            {
                var commands = from command in this.activeLayout.Commands
                               where command.Trigger == trigger
                               select command;
                foreach (var cmd in commands)
                {
                    MessageQueue.Dispatch(this, new ActionEventArgs<T>(cmd.Action, value));
                }
            }
            #endregion

            // process X/Y (always report relative)
            if (currentMouseState.X != this.previousMouseState.X)
            {
                var value = currentMouseState.X - this.previousMouseState.X;
                MessageQueue.Dispatch(this, new ActionEventArgs<int>(MessageAction.MouseX, value));
                TranslateAction(XAxis, value);
            }
            if (currentMouseState.Y != this.previousMouseState.Y)
            {
                var value = currentMouseState.Y - this.previousMouseState.Y;
                MessageQueue.Dispatch(this, new ActionEventArgs<int>(MessageAction.MouseX, value));
                TranslateAction(YAxis, value);
            }

            // process scroll wheel (send absolute and relative)
            if (currentMouseState.ScrollWheelValue != this.previousMouseState.ScrollWheelValue)
            {
                var value = (currentMouseState.ScrollWheelValue - this.previousMouseState.ScrollWheelValue) / 120;
                MessageQueue.Dispatch(this, new ActionEventArgs<int>(MessageAction.MouseScrollWheelValue, currentMouseState.ScrollWheelValue));
                MessageQueue.Dispatch(this, new ActionEventArgs<int>(MessageAction.MouseScrollWheel, value));
                TranslateAction(Scroll, value);
            }
            if (currentMouseState.HorizontalScrollWheelValue != this.previousMouseState.HorizontalScrollWheelValue)
            {
                var value = (currentMouseState.HorizontalScrollWheelValue - this.previousMouseState.HorizontalScrollWheelValue) / 120;
                MessageQueue.Dispatch(this, new ActionEventArgs<int>(MessageAction.MouseHorizontalScrollWheelValue, currentMouseState.HorizontalScrollWheelValue));
                MessageQueue.Dispatch(this, new ActionEventArgs<int>(MessageAction.MouseHorizontalScrollWheel, value));
                TranslateAction(HorzScroll, value);
            }

            if (SessionContext.AutoRecenter)
            {
                // this will also store the current MouseState and position
                this.ReCenterMouse();
                return;
            }

            // finally the position (always report absolute), only if not auto-recenter
            var previousPos = this.previousMouseState.Position;
            var currentPos = currentMouseState.Position;
            if (currentPos != previousPos)
            {
                // position in session context
                SessionContext.CursorPosition = currentPos;

                MessageQueue.Dispatch(this, new ActionEventArgs<Point>(MessageAction.MousePosition, currentPos));

                // NOTE: the position is not processed as trigger for action mapping!
            }

            // remember current state
            this.previousMouseState = currentMouseState;
        }

        protected override void MessageQueue_MessageReceived(object sender, EventArgs e)
        {
            base.MessageQueue_MessageReceived(sender, e);

            if (e is ActionEventArgs<bool> msg && msg.Action == MessageAction.MouseAutoRecenter)
            {
                SessionContext.AutoRecenter = msg.Info;
                if (msg.Info)
                {
                    this.ReCenterMouse();
                }
            }
        }
        #endregion
    }
}
