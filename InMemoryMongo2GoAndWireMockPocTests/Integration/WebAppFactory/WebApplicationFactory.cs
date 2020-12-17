using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;

namespace InMemoryMongo2GoAndWireMockPocTests.Integration.WebAppFactory
{
    /// <summary>
    /// Gerencia o ciclo de vida da instancida do serviço e possibilita sobrescrever sua inicialização e funcionalidades.
    /// </summary>
    public class WebApplicationFactory : WebApplicationFactory<InMemoryMongo2GoAndWireMockPoc.Startup>
    {
        private const string ENV = "test";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Possibilita utilizar .env em esteiras.
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? ENV;
            builder.UseEnvironment(environment);
        }
    }
}
