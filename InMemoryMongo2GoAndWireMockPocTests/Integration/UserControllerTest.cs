using InMemoryMongo2GoAndWireMockPocTests.Integration.WebAppFactory;

namespace InMemoryMongo2GoAndWireMockPocTests.Integration
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
    }
}
