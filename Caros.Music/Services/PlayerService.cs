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
        public Playlist<Track> CurrentPlaylist { get; set; }
        public List<Track> TracksCollection { get; set; }

        public PlayerService(IContext context)
            : base(context) { }

        public override void Start()
        {
            ReloadAllSongs();
        }

        public Track CurrentTrack
        {
            get { return CurrentPlaylist.Current; }
        }

        private void ReloadAllSongs()
        {
            var collection = Context.Database.GetCollection<TrackModel>(TrackModel.CollectionName);
            TracksCollection = collection.FindAllAs<TrackModel>().Select(x => new Track(x)).ToList();

            CurrentPlaylist = new Playlist<Track>(TracksCollection);
            CurrentPlaylist.Shuffle();
        }

        public void Play(Track track)
        {
            CurrentPlaylist.Current = track;
            _mediaPlayer.Open(track.GetUri(Context));
            Resume();
        }

        public void Resume()
        {
            IsPlaying = true;
            _mediaPlayer.Play();
        }

        public void Pause()
        {
            IsPlaying = false;
            _mediaPlayer.Pause();
        }

        public void TogglePlayback()
        {
            if (IsPlaying)
                Pause();
            else
                Resume();
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
