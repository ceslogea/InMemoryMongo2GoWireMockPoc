using InMemoryMongo2GoAndWireMockPoc.Domain.Entity;
using InMemoryMongo2GoAndWireMockPoc.Domain.Interface.Query;
using InMemoryMongo2GoAndWireMockPoc.Models.Response;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryMongo2GoAndWireMockPoc.Infra.Query
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly IMongoContext mongoContext;
        private readonly ILogger logger;

        private IMongoCollection<User> Collection => mongoContext.Database.GetCollection<User>("User");

        public UserQueryRepository(IMongoContext mongoContext, ILogger<UserQueryRepository> logger)
        {
            this.mongoContext = mongoContext;
            this.logger = logger;
        }

        public async Task<GetUserByDocumentResponse> GetUserByDocumentAsync(string userDocument)
        {
            try
            {
                logger.LogInformation($"Get user by document: {userDocument}");
                if (string.IsNullOrEmpty(userDocument) || userDocument.All(char.IsWhiteSpace))
                    return default;

                var results = await Collection.Find(x => x.Document == userDocument).Limit(1).ToListAsync();
                
                if (results.Any())
                    return new GetUserByDocumentResponse(results.FirstOrDefault());

                return default;
            }
            catch (System.Exception e)
            {
                return new GetUserByDocumentResponse(new List<string>() { e.Message });
            }
        }
    }
}
