using DependencyInjection.Jobs;
using DependencyInjection.TestTools;
using System;
using Xunit;

namespace DependencyInjection.WebJobs.Tests
{
    public class WebJobsDependencyInjectionTests : WebJobsDependencyInjectionTestBase
    {
        [Fact]
        public void DependenciesRegistrationTest()
        {
            SetUpServiceProvider(appSettingsPath: "appsettings.json",
                configuration => Program.Configuration = configuration,
                services => Program.ConfigureServices(services));

            var instances = ResolveEntryPoints(typeof(MesageHandlerWithDependency).Assembly, typeof(CustomClassWithDependency));

            Assert.NotEmpty(instances);
            foreach (var instance in instances)
            {
                Assert.NotNull(instance);
            }
        }
    }
}
