using InMemoryMongo2GoAndWireMockPoc.Domain.Entity;
using System.Collections.Generic;

namespace InMemoryMongo2GoAndWireMockPoc.Models.Response
{
    public class GetUserByDocumentResponse
    {
        public string Document { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();

        public GetUserByDocumentResponse(User user)
        {
            Document = user.Document;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }

        public GetUserByDocumentResponse(IEnumerable<string> errors)
        {
            Errors = errors;
        }

    }
}
