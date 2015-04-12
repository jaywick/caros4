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
    public interface IDatabase : IContextComponent
    {
        MongoDB.Driver.MongoCollection GetCollection(string name);
        MongoDB.Driver.MongoCollection<TEntity> GetCollection<TEntity>(string name);
    }

    public class Database : IDatabase
    {
        private const string ConnectionString = "mongodb://localhost";
        private const string DatabaseName = "master";

        public virtual IContext Context { get; set; }

        public Database(IContext context)
        {
            Context = context;
        }

        public MongoCollection<TEntity> GetCollection<TEntity>(string name)
        {
            var server = new MongoClient(ConnectionString).GetServer();
            var database = server.GetDatabase(DatabaseName);

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
