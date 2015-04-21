using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caros.Core.Extensions;

namespace Caros.Music
{
    public class Track
    {
        public TrackModel Model { get; set; }

        public Track(TrackModel model)
        {
            Model = model;
        }

        public virtual Uri GetUri(IContext context)
        {
            return new Uri(context.Storage.MusicInternalCache.Combine(Model.HashName + Model.Extension));
        }

        public string DisplayName
        {
            get { return String.Format("{0} - {1}", Model.Artist, Model.Title); }
        }

        public override bool Equals(object other)
        {
            var otherTrackModel = other as Track;

            if (otherTrackModel == null)
                return false;

            return this.Model.HashName == otherTrackModel.Model.HashName;
        }

        public static bool operator ==(Track a, Track b)
        {
            return a.Model.HashName == b.Model.HashName;
        }

        public static bool operator !=(Track a, Track b)
        {
            return a.Model.HashName != b.Model.HashName;
        }
    }
}
