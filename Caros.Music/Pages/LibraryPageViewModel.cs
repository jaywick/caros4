using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caros.Core.Context;
using Caros.Core.UI;
using Caros.Music.Pages;

namespace Caros.Music
{
    public class LibraryPageViewModel : PageViewModel
    {
        public BindableCollection<LibraryFolder> Folders { get; set; }

        private PlayerService Player { get; set; }
        private ImporterService Importer { get; set; }
        private LibraryFolder _searchFolder;

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

            _searchFolder = new LibraryFolder("Search", null, isEnabled: false);
            Folders.Add(_searchFolder);

            Folders.Refresh();
        }

        private LibraryFolder _selectedFolder;
        public LibraryFolder SelectedFolder
        {
            get
            {
                return _selectedFolder;
            }
            set
            {
                _selectedFolder = value;
                UpdateFolderChange();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                NotifyOfPropertyChange(() => SearchText);
                UpdateSearch(value);
            }
        }

        public override void OnSearch()
        {
            SelectedFolder = _searchFolder;
        }

        public bool IsSearching
        {
            get { return SelectedFolder == _searchFolder; }
        }

        private void UpdateFolderChange()
        {
            NotifyOfPropertyChange(() => Folders);
            NotifyOfPropertyChange(() => IsSearching);
            NotifyOfPropertyChange(() => SelectedFolder);
        }

        private void UpdateSearch(string searchText)
        {
            _searchFolder.Tracks.Clear();
            _searchFolder.Tracks.AddRange(Player.SearchTracks(searchText));
        }
    }
}
