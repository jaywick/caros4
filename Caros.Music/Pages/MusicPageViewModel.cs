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
    public class MusicPageViewModel : PageViewModel
    {
        public BindableCollection<Track> NowPlaying { get; set; }
        public bool IsEmptyLibrary { get; set; }

        private PlayerService Player { get; set; }
        private ImporterService Importer { get; set; }

        public MusicPageViewModel(IContext context)
            : base(context)
        {
            Importer = Context.Services.Utilise<ImporterService>();
            Player = Context.Services.Utilise<PlayerService>();
            Player.Start();
        }

        public override void OnVisit(bool isFirst)
        {
            if (!isFirst)
                return;

            if (!Player.TracksCollection.Any())
            {
                IsEmptyLibrary = true;
                return;
            }

            NowPlaying = new BindableCollection<Track>(Player.CurrentPlaylist.ToList());
            Player.Play(NowPlaying.First());

            NotifyOfPropertyChange(() => NowPlaying);
        }

        public Track CurrentTrack
        {
            get { return Player.CurrentTrack; }
        }

        public string TogglePlayButtonText
        {
            get { return Player.IsPlaying ? "Pause" : "Play"; }
        }

        public void PreviousTrack()
        {
            Player.PreviousTrack();
            UpdateDisplay();
        }

        public void TogglePlayback()
        {
            Player.TogglePlayback();
            UpdateDisplay();
        }

        public void SkipTrack()
        {
            Player.SkipTrack();
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            NotifyOfPropertyChange(() => CurrentTrack);
            NotifyOfPropertyChange(() => TogglePlayButtonText);
        }

        public void OpenLibrary()
        {
            Context.Navigator.Visit<LibraryPageViewModel>();
        }
    }
}
