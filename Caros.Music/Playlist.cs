using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caros.Core.Extensions;

namespace Caros.Music
{
    public class Playlist<T> where T : class
    {
        private List<T> _items;
        private int _index;

        public Playlist(IEnumerable<T> other)
        {
            _items = new List<T>(other);
            _index = 0;
        }

        public T Current
        {
            get { return _items[_index]; }
            set { _index = _items.IndexOf(value); }
        }

        public T Next()
        {
            _index++;

            if (_index > _items.Count - 1)
                _index = 0;

            return Current;
        }

        public T Previous()
        {
            _index--;

            if (_index < 0)
                _index = _items.Count - 1;

            return Current;
        }

        public void Shuffle()
        {
            var current = Current;
            _items.Shuffle();

            _index = _items.IndexOf(current);
        }

        public List<T> ToList()
        {
            return _items;
        }
    }
}
