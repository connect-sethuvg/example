using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Business
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddBusinessService (this IServiceCollection services)
        {

            return services;
        }
    }
}
