using System;
using Caros.Core.Context;
using MongoDB.Bson;
using System.IO;
using System.Collections.Generic;
using TagLib;
using Caros.Core;
using System.Linq;
using Caros.Core.Services;
using System.Threading.Tasks;

namespace Caros.Music
{
    public class ImporterService : SystemService
    {
        public Action ImportCompleted;

        public ImporterService(IContext context)
            : base(context)
        {
        }

        public async override void Start()
        {
            await Task.Run((Action)StartImport);
        }

        private void StartImport()
        {
            var importSource = Context.Storage.MusicDropFolder;
            var internalCachePath = Context.Storage.MusicInternalCache.FullName;
            var completedSinkPath = Context.Storage.MusicCompletedImportFolder.FullName;
            var ignoredSinkPath = Context.Storage.MusicIgnoredImportFolder.FullName;

            var collection = Context.Database.Load<TrackModel>();

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

                    Context.Database.Insert(track);
                }
                else
                {
                    file.CopyTo(Path.Combine(ignoredSinkPath, file.Name), true);
                    file.Delete();
                }
            }

            if (ImportCompleted != null)
                ImportCompleted.Invoke();
        }

        public void PurgeLibrary()
        {
            Context.Services.Utilise<PlayerService>().Dipose();
            Context.Storage.MusicInternalCache.EnumerateFiles().ToList().ForEach(x => x.Delete());
            Context.Database.Clear<TrackModel>();
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
