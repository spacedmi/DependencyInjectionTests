using DependencyInjection.Common.Dependencies;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DependencyInjection.Jobs
{
    internal class BackgroundServiceWithDependency : BackgroundService
    {
        private readonly IDependency dependency;

        public BackgroundServiceWithDependency(IDependency dependency)
        {
            this.dependency = dependency;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine($"Started {nameof(BackgroundServiceWithDependency)}. Dependency output:");
            Console.WriteLine(dependency.JustDependencyMethod());

            return Task.CompletedTask;
        }
    }
}