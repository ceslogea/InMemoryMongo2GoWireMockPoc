using InMemoryMongo2GoAndWireMockPoc.Domain.Entity;
using InMemoryMongo2GoAndWireMockPoc.Domain.Interface.Repository;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace InMemoryMongo2GoAndWireMockPoc.Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoContext mongoContext;
        private IMongoCollection<User> Collection => mongoContext.Database.GetCollection<User>("User");

        public UserRepository(IMongoContext mongoContext)
        {
            this.mongoContext = mongoContext;
        }

        public async Task<User> Create(User user)
        {
            await Collection.InsertOneAsync(user);
            return user;
        }
    }
}
