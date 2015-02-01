using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Context
{
    public class Storage
    {
        private const string SYSTEM_DIRECTORY = @"c:\caros\system\";
        private const string DROP_DIRECTORY = @"c:\caros\drop\";

        public DirectoryInfo MusicInternalCache
        {
            get { return GetFolder(SYSTEM_DIRECTORY, @"music\library"); }
        }

        public DirectoryInfo MusicDropFolder
        {
            get { return GetFolder(DROP_DIRECTORY, "music"); }
        }

        public DirectoryInfo MusicCompletedImportFolder
        {
            get { return GetFolder(DROP_DIRECTORY, @"music\completed"); }
        }

        public DirectoryInfo MusicIgnoredImportFolder
        {
            get { return GetFolder(DROP_DIRECTORY, @"music\ignored"); }
        }

        public DirectoryInfo GetFolder(params string[] pathComponents)
        {
            var directory = new DirectoryInfo(Path.Combine(pathComponents));

            if (!directory.Exists)
                directory.Create();

            return directory;
        }
    }
}
