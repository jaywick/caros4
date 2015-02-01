using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Music
{
    class PlayerService
    {
        public void Start(IContext context)
        {
            var collection = context.Database.GetCollection<TrackModel>("tracks");
            var tracks = collection.FindAllAs<TrackModel>();
        }

        public bool IsPlaying { get; set; }
        public TrackModel CurrentTrack { get; set; }
        public TrackModel[] Playlist { get; set; }

        public void Play()
        {
        }

        public void Resume()
        {
        }

        public void Pause()
        {
        }

        public void SkipTrack()
        {
        }

        public void PreviousTrack()
        {
        }
    }
}
