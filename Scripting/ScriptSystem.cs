using System;
using Microsoft.Xna.Framework;
using Starlight.Core;
using Starlight.Core.Messaging;

namespace Starlight.Scripting
{
    public class ScriptSystem : GameComponent
    {
        public ScriptSystem(Game game) : base(game)
        {
            MessageQueue.MessageReceived += MessageQueue_MessageReceived;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        private void MessageQueue_MessageReceived(object sender, EventArgs e)
        {
            if (e is ActionEventArgs message)
            {
                if (message.Action == MessageAction.Quit)
                {
                    this.Game.Exit();
                }
                else if (message.Action == MessageAction.ToggleMenu)
                {
                    var menuVisible = !SessionContext.Get<bool>("IsMenuVisible");
                    SessionContext.Properties["IsMenuVisible"] = menuVisible;

                    this.Game.IsMouseVisible = menuVisible;
                    MessageQueue.Dispatch(this, new ActionEventArgs<string>(MessageAction.InputMapping, menuVisible ? "Menu" : "Game"));
                    MessageQueue.Dispatch(this, new ActionEventArgs<bool>(MessageAction.MouseAutoRecenter, !menuVisible));
                }
                // TODO: to show info while key is pressed:
                //else if (message.Action == "ToggleDebugInfo" && message is Message<bool> msgDbg /*&& msgDbg.Info*/)
                //{
                //    SessionContext.Instance["DebugInfo"] = msgDbg.Info;
                //}
                // or make it a toggle, like so:
                else if (message.Action == "ToggleDebugInfo")
                {
                    var debugInfo = !SessionContext.Get<bool>("DebugInfo");
                    SessionContext.Properties["DebugInfo"] = debugInfo;
                }

                //
                // TODO: add support for conditions in input configuration, like so?
                //
                //{
                //  "Trigger": "F1",
                //  "Condition": "KEYDOWN",
                //  "Action": "DebugInfoOn"
                //},
                //{
                //  "Trigger": "F1",
                //  "Condition": "KEYUP",
                //  "Action": "DebugInfoOff"
                //}
                //
                // and then process'em like so:
                //
                //else if (message.Action == "DebugInfoOn")
                //{
                //    SessionContext.Instance["DebugInfo"] = true;
                //}
                //else if (message.Action == "DebugInfoOff")
                //{
                //    SessionContext.Instance["DebugInfo"] = false;
                //}
            }
        }
    }
}
