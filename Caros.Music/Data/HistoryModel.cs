using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caros.Core.Extensions;
using MongoDB.Bson;
using Caros.Core;

namespace Caros.Music
{
    [Collection("music.history")]
    public class HistoryModel
    {
        public ObjectId _id { get; set; }
        public string TrackHashName { get; set; }
        public DateTime DatePlayed { get; set; }
    }
}
