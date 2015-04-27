using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Music.Pages
{
    public class LibraryFolder
    {
        public string Name { get; set; }

        public BindableCollection<Track> Tracks { get; set; }

        public bool IsEnabled { get; set; }

        public LibraryFolder(string name, IEnumerable<Track> tracks, bool isEnabled = true)
        {
            Name = name;

            Tracks = new BindableCollection<Track>();
            Tracks.AddRange(tracks ?? Enumerable.Empty<Track>());

            IsEnabled = isEnabled;
        }
    }
}
