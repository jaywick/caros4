using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Caros.Core.Integration
{
    public class VersionMeta
    {
        static VersionMeta()
        {
            ReadCurrentVersion();
        }

        private static void ReadCurrentVersion()
        {
            var xdoc = XDocument.Load("meta.xml");
            var releaseNumber = int.Parse(xdoc.Element("Meta").Element("Release").Value);

            CurrentVersion = new ReleaseVersion(releaseNumber);
        }

        public static ReleaseVersion CurrentVersion { get; private set; }

        public class ReleaseVersion
        {
            public string ReleaseName { get; set; }
            public int ReleaseNumber { get; set; }

            public ReleaseVersion(string releaseName)
            {
                ReleaseName = releaseName;
                ReleaseNumber = int.Parse(new Regex(@"r(\d+)").Match(releaseName).Groups[1].Value);
            }

            public ReleaseVersion(int releaseNumber)
            {
                ReleaseNumber = releaseNumber;
                ReleaseName = "r" + ReleaseNumber;
            }
        }

    }
}
