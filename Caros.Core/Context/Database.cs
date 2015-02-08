using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;

namespace Caros.Core.Context
{
    public class Database : ContextComponent
    {
        private const string ConnectionString = "mongodb://localhost";
        private const string DatabaseName = "master";
        
        public Database(IContext context)
            : base(context)
        {
        }

        public MongoCollection<TEntity> GetCollection<TEntity>(string name)
        {
            var database = new MongoClient(ConnectionString).GetServer().GetDatabase(DatabaseName);

            if (!database.CollectionExists(name))
                database.CreateCollection(name);

            return database.GetCollection<TEntity>(name);
        }

        public MongoCollection GetCollection(string name)
        {
            return GetCollection<object>(name);
        }
    }
}
