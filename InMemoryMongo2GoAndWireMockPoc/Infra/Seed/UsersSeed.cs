using InMemoryMongo2GoAndWireMockPoc.Domain.Entity;
using System.Collections.Generic;

namespace InMemoryMongo2GoAndWireMockPoc.Infra.Seed
{
    public static class UsersSeed
    {
        public static User[] UsersSeedData => new List<User>()
            {
                new User()
                {
                    Document = "32893956858",
                    FirstName =  "Celso",
                    LastName = "Gea"
                }
            }.ToArray();

    }
}
