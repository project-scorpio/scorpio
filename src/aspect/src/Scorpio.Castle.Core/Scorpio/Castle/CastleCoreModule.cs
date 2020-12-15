
using Microsoft.Extensions.DependencyInjection;

using Scorpio.Castle.DynamicProxy;
using Scorpio.Modularity;

namespace Scorpio.Castle
{
    /// <summary>
    /// 
    /// </summary>
    public class CastleCoreModule:ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context) => 
            context.Services.AddTransient(typeof(AsyncDeterminationInterceptor<>));
    }
}
