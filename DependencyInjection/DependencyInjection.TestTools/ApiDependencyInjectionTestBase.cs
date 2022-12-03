using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependencyInjection.TestTools
{
    public abstract class ApiDependencyInjectionTestBase<TStartup>
        where TStartup : class
    {
        protected IEnumerable<object> ResolveEntryPoints()
        {
            var host = WebHost.CreateDefaultBuilder()
               .UseStartup<TStartup>()
               .Build();

            var serviceProvider = host.Services;

            using var serviceScope = serviceProvider.CreateScope();

            var baseClassType = typeof(ControllerBase);
            var typesToResolve = typeof(TStartup).Assembly
                .GetTypes()
                .Where(x => x.IsClass && !x.IsInterface && !x.IsAbstract && baseClassType.IsAssignableFrom(x))
                .ToList();

            foreach (var typeToResolve in typesToResolve)
            {
                yield return ActivatorUtilities.CreateInstance(serviceProvider, typeToResolve);
            }
        }
    }
}
