using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Caros.Music
{
    public class TagReader
    {
        private FileInfo _file;
        private TagLib.File _tagFile;

        public TagReader(string path)
        {
            _file = new FileInfo(path);
            _tagFile = TagLib.File.Create(new LocalFileAbstraction(path, false));
        }

        public string GetTitle()
        {
            return _tagFile.Tag.Title;
        }

        public string GetArtist()
        {
            return _tagFile.Tag.FirstPerformer;
        }

        public string GetAlbum()
        {
            return _tagFile.Tag.Album;
        }

        public long GetLength()
        {
            return _tagFile.Length;
        }

        private class LocalFileAbstraction : TagLib.File.IFileAbstraction
        {
            public LocalFileAbstraction(string path, bool openForWrite = false)
            {
                Name = Path.GetFileName(path);
                var fileStream = openForWrite ? System.IO.File.Open(path, FileMode.Open, FileAccess.ReadWrite) : System.IO.File.OpenRead(path);
                ReadStream = WriteStream = fileStream;
            }

            public string Name { get; private set; }

            public Stream ReadStream { get; private set; }

            public Stream WriteStream { get; private set; }

            public void CloseStream(Stream stream)
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }
    }
}
