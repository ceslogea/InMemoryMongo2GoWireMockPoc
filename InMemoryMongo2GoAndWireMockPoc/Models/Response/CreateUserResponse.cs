using InMemoryMongo2GoAndWireMockPoc.Domain.Entity;
using System.Collections.Generic;

namespace InMemoryMongo2GoAndWireMockPoc.Models.Response
{
    public class CreateUserResponse
    {
        public User User { get; private set; }
        public IEnumerable<string> Errors { get; } = new List<string>();

        public CreateUserResponse(User user)
        {
            User = user;
        }

        public CreateUserResponse(IEnumerable<string> errors = null)
        {
            Errors = errors;
        }
    }
}
