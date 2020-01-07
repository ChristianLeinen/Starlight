using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Starlight.Core.Messaging;

namespace Starlight.Input
{
    public class GamePadDevice : InputDevice
    {
        private GamePadCapabilities gamePadCapabilities;
        private GamePadState previousGamePadState;

        public GamePadDevice(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            // TODO: support multiple gamepads!
            this.gamePadCapabilities = GamePad.GetCapabilities(0);
            if (!(this.gamePadCapabilities.IsConnected))
            {
                Trace.TraceWarning($"GamePad 0 not connected!");
                return;
            }

            Trace.TraceInformation($"GamePad 0 connected: {this.gamePadCapabilities.ToString()}");
            this.previousGamePadState = GamePad.GetState(0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!(this.gamePadCapabilities.IsConnected))
            {
                return;
            }

            var currentGamePadState = GamePad.GetState(0);
            if (this.previousGamePadState == currentGamePadState)
                return;

            // process all buttons
            //public ButtonState RightShoulder { get; }
            //public ButtonState LeftStick { get; }
            //public ButtonState LeftShoulder { get; }
            //public ButtonState Start { get; }
            //public ButtonState Y { get; }
            //public ButtonState X { get; }
            //public ButtonState RightStick { get; }
            //public ButtonState Back { get; }
            //public ButtonState A { get; }
            //public ButtonState B { get; }
            //public ButtonState BigButton { get; }
            var properties = typeof(GamePadButtons).GetProperties();
            foreach (var prop in properties)
            {
                // 1. see if value has changed
                var previous = (ButtonState)prop.GetValue(this.previousGamePadState);
                var current = (ButtonState)prop.GetValue(currentGamePadState);
                if (current == previous)
                    continue;

                // 3. dispatch it
                MessageQueue.Dispatch(this, new GamePadButtonMessage(0, prop.Name, current));
            }

            // dpad
            if (this.previousGamePadState.DPad != currentGamePadState.DPad)
            {
                MessageQueue.Dispatch(this, new GamePadDPadMessage(0, currentGamePadState.DPad));
            }

            // triggers
            if (this.previousGamePadState.Triggers.Left != currentGamePadState.Triggers.Left)
            {
                MessageQueue.Dispatch(this, new GamePadTriggerMessage(MessageAction.GamePadTriggerL, 0, currentGamePadState.Triggers.Left));
            }
            if (this.previousGamePadState.Triggers.Right != currentGamePadState.Triggers.Right)
            {
                MessageQueue.Dispatch(this, new GamePadTriggerMessage(MessageAction.GamePadTriggerR, 0, currentGamePadState.Triggers.Right));
            }

            // thumbsticks
            if (this.previousGamePadState.ThumbSticks.Left != currentGamePadState.ThumbSticks.Left)
            {
                MessageQueue.Dispatch(this, new GamePadThumbStickMessage(MessageAction.GamePadThumbStickL, 0, currentGamePadState.ThumbSticks.Left));
            }
            if (this.previousGamePadState.ThumbSticks.Right != currentGamePadState.ThumbSticks.Right)
            {
                MessageQueue.Dispatch(this, new GamePadThumbStickMessage(MessageAction.GamePadThumbStickR, 0, currentGamePadState.ThumbSticks.Right));
            }
        }
    }
}
