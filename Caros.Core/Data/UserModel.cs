using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core
{
    public class UserModel
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string HashName { get; set; }
    }
}
