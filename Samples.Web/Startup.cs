using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.GenericRepository.Context;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Persistence;
using MongoDB.GenericRepository.Repository;
using MongoDB.GenericRepository.UoW;
using System;

namespace MongoDB.GenericRepository
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // Configure the persistence in another layer
            MongoDbPersistence.Configure();
            
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var serviceProvider = app.ApplicationServices;

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
              //  endpoints.MapHealthChecks("/");
                endpoints.MapControllers();
            });

            var endPoint = Configuration.GetValue<string>("Kestrel:EndPoints:Http:Url");
            Console.WriteLine($"WEB SERVER RUNNING ON:{endPoint}");           
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
