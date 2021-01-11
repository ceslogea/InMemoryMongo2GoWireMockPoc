using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMemoryMongo2GoAndWireMockPoc.Domain.Entity;
using InMemoryMongo2GoAndWireMockPoc.Domain.Interface.Query;
using InMemoryMongo2GoAndWireMockPoc.Domain.Interface.Services;
using InMemoryMongo2GoAndWireMockPoc.Models.Request;
using InMemoryMongo2GoAndWireMockPoc.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InMemoryMongo2GoAndWireMockPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IUserQueryRepository userQueryRepository;

        public UserController(IUserQueryRepository userQueryRepository, IUserService userService)
        {
            this.userQueryRepository = userQueryRepository;
            this.userService = userService;
        }

        [HttpGet]
        [Route("{document}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(GetUserByDocumentResponse), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        public async Task<IActionResult> Get(string document)
        {
            var userResponse = await userQueryRepository.GetUserByDocumentAsync(document);

            if(userResponse == null)
                return NoContent();

            if (userResponse.Errors.Any())
                return NoContent();

            return Ok(userResponse);
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), 201)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        public async Task<IActionResult> Post(UserCreateModel userModel)
        {
            var userCreatedResponse = await userService.Create(userModel);

            if (userCreatedResponse.Errors.Any())
                return BadRequest(userCreatedResponse.Errors);
             
            return Created($"api/User/{userCreatedResponse.User.Id}", userCreatedResponse.User);
        }
    }
}
