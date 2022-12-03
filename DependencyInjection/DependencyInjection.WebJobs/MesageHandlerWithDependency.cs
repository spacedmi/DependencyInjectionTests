using DependencyInjection.Common.Dependencies;
using DependencyInjection.Common.MessageHandler;

namespace DependencyInjection.Jobs
{
    public class MesageHandlerWithDependency : IMessageHandler
    {
        private readonly IDependency dependency;

        public MesageHandlerWithDependency(IDependency dependency)
        {
            this.dependency = dependency;
        }
    }
}