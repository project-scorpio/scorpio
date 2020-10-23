using Microsoft.Extensions.DependencyInjection;

using Scorpio.EntityFrameworkCore;
using Scorpio.Modularity;

namespace Scorpio.Uow
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(UnitOfWorkModule))]
    [DependsOn(typeof(EntityFrameworkCoreModule))]
    public sealed class UnitOfWorkEntityFrameworkCoreModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.AddConventionalRegistrar<ConventionalRegistrar>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.ReplaceTransient<IUnitOfWork, EfUnitOfWork>();
            context.Services.AddTransient<IEfTransactionStrategy, UnitOfWorkEfTransactionStrategy>();
            context.Services.ReplaceOrAdd(ServiceDescriptor.Transient(typeof(IDbContextProvider<>), typeof(UnitOfWorkDbContextProvider<>)), true);
        }
    }
}
