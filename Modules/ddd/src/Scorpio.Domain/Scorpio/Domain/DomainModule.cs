using Scorpio.Modularity;
using Scorpio.Threading;
using Scorpio.Uow;
using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Data;

namespace Scorpio.Domain
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(UnitOfWorkModule))]
    [DependsOn(typeof(DataModule))]
    public sealed class DomainModule:ScorpioModule
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddConventionalRegistrar<ConventionaInterceptorRegistrar>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            
        }
    }
}
