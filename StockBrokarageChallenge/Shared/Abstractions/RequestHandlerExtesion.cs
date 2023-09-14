using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBrokarageChallenge.Application.Shared.Abstractions
{
    public static class RequestHandlerExtesion
    {
        public static IServiceCollection AddRequestHandlers(this IServiceCollection services)
        {
            services.TryAddSingleton(typeof(IRequestHandlerCollection), typeof(RequestHandlerCollection));

            var types = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                types.AddRange(assembly.GetTypes().Where(x => x.GetInterfaces().Any(b => b.IsGenericType && (typeof(IRequestHandler<,>).Equals(b.GetGenericTypeDefinition()) ||
                    typeof(IRequestHandler<>).Equals(b.GetGenericTypeDefinition())) && !x.IsInterface && !x.IsAbstract)).ToList());
            }

            foreach (var type in types)
            {
                services.TryAddScoped(type);
            }

            return services;
        }
    }
}
