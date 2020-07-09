using Microsoft.Extensions.DependencyInjection;

using Quartz;

using Scorpio.Modularity;

namespace Scorpio.Quartz
{

    /// <summary>
    /// 
    /// </summary>
    public class QuartzModule : ScorpioModule
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddConventionalRegistrar<ConventionalRegistrar>();
            base.PreConfigureServices(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddSingleton(sp => sp.GetService<ISchedulerFactory>().GetScheduler().Result);
            context.Services.AddHostedService<QuartzService>();
            context.Services.RegisterAssemblyByConvention();
            base.ConfigureServices(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void Initialize(ApplicationInitializationContext context)
        {
            base.Initialize(context);
        }
    }
}
