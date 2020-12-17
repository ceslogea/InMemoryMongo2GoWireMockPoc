using InMemoryMongo2GoAndWireMockPoc.Domain.Entity;
using InMemoryMongo2GoAndWireMockPoc.Domain.Interface.Query;
using MongoDB.Driver;

namespace InMemoryMongo2GoAndWireMockPoc.Infra.Query
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly IMongoContext mongoContext;
        private IMongoCollection<User> collection => mongoContext.Database.GetCollection<User>("Contatos");

        public UserQueryRepository(IMongoContext mongoContext)
        {
            this.mongoContext = mongoContext;
        }
    }
}
