using System;
using Caros.Core.Context;
using MongoDB.Bson;
using System.IO;
using System.Collections.Generic;
using TagLib;
using Caros.Core;
using System.Linq;
using Caros.Core.Services;

namespace Caros.Music
{
    [AutoStart]
    public class ImporterService : Service
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

            return;
            var collection = Context.Database.GetCollection<TrackModel>(DatabaseReferences.MusicTracks);
            
            foreach (var file in importSource.EnumerateFiles("*.mp3", System.IO.SearchOption.AllDirectories))
            {
                if (file.Directory.FullName == completedSinkPath || file.Directory.FullName == ignoredSinkPath)
                    continue;

                var hashName = Crypto.GenerateGuid();
                var track = CreateTrackRecord(file, hashName, Context.Profiles.CurrentUser.UserCode);

                if (track != null)
                {
                    file.CopyTo(Path.Combine(internalCachePath, hashName + file.Extension), true);
                    file.CopyTo(Path.Combine(completedSinkPath, file.Name), true);
                    file.Delete();

                    collection.Insert(track);
                }
                else
                {
                    file.CopyTo(Path.Combine(ignoredSinkPath, file.Name), true);
                    file.Delete();
                }
            }
        }

        public void PurgeLibrary()
        {
            Context.Services.Utilise<PlayerService>().Dipose();
            Context.Storage.MusicInternalCache.EnumerateFiles().ToList().ForEach(x => x.Delete());
            Context.Database.GetCollection(DatabaseReferences.MusicTracks).RemoveAll();
        }

        private TrackModel CreateTrackRecord(FileInfo originalFile, string hashName, string userCode)
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
                Bitrate = tagReader.GetBitrate(),
                PlayCount = 0,
                DateAdded = DateTime.Now,
                Extension = originalFile.Extension,
                UserCode = userCode,
            };
        }
    }
}
