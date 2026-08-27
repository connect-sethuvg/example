using Example.Data.Contracts;
using ExampleMS.Data.Entities;
using ExampleMS.Framework;
using ExampleMS.Framework.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Data
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddEntities(this IServiceCollection services) 
        {
            services.AddScoped<DbContext, ExampleMSContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            _ = services.AddTransient<IExample, ExampleData>();
            return services;
        }
    }
}
