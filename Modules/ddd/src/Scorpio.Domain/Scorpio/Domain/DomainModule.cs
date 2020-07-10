using Microsoft.Extensions.DependencyInjection;

using Scorpio.Data;
using Scorpio.Modularity;
using Scorpio.Uow;

namespace Scorpio.Domain
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(UnitOfWorkModule))]
    [DependsOn(typeof(DataModule))]
    public sealed class DomainModule : ScorpioModule
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddConventionalRegistrar<ConventionalRegistrar>();
        }
       
    }
}
