using DependencyInjection.Common.MessageHandler;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependencyInjection.TestTools
{
    public abstract class WebJobsDependencyInjectionTestBase
    {
        private IServiceProvider serviceProvider { get; set; }

        protected void SetUpServiceProvider(
            string appSettingsPath,
            Action<IConfiguration> setConfiguration,
            Action<IServiceCollection> configureServices)
        {
            var configuration = new ConfigurationBuilder()
               .AddJsonFile(appSettingsPath)
               .Build();

            setConfiguration(configuration);

            var host = new HostBuilder()
                .ConfigureHostConfiguration(configBuilder =>
                {
                    configBuilder.AddConfiguration(configuration);
                })
                .ConfigureServices(configureServices)
                .Build();

            serviceProvider = host.Services;
        }

        protected IEnumerable<object> ResolveEntryPoints(Assembly assembly, params Type[] additionalTypesToResolve)
        {
            using var serviceScope = serviceProvider.CreateScope();

            var messageHandlerInterface = typeof(IMessageHandler);
            var backgroundServiceBaseClass = typeof(BackgroundService);
            var typesToResolve = assembly
                .GetTypes()
                .Where(x => x.IsClass && !x.IsInterface && !x.IsAbstract &&
                (messageHandlerInterface.IsAssignableFrom(x) || backgroundServiceBaseClass.IsAssignableFrom(x)))
                .ToList();
            typesToResolve.AddRange(additionalTypesToResolve);

            foreach (var typeToResolve in typesToResolve)
            {
                yield return ActivatorUtilities.CreateInstance(serviceProvider, typeToResolve);
            }
        }
    }
}
