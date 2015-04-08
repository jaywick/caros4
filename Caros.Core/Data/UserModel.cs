using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Data
{
    public class UserModel
    {
        public const string CollectionName = "users";

        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string UserCode { get; set; }
    }
}
