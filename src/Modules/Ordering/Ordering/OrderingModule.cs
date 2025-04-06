using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering
{
    public static class OrderingModule
    {
        public static IServiceCollection AddOrderingModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.

            //services
            //    .AddApplicationServices()
            //    .AddInfrastructureServices(configuration)
            //    .AddApiServices(configuration);

            return services;
        }

        public static IApplicationBuilder UseOrderingModule(this IApplicationBuilder app)
        {

            // Container the HTTP request pipeline.

            //services
            //    .UseApplicationServices()
            //    .UseInfrastructureServices(configuration)
            //    .UseApiServices(configuration);

            return app;
        }
    }
}
