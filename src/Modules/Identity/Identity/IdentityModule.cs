using Identity.Keycloak.Features.Login;
using Microsoft.Extensions.DependencyInjection;

namespace Identity
{
    public static class IdentityModule
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services)
        {
            // Add services to the container.

            // Api Endpoint services

            // Application Use Case services
            services.AddHttpClient<LoginHandler>();
            // Data - Infrastructure services
            return services;
        }

        public static IApplicationBuilder UseIdentityModule(this IApplicationBuilder app)
        {

            // Container the HTTP request pipeline.

            // Use Api Endpoint services

            // Use Application Use Case services

            // Use Data - Infrastructure services
            return app;
        }
    }
}
