using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Starlight.Core.Messaging;

namespace Starlight.Input
{
    public class JoystickDevice : InputDevice
    {
        private JoystickCapabilities joystickCapabilities;
        private JoystickState previousJoystickState;

        public JoystickDevice(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            // TODO: support multiple joysticks!
            this.joystickCapabilities = Joystick.GetCapabilities(0);
            if (!(this.joystickCapabilities.IsConnected))
            {
                Trace.TraceWarning($"Joystick 0 not connected!");
                return;
            }

            Trace.TraceInformation($"Joystick 0 connected: {this.joystickCapabilities.ToString()}");
            this.previousJoystickState = Joystick.GetState(0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!(this.joystickCapabilities.IsConnected))
            {
                return;
            }

            var currentJoystickState = Joystick.GetState(0);
            if (this.previousJoystickState == currentJoystickState)
                return;

            void ProcessAspect<TAspect>(TAspect[] previous, TAspect[] current, Action<int, TAspect> dispatchMessage)
            {
                var max = Math.Min(previous.Length, current.Length);
                for (int i = 0; i < max; ++i)
                {
                    var prev = previous[i];
                    var curr = current[i];
                    if (curr.Equals(prev))
                        continue;

                    dispatchMessage(i, curr);
                }
            }

            // process all buttons
            ProcessAspect(this.previousJoystickState.Buttons, currentJoystickState.Buttons, (i, current) => MessageQueue.Dispatch(this, new JoystickButtonMessage(0, i, current)));

            // process all axes
            ProcessAspect(this.previousJoystickState.Axes, currentJoystickState.Axes, (i, current) => MessageQueue.Dispatch(this, new JoystickAxisMessage(0, i, current)));

            // process all hats
            ProcessAspect(this.previousJoystickState.Hats, currentJoystickState.Hats, (i, current) => MessageQueue.Dispatch(this, new JoystickHatMessage(0, i, current)));

            // remember current state
            this.previousJoystickState = currentJoystickState;
        }
    }
}
