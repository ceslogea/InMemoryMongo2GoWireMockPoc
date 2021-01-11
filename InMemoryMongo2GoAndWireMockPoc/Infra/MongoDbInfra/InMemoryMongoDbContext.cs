using InMemoryMongo2GoAndWireMockPoc.Domain.Entity;
using InMemoryMongo2GoAndWireMockPoc.Infra.Seed;
using Mongo2Go;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace InMemoryMongo2GoAndWireMockPoc.Infra
{
    public class InMemoryMongoDbContext : IMongoContext, IDisposable
    {
        public IMongoClient Client { get; }

        public IMongoDatabase Database { get; }

        private readonly MongoDbRunner _mongoDbRunner;

        public InMemoryMongoDbContext(bool seedDatabase = true)
        {
            _mongoDbRunner = MongoDbRunner.Start();

            Client = new MongoClient(_mongoDbRunner.ConnectionString);
            Database = Client.GetDatabase("poc");
            if(seedDatabase)
                Seed();
        }


        private void Seed()
        {
            Database.GetCollection<User>("User").InsertMany(UsersSeed.UsersSeedData);
        }

        /// <summary>
        /// Método Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Método Virtual Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            _mongoDbRunner.Dispose();
        }
    }
}
