using DependencyInjection.Common.Dependencies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DependencyInjection.Jobs
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        static async Task Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var host = CreateHostBuilder(args).Build();

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(configBuilder =>
                {
                    configBuilder.AddConfiguration(Configuration);
                    configBuilder.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) => ConfigureServices(services));

        public static ServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Simulating a situation when appsettings is used in registering dependencies
            services.AddTransient<IDependency, Dependency>(s =>
                new Dependency(Configuration.GetValue<string>("DependencySettings:Value")));

            services.AddTransient<MesageHandlerWithDependency>();
            services.AddTransient<CustomClassWithDependency>();
            services.AddHostedService<BackgroundServiceWithDependency>();

            return services.BuildServiceProvider();
        }
    }
}
