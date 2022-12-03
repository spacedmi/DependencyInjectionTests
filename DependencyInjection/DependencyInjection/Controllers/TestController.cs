using DependencyInjection.Common.Dependencies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> logger;
        private readonly IDependency dependency;

        public TestController(IDependency dependency, ILogger<TestController> logger)
        {
            this.logger = logger;
            this.dependency = dependency;
        }

        [HttpGet]
        public string Get()
        {
            logger.Log(LogLevel.Information, "Get method");
            return dependency.JustDependencyMethod();
        }
    }
}
