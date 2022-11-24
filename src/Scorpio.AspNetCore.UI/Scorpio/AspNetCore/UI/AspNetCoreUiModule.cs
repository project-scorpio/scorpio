using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity;

namespace Scorpio.AspNetCore.UI
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(AspNetCoreModule))]
    public sealed class AspNetCoreUiModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ConfigureServicesContext context) => context.Services.AddConventionalRegistrar<AspNetCoreUiConventionalRegistrar>();

    }
}
