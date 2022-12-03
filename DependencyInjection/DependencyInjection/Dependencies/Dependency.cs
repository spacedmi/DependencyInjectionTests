using System;

namespace DependencyInjection.Api.Dependencies
{
    public class Dependency : IDependency
    {
        public string JustDependencyMethod()
        {
            return "Dependency has been resolved";
        }
    }
}
