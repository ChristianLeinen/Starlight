using Microsoft.Xna.Framework;

namespace Starlight.Audio
{
    public class SoundSystem : GameComponent
    {
        public SoundSystem(Game game) : base(game)
        {
            //MessageQueue.MessageReceived += MessageQueue_MessageReceived;
        }

        //SoundEffectInstance instance;
        //AudioListener listener = new AudioListener();
        //AudioEmitter emitter = new AudioEmitter();
        public override void Initialize()
        {
            base.Initialize();

            //var sound = this.Game.Content.Load<SoundEffect>("sound_click_tick");
            //sound.Play();

            //this.instance = sound.CreateInstance();
            //this.listener = new AudioListener();
            //this.emitter = new AudioEmitter();

            //this.listener.Position = Vector3.Zero;
            //this.listener.Forward = Vector3.Forward;
            //this.listener.Up = Vector3.Up;

            //this.emitter.Position = new Vector3(0f, 0f, -2f);
            //this.emitter.Forward = -Vector3.Forward;
            //this.emitter.Up = Vector3.Up;

            //this.instance.Apply3D(listener, emitter);
            //this.instance.IsLooped = true;
            //this.instance.Play();

            // set overall sound volume [0f..1f]
            //SoundEffect.MasterVolume;
        }

        //float diff = -0.1f;
        public override void Update(GameTime gameTime)
        {
            //this.emitter.Position = new Vector3(this.emitter.Position.X + diff, 0f, -2f);
            //if (this.emitter.Position.X < -10f || this.emitter.Position.X > 10f)
            //    diff = -diff;

            //this.instance.Apply3D(listener, emitter);

            base.Update(gameTime);
        }

        //private void MessageQueue_MessageReceived(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
