using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caros.Core;
using Caros.Core.Services;

namespace Caros.Music
{
    public class PlayerService : Service
   {
        public bool IsPlaying { get; set; }
        public IMediaPlayer MediaPlayer { get; set; }
        public Playlist<Track> CurrentPlaylist { get; set; }
        public List<Track> TracksCollection { get; set; }
        public History HistoryManager { get; set; }

        public PlayerService(IContext context)
            : base(context)
        {
            CurrentPlaylist = new Playlist<Track>();
            MediaPlayer = new MediaPlayer();
            HistoryManager = new History(context);
        }

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

            HistoryManager.Load();

            CurrentPlaylist = new Playlist<Track>(TracksCollection);
            CurrentPlaylist.Shuffle();
        }

        public void Play(Track track)
        {
            CurrentPlaylist.Current = track;
            MediaPlayer.Open(track.GetUri(Context));
            HistoryManager.Add(track.Model);
            Resume();
        }

        public void Resume()
        {
            IsPlaying = true;
            MediaPlayer.Play();
        }

        public void Pause()
        {
            IsPlaying = false;
            MediaPlayer.Pause();
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
            MediaPlayer.Stop();
            MediaPlayer = null;
        }

        public IEnumerable<Track> GetRecentTracks()
        {
            var playHistory = HistoryManager.Items
                .OrderByDescending(x => x.DatePlayed)
                .Take(50);

            if (!playHistory.Any())
                return Enumerable.Empty<Track>();

            return playHistory
                .Join(TracksCollection,
                      h => h.TrackHashName,
                      t => t.Model.HashName,
                      (a, b) => b)
                .ToList();
        }

        public IEnumerable<Track> GetUnheardTracks()
        {
            var playHistory = HistoryManager.Items;

            if (!playHistory.Any())
                return TracksCollection;

            return TracksCollection
                .Where(t => !playHistory.
                    Any(h => h.TrackHashName == t.Model.HashName))
                .ToList();
        }

        public IEnumerable<Track> GetFavouriteTracks()
        {
            var playHistory = HistoryManager.Items;

            if (!playHistory.Any())
                return Enumerable.Empty<Track>();

            var favouriteTrackHashes = playHistory
                .GroupBy(x => x.TrackHashName)
                .Select(x => new { Track = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count)
                .Select(x => x.Track)
                .Take(50);

            return favouriteTrackHashes
                .Join(TracksCollection,
                      x => x,
                      t => t.Model.HashName,
                      (a, b) => b)
                .ToList();
        }
   }
}
