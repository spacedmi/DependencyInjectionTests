using DependencyInjection.Common.Dependencies;

namespace DependencyInjection.Jobs
{
    public class CustomClassWithDependency
    {
        private readonly IDependency dependency;

        public CustomClassWithDependency(IDependency dependency)
        {
            this.dependency = dependency;
        }
    }
}