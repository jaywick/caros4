using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Data
{
    [Collection("users")]
    public class UserModel : DataEntity
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string UserCode { get; set; }
        public DateTime Added { get; set; }
    }
}
