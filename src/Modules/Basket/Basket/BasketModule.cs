using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Interceptors;

namespace Basket
{
    public static class BasketModule
    {
        public static IServiceCollection AddBasketModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.

            // Api Endpoint services

            // Application Use Case services
            services.AddScoped<IBasketRepository, BasketRepository>();
            
            // Data - Infrastructure services
            var connectionString = configuration.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<BasketDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            });

            return services;
        }

        public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
        {

            // Configure the HTTP request pipeline.

            // Use Api Endpoint services

            // Use Application Use Case services

            // Use Data - Infrastructure services
            app.UseMigration<BasketDbContext>();

            return app;
        }
    }
}
