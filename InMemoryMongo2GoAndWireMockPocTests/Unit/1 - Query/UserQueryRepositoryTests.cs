using InMemoryMongo2GoAndWireMockPoc.Domain.Entity;
using InMemoryMongo2GoAndWireMockPoc.Domain.Interface.Query;
using InMemoryMongo2GoAndWireMockPoc.Infra;
using InMemoryMongo2GoAndWireMockPoc.Infra.Query;
using InMemoryMongo2GoAndWireMockPoc.Models.Response;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Unit.Query
{

    /// <summary>
    /// - Nomeando seus testes. O nome do seu teste deve ser composto por três partes:
    /// * O nome do método que está sendo testado.
    /// * O cenário em que ele está sendo testado.
    /// * O comportamento esperado quando o cenário é invocado.
    /// </summary>
    public class UserQueryRepositoryTests
    {
        IUserQueryRepository usuarioQueryService;
        private readonly Mock<ILogger<UserQueryRepository>> logger;

        public UserQueryRepositoryTests()
        {
            logger = new Mock<ILogger<UserQueryRepository>>();
        }

        [Fact(DisplayName = "Deve obter o usuário Com documento válido Deve retornar o usuário")]
        public async Task Should_get_user_With_valid_document_Must_return_user()
        {
            // Arrange
            const string USERDOCUMENT = "32893956858";
            usuarioQueryService = new UserQueryRepository(new InMemoryMongoDbContext(true), logger.Object);

            // Act
            GetUserByDocumentResponse user = await usuarioQueryService.GetUserByDocumentAsync(USERDOCUMENT);

            // Assert
            Assert.Equal(USERDOCUMENT, user.Document);
        }

        [Fact(DisplayName = "Não deve obter usuário Com documento válido Usuário não encontrado no banco de dados")]
        public async Task Should_not_get_user_With_valid_document_User_not_found_in_database()
        {
            // Arrange
            const string USERDOCUMENT = "45166568095";
            usuarioQueryService = new UserQueryRepository(new InMemoryMongoDbContext(true), logger.Object);

            // Act
            GetUserByDocumentResponse user = await usuarioQueryService.GetUserByDocumentAsync(USERDOCUMENT);

            // Assert
            Assert.Null(user);
        }

        [Fact(DisplayName = "Não deve obter usuário Erro no repositório Deve retornar mensagem de erro de repositório")]
        public async Task Should_not_get_user_Repository_error_Should_return_repository_error_message()
        {
            logger.Setup(x => x.Log(
                                It.IsAny<LogLevel>(),
                                It.IsAny<EventId>(),
                                It.IsAny<It.IsAnyType>(),
                                It.IsAny<Exception>(),
                                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>())).Throws(new System.Exception(""));

            // Arrange
            const string USERDOCUMENT = "32893956858";
            usuarioQueryService = new UserQueryRepository(new InMemoryMongoDbContext(true), logger.Object);

            // Act
            GetUserByDocumentResponse user = await usuarioQueryService.GetUserByDocumentAsync(USERDOCUMENT);

            // Assert
            logger.Verify(x => x.Log(
                                It.IsAny<LogLevel>(),
                                It.IsAny<EventId>(),
                                It.IsAny<It.IsAnyType>(),
                                It.IsAny<Exception>(),
                                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));

            Assert.NotEmpty(user.Errors);
        }

    }
}
