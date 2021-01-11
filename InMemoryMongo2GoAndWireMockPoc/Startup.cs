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
            //Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
            Configuration = configuration;
            Env = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IMongoContext, MongoDbContext>();
            services.AddSingleton<IUserQueryRepository, UserQueryRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IUserService, UserService>();
            services.AddRefitClient<ICepApiService>().ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration["CepApiServiceHost"]));
            services.AddLogging();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

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
