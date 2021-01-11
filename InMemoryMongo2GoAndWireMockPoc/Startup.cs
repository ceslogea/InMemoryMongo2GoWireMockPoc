using System;
using InMemoryMongo2GoAndWireMockPoc.Domain.Interface.ExternalResourcesRepository;
using InMemoryMongo2GoAndWireMockPoc.Domain.Interface.Query;
using InMemoryMongo2GoAndWireMockPoc.Domain.Interface.Repository;
using InMemoryMongo2GoAndWireMockPoc.Domain.Interface.Services;
using InMemoryMongo2GoAndWireMockPoc.Domain.Services;
using InMemoryMongo2GoAndWireMockPoc.Infra;
using InMemoryMongo2GoAndWireMockPoc.Infra.Query;
using InMemoryMongo2GoAndWireMockPoc.Infra.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;

namespace InMemoryMongo2GoAndWireMockPoc
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            if ("test".Equals(Environment.EnvironmentName))
                services.AddSingleton<IMongoContext, InMemoryMongoDbContext>();
            else
                services.AddSingleton<IMongoContext, MongoDbContext>();

            services.AddSingleton<IUserQueryRepository, UserQueryRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IUserService, UserService>();

            if ("test".Equals(Environment.EnvironmentName))
                Configuration["CepApiServiceHost"] = "http://localhost:9093";

            services.AddRefitClient<ICepApiService>().ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration["CepApiServiceHost"]));

            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
