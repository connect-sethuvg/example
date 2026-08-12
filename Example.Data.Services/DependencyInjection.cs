using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Data.Services
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddDataService (this IServiceCollection services)
        {
            // Register DbContext
            //services.AddDbContext<DressStoreDbContext>(options =>options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register Repositories (Interface → Implementation)

            return services;
        }
    }
}
