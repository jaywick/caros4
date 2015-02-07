using System;
using Caros.Core.Context;
using MongoDB.Bson;
using System.IO;
using System.Collections.Generic;
using TagLib;
using Caros.Core;

namespace Caros.Music
{
    public class ImporterService : SystemService
    {
        public ImporterService(IContext context)
            : base(context)
        {
        }

        public override void Start()
        {
            var importSource = Context.Storage.MusicDropFolder;
            var internalCachePath = Context.Storage.MusicInternalCache.FullName;
            var completedSinkPath = Context.Storage.MusicCompletedImportFolder.FullName;
            var ignoredSinkPath = Context.Storage.MusicIgnoredImportFolder.FullName;

            var collection = Context.Database.GetCollection<TrackModel>(DatabaseReferences.MusicTracks);
            collection.RemoveAll();

            foreach (var file in importSource.EnumerateFiles("*.mp3", System.IO.SearchOption.AllDirectories))
            {
                if (file.Directory.FullName == completedSinkPath || file.Directory.FullName == ignoredSinkPath)
                    continue;

                var hashName = Guid.NewGuid().ToString();
                var track = CreateTrackRecord(file, hashName);

                if (track != null)
                {
                    file.CopyTo(Path.Combine(internalCachePath, hashName));
                    file.MoveTo(Path.Combine(completedSinkPath, file.Name));
                    collection.Insert(track);
                }
                else
                {
                    file.MoveTo(Path.Combine(ignoredSinkPath, file.Name));
                }
            }
        }

        private TrackModel CreateTrackRecord(FileInfo originalFile, string hashName)
        {
            TagReader tagReader = null;

            try
            {
                tagReader = new TagReader(originalFile.FullName);
            }
            catch
            {
                return null;
            }

            return new TrackModel
            {
                HashName = hashName,
                OriginalPath = originalFile.FullName,
                Title = tagReader.GetTitle(),
                Artist = tagReader.GetArtist(),
                Album = tagReader.GetAlbum(),
                Length = tagReader.GetLength(),
                PlayCount = 0,
                DateAdded = DateTime.Now,
            };
        }
    }
}
