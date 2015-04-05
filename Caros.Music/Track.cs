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

        public Uri GetUri(IContext context)
        {
            return new Uri(context.Storage.MusicInternalCache.Combine(Model.HashName + Model.Extension));
        }

        public string DisplayName
        {
            get { return String.Format("{0} - {1}", Model.Artist, Model.Title); }
        }
    }
}
