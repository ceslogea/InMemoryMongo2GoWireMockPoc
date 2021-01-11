using InMemoryMongo2GoAndWireMockPoc.Models.ExternalResources.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryMongo2GoAndWireMockPoc.Domain.Interface.ExternalResourcesRepository
{
    public interface ICepApiService
    {
        [Get("/ws/{cep}/json")]
        Task<PostalCodeResponse> GetAddressAsync(string cep);
    }
}
