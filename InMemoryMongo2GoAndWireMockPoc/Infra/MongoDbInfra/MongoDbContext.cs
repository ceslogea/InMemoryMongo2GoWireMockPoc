using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryMongo2GoAndWireMockPoc.Infra
{
    public class MongoDbContext : IMongoContext
    {
        public IMongoClient Client { get; }

        public IMongoDatabase Database { get; }

        public MongoDbContext(IConfiguration configuration)
        {
            Client = new MongoClient(configuration.GetValue<string>("MongoConfig:ConnectionString"));
            Database = Client.GetDatabase(configuration.GetValue<string>("MongoConfig:Database"));
        }
    }
}
