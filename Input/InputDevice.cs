using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Starlight.Core;
using Starlight.Core.Messaging;

namespace Starlight.Input
{
    public abstract class InputDevice : GameComponent
    {
        protected InputMapping inputMapping;
        protected InputLayout activeLayout;

        protected InputDevice(Game game) : base(game)
        {
            MessageQueue.MessageReceived += MessageQueue_MessageReceived;
        }

        public override void Initialize()
        {
            base.Initialize();

            var deviceName = this.GetType().Name;
            try
            {
                var path = $"Config/{deviceName}.json";
                this.inputMapping = Json.ReadFromFile<InputMapping>(path);
                this.activeLayout = this.inputMapping.Layouts.FirstOrDefault();
            }
            catch (IOException ex)
            {
                Trace.TraceError($"Failed to load device configuration for {deviceName}!");
                Trace.TraceError(ex.ToString());
                this.Enabled = false;
            }
        }

        protected virtual void MessageQueue_MessageReceived(object sender, EventArgs e)
        {
            if (e is ActionEventArgs<string> args && args.Action == MessageAction.InputMapping)
            {
                this.activeLayout = this.inputMapping?.Layouts?.FirstOrDefault(layout => layout.Name == args.Info);
                if (this.activeLayout == null)
                {
                    Trace.TraceError($"{this.GetType().Name} failed to activate input layout {args.Info}!");
                }
            }
        }
    }
}
