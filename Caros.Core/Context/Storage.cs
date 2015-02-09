using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Context
{
    public class Storage : ContextComponent
    {
        public Storage(IContext context)
            : base(context)
        {
        }

        private const string RootDirectory = "/caros/";
        private const string SystemDirectory = "/caros/system/";
        private const string DropDirectory = "/caros/drop/";

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
            get { return GetFolder(RootDirectory, "data"); }
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
