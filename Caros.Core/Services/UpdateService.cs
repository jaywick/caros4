using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Services
{
    class UpdateService : Service
    {
        private const string FtpHost = "103.9.171.165";
        private const string FtpPath = @"/caros4/packages";
        private const string FtpUserName = "caros@jay-wick.com";
        private const string FtpPassword = "_=nMqH!m@naV";

        public UpdateService (IContext context)
            : base(context)
	    {
	    }

        public override void Start()
        {
        }
    }
}
