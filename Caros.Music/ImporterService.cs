using System;
using Caros.Core.Context;
using MongoDB.Bson;
using System.IO;
using System.Collections.Generic;
using TagLib;

namespace Caros.Music
{
    class ImporterService
    {
        public void Start(IContext context)
        {
            var importSource = context.Storage.MusicDropFolder;
            var internalCachePath = context.Storage.MusicInternalCache.FullName;
            var completedSinkPath = context.Storage.MusicCompletedImportFolder.FullName;
            var ignoredSinkPath = context.Storage.MusicIgnoredImportFolder.FullName;

            var database = context.Database;

            var tracks = new List<TrackModel>();
            foreach (var file in importSource.EnumerateFiles("*.mp3", System.IO.SearchOption.AllDirectories))
            {
                var hashName = Guid.NewGuid().ToString();
                var track = CreateTrackRecord(file, hashName);

                if (track != null)
                {
                    file.CopyTo(Path.Combine(internalCachePath, hashName));
                    file.MoveTo(Path.Combine(completedSinkPath, file.Name));
                    tracks.Add(track);
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
