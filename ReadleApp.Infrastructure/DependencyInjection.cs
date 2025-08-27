using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadleApp.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadleApp.Infrastructure
{
   public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration Configuration)
        {

          
            return services;
        }

    }
}
