using DependencyInjection.TestTools;
using System;
using Xunit;

namespace DependencyInjection.Api.Tests
{
    public class ApiDependencyInjectionTests : ApiDependencyInjectionTestBase<Startup>
    {
        [Fact]
        public void DependenciesRegistrationTest()
        {
            var instances = ResolveEntryPoints();

            Assert.NotEmpty(instances);
            foreach (var instance in instances)
            {
                Assert.NotNull(instance);
            }
        }
    }
}
