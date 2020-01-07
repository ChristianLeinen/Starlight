using Microsoft.Xna.Framework;

namespace Starlight.Core
{
    // allows to 'look' at our world, used by Renderer3D
    public interface ICamera
    {
        Matrix ViewMatrix { get; }
        Matrix ProjectionMatrix { get; }
    }

    public abstract class Camera : ICamera
    {
        public float FOV { get; set; }
        public float NearClipPlane { get; set; }
        public float FarClipPlane { get; set; }

        protected Camera() { }
        protected Camera(float fov = MathHelper.PiOver4, float nearClipPlane = 0.1f, float farClipPlane = 1000f)
        {
            this.FOV = fov;
            this.NearClipPlane = nearClipPlane;
            this.FarClipPlane = farClipPlane;
        }

        public abstract Matrix ViewMatrix { get; }
        public virtual Matrix ProjectionMatrix => Matrix.CreatePerspectiveFieldOfView(this.FOV, SessionContext.GraphicsDeviceManager.GraphicsDevice.Adapter.CurrentDisplayMode.AspectRatio, this.NearClipPlane, this.FarClipPlane);
    }
}
