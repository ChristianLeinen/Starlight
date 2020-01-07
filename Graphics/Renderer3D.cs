using Microsoft.Xna.Framework;

namespace Starlight.Graphics
{
    //public class Floor
    //{
    //    int floorWidth, floorHeight;
    //    VertexBuffer floorBuffer;
    //    GraphicsDevice device;
    //    Color[] floorColors = new Color[2] { Color.White, Color.Black };

    //    public Floor(GraphicsDevice device, int width, int height)
    //    {
    //        this.device = device;
    //        this.floorWidth = width;
    //        this.floorHeight = height;
    //        this.BuildFloorBuffer();
    //    }

    //    // build vertexbuffer
    //    private void BuildFloorBuffer()
    //    {
    //        var vertexList = new List<VertexPositionColor>();
    //        var counter = 0;
    //        for (int x = 0; x < floorWidth; ++x)
    //        {
    //            ++counter;
    //            for (int z = 0; z < floorHeight; ++z)
    //            {
    //                ++counter;

    //                // loop through and add vertices
    //                foreach (var vertex in this.FloorTile(x, z, floorColors[counter % 2]))
    //                {
    //                    vertexList.Add(vertex);
    //                }
    //            }
    //        }

    //        // create buffer
    //        this.floorBuffer = new VertexBuffer(this.device, VertexPositionColor.VertexDeclaration, vertexList.Count, BufferUsage.WriteOnly);
    //        this.floorBuffer.SetData(vertexList.ToArray());
    //    }

    //    // define a single tile in our floor
    //    private IList<VertexPositionColor> FloorTile(int xOffset, int zOffset, Color tileColor)
    //    {
    //        var list = new List<VertexPositionColor>
    //        {
    //            new VertexPositionColor(new Vector3(0 + xOffset, 0, 0 + zOffset), tileColor),
    //            new VertexPositionColor(new Vector3(1 + xOffset, 0, 0 + zOffset), tileColor),
    //            new VertexPositionColor(new Vector3(0 + xOffset, 0, 1 + zOffset), tileColor),

    //            new VertexPositionColor(new Vector3(1 + xOffset, 0, 0 + zOffset), tileColor),
    //            new VertexPositionColor(new Vector3(1 + xOffset, 0, 1 + zOffset), tileColor),
    //            new VertexPositionColor(new Vector3(0 + xOffset, 0, 1 + zOffset), tileColor)
    //        };

    //        return list;
    //    }

    //    public void Draw(Camera camera, BasicEffect effect)
    //    {
    //        effect.VertexColorEnabled = true;
    //        effect.View = camera.ViewMatrix;
    //        effect.Projection = camera.ProjectionMatrix;
    //        effect.World = Matrix.Identity;
    //        this.device.RasterizerState = RasterizerState.CullNone;
    //        // loop through and draw each tile
    //        foreach (var pass in effect.CurrentTechnique.Passes)
    //        {
    //            pass.Apply();
    //            device.SetVertexBuffer(floorBuffer);
    //            device.DrawPrimitives(PrimitiveType.TriangleList, 0, floorBuffer.VertexCount / 3);
    //        }
    //    }
    //}

    // render game world through camera
    // should be able to render specific parts of the world through different cameras:
    // - draw a mirror
    // - draw a mini map (top down)
    public class Renderer3D : DrawableGameComponent
    {
        public Renderer3D(Game game) : base(game)
        {
        }

        //Terrain terrain;
        //FreeCamera cam;
        //Floor floor;
        //BasicEffect effect;

        public override void Initialize()
        {
            base.Initialize();

            // camera stuff
            //cam = new FreeCamera(this.GraphicsDevice);
            //cam.Position = new Vector3(10f, 1f, 5f);
            //floor = new Floor(this.GraphicsDevice, 20, 20);
            //effect = new BasicEffect(this.GraphicsDevice);
        }

        // NOTE: this gets called before the Games' LoadContent()!
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        // NOTE: this never gets called by the Game!
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //cam.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //floor.Draw(this.cam, this.effect);

            base.Draw(gameTime);
        }
    }
}
