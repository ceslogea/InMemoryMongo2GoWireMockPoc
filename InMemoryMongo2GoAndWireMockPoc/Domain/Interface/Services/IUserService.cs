using InMemoryMongo2GoAndWireMockPoc.Domain.Entity;
using InMemoryMongo2GoAndWireMockPoc.Models.Request;
using InMemoryMongo2GoAndWireMockPoc.Models.Response;
using System.Threading.Tasks;

namespace InMemoryMongo2GoAndWireMockPoc.Domain.Interface.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> Create(UserCreateModel user);
    }
}
