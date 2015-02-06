using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caros.Core.Extensions;

namespace Caros.Music
{
    public class TrackModel
    {
        public string ID { get; set; }
        public string HashName { get; set; }
        public string OriginalPath { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public long Length { get; set; }
        public int PlayCount { get; set; }
        public DateTime DateAdded { get; set; }

        public Uri GetUri(IContext context)
        {
            return new Uri(context.Storage.MusicInternalCache.Combine(HashName));
        }
    }
}
