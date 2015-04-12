using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Context
{
    interface IStorage
    {
        System.IO.DirectoryInfo DataFolder { get; }
        System.IO.DirectoryInfo GetFolder(params string[] paths);
        System.IO.DirectoryInfo MusicCompletedImportFolder { get; }
        System.IO.DirectoryInfo MusicDropFolder { get; }
        System.IO.DirectoryInfo MusicIgnoredImportFolder { get; }
        System.IO.DirectoryInfo MusicInternalCache { get; }
        System.IO.DirectoryInfo UserProfile { get; }
    }

    public class Storage : ContextComponent, IStorage
    {
        public Storage(IContext context)
            : base(context)
        {
        }

        public readonly static string DataDirectory = "/caros/data";
        public readonly static string BinariesDirectory = "/caros/system/binaries/";

        public readonly static string RootDirectory = "/caros/";
        public readonly static string SystemDirectory = "/caros/system/";
        public readonly static string DropDirectory = "/caros/drop/";
        public readonly static string UsersDirectory = "/caros/users/";

        public DirectoryInfo UserProfile
        {
            get { return GetFolder(UsersDirectory, Context.Profiles.CurrentUser.UserCode); }
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
