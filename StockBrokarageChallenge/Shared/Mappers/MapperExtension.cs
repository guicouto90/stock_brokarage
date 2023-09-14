using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBrokarageChallenge.Application.Shared.Mappers
{
    public static class MapperExtension
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ResourceMapperProfile));
            return services;
        }
    }
}
