using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Caros.Core;
using Caros.Core.Services;

namespace Caros.Music
{
    public class PlayerService : Service
    {
        private MediaPlayer _mediaPlayer = new MediaPlayer();

        public bool IsPlaying { get; set; }
        public TrackModel CurrentTrack { get; set; }
        public Playlist<TrackModel> CurrentPlaylist { get; set; }
        public List<TrackModel> AllTracks { get; set; }

        public PlayerService(IContext context)
            : base(context) { }

        public override void Start()
        {
            ReloadAllSongs();
        }

        private void ReloadAllSongs()
        {
            var collection = Context.Database.GetCollection<TrackModel>(DatabaseReferences.MusicTracks);
            AllTracks = new List<TrackModel>(collection.FindAllAs<TrackModel>());
            CurrentPlaylist = new Playlist<TrackModel>(AllTracks);
        }

        public void Play(TrackModel track)
        {
            _mediaPlayer.Open(track.GetUri(Context));
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
            Play(CurrentPlaylist.Next());
        }

        public void PreviousTrack()
        {
            Play(CurrentPlaylist.Previous());
        }

        public void Dipose()
        {
            _mediaPlayer.Stop();
            _mediaPlayer = null;
        }
    }
}
