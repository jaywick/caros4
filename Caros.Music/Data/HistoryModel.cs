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
    public class HistoryModel
    {
        public const string CollectionName = "music.history";

        public ObjectId _id { get; set; }
        public string TrackHashName { get; set; }
        public DateTime DatePlayed { get; set; }
    }
}
