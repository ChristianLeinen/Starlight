using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Starlight.Graphics
{
    public class GraphicSystem : DrawableGameComponent
    {
        private readonly GameComponentCollection devices = new GameComponentCollection();

        public IEnumerable<DrawableGameComponent> Devices => this.devices.OfType<DrawableGameComponent>();
        public IEnumerable<DrawableGameComponent> EnabledDevices => this.Devices.Where(item => item.Enabled);

        public GraphicSystem(Game game) : base(game)
        {
            //MessageQueue.MessageReceived += MessageQueue_MessageReceived;
        }

        public override void Initialize()
        {
            base.Initialize();

            this.devices.Add(new Renderer3D(this.Game));
            this.devices.Add(new Renderer2D(this.Game));
            foreach (var item in this.Devices)
            {
                item.Initialize();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var item in this.EnabledDevices)
            {
                item.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            //this.GraphicsDevice.Clear(Microsoft.Xna.Framework.Graphics.ClearOptions.Target | Microsoft.Xna.Framework.Graphics.ClearOptions.DepthBuffer | Microsoft.Xna.Framework.Graphics.ClearOptions.Stencil, Color.White, 1.0f, 0);
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);

            foreach (var item in this.EnabledDevices)
            {
                item.Draw(gameTime);
            }
        }

        //private void MessageQueue_MessageReceived(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
