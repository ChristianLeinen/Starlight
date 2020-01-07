using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Starlight.Core;

namespace Starlight.Graphics
{
    // things we can render:
    // - HUD
    // - Menu
    public class Renderer2D : DrawableGameComponent
    {
        public Renderer2D(Game game) : base(game)
        {
            //MessageQueue.MessageReceived += MessageQueue_MessageReceived;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        SpriteBatch spriteBatch;
        SpriteFont font;
        protected override void LoadContent()
        {
            base.LoadContent();

            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = this.Game.Content.Load<SpriteFont>("Arial");
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (SessionContext.Get<bool>("DebugInfo"))
            {
                //var blendState = this.GraphicsDevice.BlendState;
                //var depthStencilState = this.GraphicsDevice.DepthStencilState;
                //var rasterizerState = this.GraphicsDevice.RasterizerState;

                spriteBatch.Begin();
                var pos = SessionContext.CursorPosition;
                var info = $"ClientBounds: {this.Game.Window.ClientBounds}{Environment.NewLine}CursorPos: {pos}{Environment.NewLine}FPS (Update/Draw): {SessionContext.FpsUpdate}/{SessionContext.FpsDraw}";
                spriteBatch.DrawString(font, info, Vector2.One, Color.Black);
                info = $"Adapter: {this.Game.GraphicsDevice.Adapter.Description}{Environment.NewLine}DisplayMode: {this.Game.GraphicsDevice.Adapter.CurrentDisplayMode.ToString()}{Environment.NewLine}Viewport: {this.Game.GraphicsDevice.Viewport.ToString()}";
                var size = font.MeasureString(info);
                spriteBatch.DrawString(font, info, new Vector2(1, this.Game.Window.ClientBounds.Height - size.Y - 1), Color.Black);
                if (gameTime.IsRunningSlowly)
                {
                    info = "RUNNING SLOWLY!";
                    size = font.MeasureString(info);
                    spriteBatch.DrawString(font, info, new Vector2(this.Game.Window.ClientBounds.Width - size.X - 1, 1), Color.OrangeRed);
                }
                spriteBatch.End();

                //this.GraphicsDevice.BlendState = blendState;
                //this.GraphicsDevice.DepthStencilState = depthStencilState;
                //this.GraphicsDevice.RasterizerState = rasterizerState;
            }

            base.Draw(gameTime);
        }

        //private void MessageQueue_MessageReceived(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
