using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationCoreServices(this IServiceCollection services, IConfiguration configuration, params Type[] types)
        {
            services.AddAutoMapper(types);
            services.AddMediatR(types);

            return services;
        }
    }
}
