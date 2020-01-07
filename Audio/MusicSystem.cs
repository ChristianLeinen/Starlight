using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Starlight.Core.Messaging;

namespace Starlight.Audio
{
    public class MusicSystem : GameComponent
    {
        public MusicSystem(Game game) : base(game)
        {
            MessageQueue.MessageReceived += MessageQueue_MessageReceived;
        }

        public override void Initialize()
        {
            base.Initialize();
            MediaPlayer.ActiveSongChanged += MediaPlayer_ActiveSongChanged;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            //MediaLibrary lib = new MediaLibrary();
            //lib.Load();
            //if (lib.Songs != null)
            //{
            //    MediaPlayer.Play(lib.Songs);
            //}

            // set overall music volume [0f..1f]
            //MediaPlayer.Volume;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        private void PlaySong(string name)
        {
            var song = this.Game.Content.Load<Song>(name);
            MediaPlayer.Play(song);
        }

        private void MessageQueue_MessageReceived(object sender, EventArgs e)
        {
            if (e is ActionEventArgs message)
            {
                if (message.Action == MessageAction.PlayMusic && message is ActionEventArgs<string> msg)
                {
                    // loading a song is quite quick, but starting the playback may take a few seconds
                    Task.Run(() => this.PlaySong(msg.Info));
                }
                else if (message.Action == MessageAction.StopMusic)
                {
                    MediaPlayer.Stop();
                }
                else if (message.Action == MessageAction.PauseMusic)
                {
                    MediaPlayer.Pause();
                }
                else if (message.Action == MessageAction.ResumeMusic)
                {
                    MediaPlayer.Resume();
                }
            }
        }

        private void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            MessageQueue.Post(this, new ActionEventArgs<string>(MessageAction.MediaStateChanged, MediaPlayer.State.ToString()));
        }

        private void MediaPlayer_ActiveSongChanged(object sender, System.EventArgs e)
        {
            MessageQueue.Post(this, new ActionEventArgs<string>(MessageAction.ActiveSongChanged, MediaPlayer.Queue.ActiveSong.Name));
        }
    }
}
