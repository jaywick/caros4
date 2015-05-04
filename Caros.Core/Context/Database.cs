using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using Caros.Core.Extensions;

namespace Caros.Core.Context
{
    public interface IDatabase : IContextComponent
    {
        IEnumerable<TEntity> Load<TEntity>();
        void Insert<TEntity>(TEntity entity);
        void Clear<TEntity>();
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

        private MongoCollection<TEntity> GetCollection<TEntity>()
        {
            var server = new MongoClient(ConnectionString).GetServer();
            var database = server.GetDatabase(DatabaseName);

            var name = typeof(TEntity).GetAttribute<CollectionAttribute>().Name;

            if (!database.CollectionExists(name))
                database.CreateCollection(name);

            return database.GetCollection<TEntity>(name);
        }

        public IEnumerable<TEntity> Load<TEntity>()
        {
            return GetCollection<TEntity>().FindAllAs<TEntity>();
        }

        public void Insert<TEntity>(TEntity entity)
        {
            GetCollection<TEntity>().Insert(entity);
        }

        public void Clear<TEntity>()
        {
            GetCollection<TEntity>().RemoveAll();
        }
    }
}
