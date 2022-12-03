using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace DependencyInjection.Api.Dependencies
{
    public class Dependency : IDependency
    {
        private readonly string value;

        public Dependency(string value) 
            => this.value = value ?? throw new ArgumentNullException("Value cannot be Null");

        public string JustDependencyMethod()
        {
            return $"Dependency has been resolved. Value from settings: {value}";
        }
    }
}
