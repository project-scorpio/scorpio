using System;

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
            _ = context.Services.AddQuartz(q =>
              {
                  q.UseMicrosoftDependencyInjectionScopedJobFactory(c => c.AllowDefaultConstructor = true);
                  q.UseSimpleTypeLoader();
                  q.UseInMemoryStore();
                  q.UseDefaultThreadPool(c => c.MaxConcurrency = 10);
                  q.UseTimeZoneConverter();
                  _ = q.UseXmlSchedulingConfiguration(x =>
                    {
                        x.Files = new[] { "~/quartz_jobs.config" };
                        x.ScanInterval = TimeSpan.FromSeconds(2);
                        x.FailOnFileNotFound = false;
                        x.FailOnSchedulingError = false;
                    });
              });
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
