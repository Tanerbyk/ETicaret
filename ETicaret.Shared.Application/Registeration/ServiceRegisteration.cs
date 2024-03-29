﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediatR;

using System.Text;
using System.Threading.Tasks;
using ETicaret.Shared.Application.Features.Category.Queries;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;

namespace ETicaret.Shared.Application.Registeration
{
    public static class ServiceRegisteration
    {

        public static IServiceCollection ApplicationRegisteration(this IServiceCollection services, params Type[] types)
        {

            var assm = Assembly.GetExecutingAssembly();
            services.AddMediatR(assm);
            services.AddAutoMapper(assm);
            return services;
        }

    }
}
