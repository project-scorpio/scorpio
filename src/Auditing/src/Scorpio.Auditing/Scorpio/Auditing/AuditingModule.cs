using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Scorpio.Modularity;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AuditingModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddOptions<AuditingOptions>();
            context.AddConventionalRegistrar<ConventionalRegistrar>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context) => context.Services.TryAddTransient<AuditingInterceptor>();
    }
}
