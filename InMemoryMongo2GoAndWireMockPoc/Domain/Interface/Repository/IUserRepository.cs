using InMemoryMongo2GoAndWireMockPoc.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryMongo2GoAndWireMockPoc.Domain.Interface.Repository
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
    }
}
