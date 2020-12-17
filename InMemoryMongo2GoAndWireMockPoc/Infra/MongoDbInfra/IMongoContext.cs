using MongoDB.Driver;

namespace InMemoryMongo2GoAndWireMockPoc.Infra
{
    public interface IMongoContext
    {
        IMongoClient Client { get; }
        IMongoDatabase Database { get; }
    }
}
