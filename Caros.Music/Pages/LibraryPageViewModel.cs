using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caros.Core.Context;
using Caros.Core.Contracts;
using Caros.Music.Pages;

namespace Caros.Music
{
    public class LibraryPageViewModel : PageViewModel
    {
        public BindableCollection<LibraryFolder> Folders { get; set; }

        private PlayerService Player { get; set; }
        private ImporterService Importer { get; set; }

        public LibraryPageViewModel(IContext context)
            : base(context)
        {
            Importer = Context.Services.Utilise<ImporterService>();
            Player = Context.Services.Utilise<PlayerService>();

            Folders = new BindableCollection<LibraryFolder>();
            Folders.Add(new LibraryFolder("Now Playing", Player.CurrentPlaylist.ToList()));
            Folders.Add(new LibraryFolder("All Tracks", Player.TracksCollection));
            Folders.Add(new LibraryFolder("Recent", Player.GetRecentTracks()));
            Folders.Add(new LibraryFolder("Unheard", Player.GetUnheardTracks()));
            Folders.Add(new LibraryFolder("Favourites", Player.GetFavouriteTracks()));
            //Folders.Add(new LibraryFolder("Artists", null));
            //Folders.Add(new LibraryFolder("Albums", null));

            Folders.Refresh();
        }

        private Track _selectedFolder;
        public Track SelectedFolder
        {
            get
            {
                return _selectedFolder;
            }
            set
            {
                _selectedFolder = value;
                NotifyOfPropertyChange(() => SelectedFolder);
            }
        }

        public void GoBack()
        {
            Context.Navigator.Visit<MusicPageViewModel>();
        }
    }
}
