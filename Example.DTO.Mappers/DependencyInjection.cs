using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.DTO.Mappers
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddMapperServices(this IServiceCollection services)
        {
            // Register all mapper classes here
            //_ = services.AddScoped<APIDataMapper<IAgent, HierarchyDTO>, HierarchyMapper>();


            return services;
        }
    }
}
