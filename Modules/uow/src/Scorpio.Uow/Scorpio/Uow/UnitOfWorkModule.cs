using Scorpio.Modularity;
using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class UnitOfWorkModule:ScorpioModule
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddOptions<UnitOfWorkDefaultOptions>();
            context.Services.TryAddTransient<UnitOfWorkInterceptor>();
            context.Services.TryAddTransient<IUnitOfWork, NullUnitOfWork>();
            context.Services.RegisterAssemblyByConventionOfType<UnitOfWorkModule>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PostConfigureServices(ConfigureServicesContext context)
        {
            base.PostConfigureServices(context);
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
