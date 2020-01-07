using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Starlight.Input
{
    public class InputSystem : GameComponent
    {
        private readonly GameComponentCollection devices = new GameComponentCollection();

        public IEnumerable<GameComponent> Devices => this.devices.OfType<GameComponent>();
        public IEnumerable<GameComponent> EnabledDevices => this.Devices.Where(item => item.Enabled);

        public InputSystem(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            this.devices.Add(new MouseDevice(this.Game));
            this.devices.Add(new KeyboardDevice(this.Game));
            this.devices.Add(new JoystickDevice(this.Game));
            this.devices.Add(new GamePadDevice(this.Game));
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
    }
}
