using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// 
    /// </summary>
    public class BackgroundJobsAbstractionsModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.AddConventionalRegistrar<BackgroundJobsConventionalRegistrar>();
        }
    }
}
