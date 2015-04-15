using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caros.Core.Context;
using Caros.Core.Contracts;

namespace Caros.Music
{
    public class LibraryPageViewModel : PageViewModel
    {
        public BindableCollection<Track> NowPlaying { get; set; }

        private PlayerService Player { get; set; }
        private ImporterService Importer { get; set; }

        public LibraryPageViewModel(IContext context)
            : base(context)
        {
            Importer = Context.Services.Utilise<ImporterService>();
            Player = Context.Services.Utilise<PlayerService>();
            Player.Start();

            NowPlaying = new BindableCollection<Track>(Player.CurrentPlaylist.ToList());
            SelectedTrack = NowPlaying.First();

            NotifyOfPropertyChange(() => NowPlaying);
        }

        public Track SelectedTrack
        {
            get
            {
                return Player.CurrentTrack;
            }
            set
            {
                Player.Play(value);
                NotifyOfPropertyChange(() => SelectedTrack);
            }
        }

        public void GoBack()
        {
            Context.Navigator.Return();
        }
    }
}
