using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Context
{
    public interface IStorage : IContextComponent
    {
        DirectoryInfo DataFolder { get; }
        DirectoryInfo GetFolder(params string[] paths);
        DirectoryInfo MusicCompletedImportFolder { get; }
        DirectoryInfo MusicDropFolder { get; }
        DirectoryInfo MusicIgnoredImportFolder { get; }
        DirectoryInfo MusicInternalCache { get; }
        DirectoryInfo UserProfile { get; }
    }

    public class Storage : IStorage
    {
        public virtual IContext Context { get; set; }

        static Storage()
        {
            CreateDirIfMissing(DataDirectory);
            CreateDirIfMissing(LogsDirectory);
            CreateDirIfMissing(LogDumpDirectory);
            CreateDirIfMissing(BinariesDirectory);
            CreateDirIfMissing(RootDirectory);
            CreateDirIfMissing(SystemDirectory);
            CreateDirIfMissing(DropDirectory);
            CreateDirIfMissing(UsersDirectory);
        }

        private static void CreateDirIfMissing(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        public Storage(IContext context)
        {
            Context = context;
        }

        public readonly static string DataDirectory = "/caros/data/";
        public readonly static string LogsDirectory = "/caros/system/logs/";
        public readonly static string LogDumpDirectory = "/caros/system/logs/dumps/";
        public readonly static string BinariesDirectory = "/caros/system/binaries/";

        public readonly static string RootDirectory = "/caros/";
        public readonly static string SystemDirectory = "/caros/system/";
        public readonly static string DropDirectory = "/caros/drop/";
        public readonly static string UsersDirectory = "/caros/users/";

        public DirectoryInfo UserProfile
        {
            get { return GetFolder(UsersDirectory, Context.Profiles.CurrentUser.Name); }
        }

        public DirectoryInfo MusicInternalCache
        {
            get { return GetFolder(SystemDirectory, "music/library"); }
        }

        public DirectoryInfo MusicDropFolder
        {
            get { return GetFolder(DropDirectory, "music"); }
        }

        public DirectoryInfo MusicCompletedImportFolder
        {
            get { return GetFolder(DropDirectory, "music/completed"); }
        }

        public DirectoryInfo MusicIgnoredImportFolder
        {
            get { return GetFolder(DropDirectory, "music/ignored"); }
        }

        public DirectoryInfo DataFolder
        {
            get { return GetFolder(DataDirectory); }
        }

        public DirectoryInfo GetFolder(params string[] paths)
        {
            var directory = new DirectoryInfo(Path.Combine(paths));

            if (!directory.Exists)
                directory.Create();

            return directory;
        }
    }
}
