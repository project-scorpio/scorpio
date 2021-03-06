﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Scorpio.Application;
using Scorpio.Modularity;
namespace Scorpio.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class UnitOfWorkModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ConfigureServicesContext context) => context.AddConventionalRegistrar<ConventionalRegistrar>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddOptions<UnitOfWorkDefaultOptions>();
            context.Services.TryAddTransient<UnitOfWorkInterceptor>();
            context.Services.TryAddTransient<IUnitOfWork, NullUnitOfWork>();
        }

    }
}
