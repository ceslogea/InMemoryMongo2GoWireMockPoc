using InMemoryMongo2GoAndWireMockPoc.Models.Request;
using InMemoryMongo2GoAndWireMockPoc.Models.Response;
using Integration.WebAppFactory;
using System.Net;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace Integration
{
    /// <summary>
    /// - Nomeando seus testes. O nome do seu teste deve ser composto por três partes:
    /// * O nome do método que está sendo testado.
    /// * O cenário em que ele está sendo testado.
    /// * O comportamento esperado quando o cenário é invocado.
    /// </summary>
    public class UserControllerTest : BaseTestController
    {
        public UserControllerTest(WebApplicationFactory factory) : base(factory) { }

        /// <summary>
        /// Mock Mongo2go
        /// </summary>
        [Fact(DisplayName = "Get deve retornar usuário Com um cpf valido Deve retornar sucesso Async")]
        public async Task Get_should_return_user_With_valid_document_Must_return_success_Async()
        {
            var request = await GetAsync("api/User/32893956858");
            request.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, request.StatusCode);
        }

        /// <summary>
        /// Mock Mongo2go
        /// </summary>
        [Fact(DisplayName = "Get não deve retornar o usuário Com um cpf valido que não esta no bando de dados Deve retornar no content Async")]
        public async Task Get_should_not_return_user_With_valid_document_not_in_database_Must_return_no_content_Async()
        {
            var request = await GetAsync("api/User/45166568095");
            Assert.Equal(HttpStatusCode.NoContent, request.StatusCode);
        }

        /// <summary>
        /// Mock Mongo2go, WireMock
        /// </summary>
        [Fact(DisplayName = "Post deve Criar usuario Com com dados válidos Deve retornar usuario criado Async")]
        public async Task Post_should_create_user_With_valid_data_and_postal_code_Must_return_user_created_Async()
        {
            var user = new UserCreateModel()
            {
                Document = "45166568095",
                FirstName = "FirstName",
                LastName = "LastName",
                PostalCode = "14804412"
            };

            httpMockServer.Reset();
            httpMockServer.Given(Request.Create().WithPath($"/ws/{user.PostalCode}/json").UsingGet())
                           .RespondWith(Response.Create().WithStatusCode(200).WithBody(@"{
                                                                                          ""cep"": ""14804 - 412"",
                                                                                          ""logradouro"": ""Rua Joaquim de Arruda Campos"",
                                                                                          ""complemento"": ""wire mock data"",
                                                                                          ""bairro"": ""Parque Igaçaba"",
                                                                                          ""localidade"": ""Araraquara"",
                                                                                          ""uf"": ""SP"",
                                                                                          ""ibge"": ""3503208"",
                                                                                          ""gia"": ""1818"",
                                                                                          ""ddd"": ""16"",
                                                                                          ""siafi"": ""6163""
                                                                                        }"));

            var request = await PostAsync("api/User", user);
            Assert.Equal(HttpStatusCode.Created, request.StatusCode);
        }

        /// <summary>
        /// Mock Mongo2go, WireMock
        /// </summary>
        [Fact(DisplayName = "Post deve Criar usuario Com com dados válidos e cep nao encontrado Deve retornar usuario criado Async")]
        public async Task Post_should_create_user_With_valid_data_and_postal_code_not_found_Must_return_user_created_Async()
        {
            var user = new UserCreateModel()
            {
                Document = "45166568095",
                FirstName = "FirstName",
                LastName = "LastName",
                PostalCode = "14804412"
            };

            httpMockServer.Reset();
            httpMockServer.Given(Request.Create().WithPath($"/ws/{user.PostalCode}/json").UsingGet())
                           .RespondWith(Response.Create().WithStatusCode(204).WithBody(@""));

            var request = await PostAsync("api/User", user);
            Assert.Equal(HttpStatusCode.Created, request.StatusCode);
        }
    }
}
