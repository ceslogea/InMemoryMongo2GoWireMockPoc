using InMemoryMongo2GoAndWireMockPoc.Domain.Entity;
using InMemoryMongo2GoAndWireMockPoc.Domain.Interface.ExternalResourcesRepository;
using InMemoryMongo2GoAndWireMockPoc.Domain.Interface.Query;
using InMemoryMongo2GoAndWireMockPoc.Domain.Interface.Repository;
using InMemoryMongo2GoAndWireMockPoc.Domain.Interface.Services;
using InMemoryMongo2GoAndWireMockPoc.Models.Request;
using InMemoryMongo2GoAndWireMockPoc.Models.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryMongo2GoAndWireMockPoc.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserQueryRepository userQueryRepository;
        private readonly ICepApiService cepApiService;

        public UserService(IUserRepository userRepository, IUserQueryRepository userQueryRepository, ICepApiService cepApiService)
        {
            this.userRepository = userRepository;
            this.userQueryRepository = userQueryRepository;
            this.cepApiService = cepApiService;
        }

        public async Task<CreateUserResponse> Create(UserCreateModel user)
        {
            try
            {
                var newUser = User.CreateNewUserFromModel(user);
                var userByDocumentResponse = await userQueryRepository.GetUserByDocumentAsync(user.Document);
                var userAlreadyCreated = userByDocumentResponse != null && userByDocumentResponse.Document.Equals(user.Document);
                var userIsInvalid = !newUser.IsValidToCreate();

                if (userAlreadyCreated)
                    return new CreateUserResponse(newUser.UserAlreadyCreatedMessage());

                if (userIsInvalid)
                    return new CreateUserResponse(newUser.GetErrosToCreate());

                newUser.Address = await cepApiService.GetAddressAsync(user.PostalCode);

                var newUserCreated = await userRepository.Create(newUser);

                return new CreateUserResponse(newUserCreated);
            }
            catch (Exception e)
            {
                return new CreateUserResponse(new List<string>() { e.Message });
            }
        }
    }
}
