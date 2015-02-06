using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Caros.Music
{
    class PlayerService
    {
        private MediaPlayer _mediaPlayer = new MediaPlayer();
        private IContext _context;

        public bool IsPlaying { get; set; }
        public TrackModel CurrentTrack { get; set; }
        public Playlist<TrackModel> Playlist { get; set; }
        
        public void Start(IContext context)
        {
            var collection = context.Database.GetCollection<TrackModel>("tracks");
            var tracks = collection.FindAllAs<TrackModel>();
        }

        public void Load(IEnumerable<TrackModel> tracks)
        {
            Playlist = new Playlist<TrackModel>(tracks);
        }

        public void Play(TrackModel track)
        {
            _mediaPlayer.Open(track.GetUri(_context));
            _mediaPlayer.Play();
        }

        public void Resume()
        {
            _mediaPlayer.Play();
        }

        public void Pause()
        {
            _mediaPlayer.Pause();
        }

        public void SkipTrack()
        {
            Play(Playlist.Next());
        }

        public void PreviousTrack()
        {
            Play(Playlist.Previous());
        }
    }
}
