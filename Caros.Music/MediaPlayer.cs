using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Caros.Music
{
    public interface IMediaPlayer
    {
        void Open(Uri source);
        void Play();
        void Pause();
        void Stop();
    }

    public class MediaPlayer : IMediaPlayer
    {
        private System.Windows.Media.MediaPlayer _internalPlayer = new System.Windows.Media.MediaPlayer();

        public void Open(Uri source)
        {
            _internalPlayer.Open(source);
        }

        public void Play()
        {
            _internalPlayer.Play();
        }

        public void Pause()
        {
            _internalPlayer.Pause();
        }

        public void Stop()
        {
            _internalPlayer.Stop();
        }
    }
}
