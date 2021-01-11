using InMemoryMongo2GoAndWireMockPoc.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryMongo2GoAndWireMockPoc.Domain.Interface.Query
{
    public interface IUserQueryRepository
    {
        Task<GetUserByDocumentResponse> GetUserByDocumentAsync(string userDocument);
    }
}
