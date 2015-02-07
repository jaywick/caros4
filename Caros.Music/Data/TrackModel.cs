using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caros.Core.Extensions;
using MongoDB.Bson;

namespace Caros.Music
{
    public class TrackModel
    {
        public ObjectId _id { get; set; }
        public string HashName { get; set; }
        public string OriginalPath { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public TimeSpan Length { get; set; }
        public int Bitrate { get; set; }
        public int PlayCount { get; set; }
        public DateTime DateAdded { get; set; }
        public string Extension { get; set; }

        public Uri GetUri(IContext context)
        {
            return new Uri(context.Storage.MusicInternalCache.Combine(HashName + Extension));
        }
    }
}
