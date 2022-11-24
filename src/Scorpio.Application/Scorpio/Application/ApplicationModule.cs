using Scorpio.Data;
using Scorpio.Modularity;
using Scorpio.Uow;

namespace Scorpio.Application
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(DataModule))]
    [DependsOn(typeof(UnitOfWorkModule))]
    public class ApplicationModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ConfigureServicesContext context) => context.AddConventionalRegistrar<ConventionalRegistrar>();


    }
}
