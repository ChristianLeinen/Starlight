using System.Diagnostics;
using Microsoft.Xna.Framework;
using Starlight.AI;
using Starlight.Audio;
using Starlight.Core;
using Starlight.Graphics;
using Starlight.Input;
using Starlight.Network;
using Starlight.Physics;
using Starlight.Scripting;

namespace Starlight
{
    public class StarlightGame : Game
    {
        public StarlightGame()
        {
            SessionContext.GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.Components.Add(new InputSystem(this));
            this.Components.Add(new ScriptSystem(this));
            this.Components.Add(new NetworkSystem(this));
            this.Components.Add(new PhysicSystem(this));
            this.Components.Add(new AISystem(this));
            this.Components.Add(new MusicSystem(this));
            this.Components.Add(new SoundSystem(this));
            this.Components.Add(new GraphicSystem(this));

            // base will call Initialize() on all components
            base.Initialize();

            SessionContext.Properties["IsMenuVisible"] = true;
            //this.Dispatch(this, new Message<bool>(MessageAction.MouseAutoRecenter, true));
        }

        readonly Stopwatch stopwatchUpdate = Stopwatch.StartNew();
        int framesUpdated = 0;
        protected override void Update(GameTime gameTime)
        {
            //SessionContext.Instance.FpsUpdate = (int)(1 / gameTime.ElapsedGameTime.TotalSeconds);
            if (stopwatchUpdate.ElapsedMilliseconds > 999)
            {
                SessionContext.FpsUpdate = framesUpdated;
                framesUpdated = 0;
                stopwatchUpdate.Restart();
            }
            ++framesUpdated;

            base.Update(gameTime);
        }

        readonly Stopwatch stopwatchDraw = Stopwatch.StartNew();
        int framesDrawn = 0;
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.White);

            //SessionContext.Instance.FpsDraw = (int)(1 / gameTime.ElapsedGameTime.TotalSeconds);
            if (stopwatchDraw.ElapsedMilliseconds > 999)
            {
                SessionContext.FpsDraw = framesDrawn;
                framesDrawn = 0;
                stopwatchDraw.Restart();
            }
            ++framesDrawn;
            base.Draw(gameTime);
        }
    }
}
