using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Music
{
    public class History : IContextComponent
    {
        public IContext Context { get; set; }

        private List<HistoryModel> _internalItems;

        public IEnumerable<HistoryModel> Items
        {
            get { return _internalItems; }
            set { _internalItems = value.ToList(); }
        }

        public History(IContext context)
        {
            Context = context;
            Items = Enumerable.Empty<HistoryModel>();
        }

        public void Load()
        {
            Items = Context.Database
                .GetCollection<HistoryModel>(HistoryModel.CollectionName)
                .FindAllAs<HistoryModel>();
        }

        public void Add(TrackModel track)
        {
            var historyModel = new HistoryModel
            {
                DatePlayed = new DateTime(2000, 1, 1),
                TrackHashName = track.HashName,
            };

            _internalItems.Add(historyModel);
            AddToDatabase(historyModel);
        }

        public virtual void AddToDatabase(HistoryModel historyModel)
        {
            Context.Database
                .GetCollection<HistoryModel>(HistoryModel.CollectionName)
                .Insert(historyModel);
        }
    }
}
