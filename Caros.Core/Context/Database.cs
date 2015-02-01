using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Caros.Core.Context
{
    public class Database
    {
        public string ConnectionString
        {
            get { return "mongodb://localhost"; }
        }

        public string DatabaseName
        {
            get { return "master"; }
        }

        public MongoCollection<TEntity> GetCollection<TEntity>(string collectionName)
        {
            var database = new MongoClient(ConnectionString).GetServer().GetDatabase(DatabaseName);
            return database.GetCollection<TEntity>(collectionName);
        }
    }
}
