using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caros.Core.Context;
using Caros.Core.Contracts;

namespace Caros.Music.Pages
{
    public class MusicPageViewModel : PageViewModel
    {
        public BindableCollection<TrackModel> AllTracks { get; set; }

        public MusicPageViewModel(IContext context)
            : base(context)
        {
            var importer = new ImporterService(context);
            importer.Start();

            var player = new PlayerService(context);
            player.Start();

            AllTracks = new BindableCollection<TrackModel>(player.AllTracks);
            NotifyOfPropertyChange(() => AllTracks);
        }
    }
}
