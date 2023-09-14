using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data;
using StockBrokarageChallenge.Application.Shared.Mappers;
using StockBrokarageChallenge.Application.Shared.Security;

namespace StockBrokarageChallenge.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModules(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJwtExtension(configuration);
            services.AddRequestHandlers();
            services.AddMapping();
            
            services.AddSwaggerExtension();
            services.AddDbInfra(configuration);

            return services;
        }
    }
}
