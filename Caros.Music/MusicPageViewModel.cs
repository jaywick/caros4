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

        private PlayerService Player { get; set; }
        private ImporterService Importer { get; set; }

        public MusicPageViewModel(IContext context)
            : base(context)
        {
            Importer = Context.Services.Utilise<ImporterService>();
            Player = Context.Services.Utilise<PlayerService>();
            Player.Start();

            AllTracks = new BindableCollection<TrackModel>(Player.AllTracks);
            NotifyOfPropertyChange(() => AllTracks);
        }

        private TrackModel _selectedTrack;
        public TrackModel SelectedTrack
        {
            get
            {
                return _selectedTrack; 
            }
            set
            {
                _selectedTrack = value;
                NotifyOfPropertyChange(() => SelectedTrack);
                Player.Play(SelectedTrack);
            }
        }

        public void PurgeLibrary()
        {
            Importer.PurgeLibrary();
        }
    }
}
