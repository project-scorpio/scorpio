using Microsoft.Extensions.DependencyInjection;
using Scorpio.EntityFrameworkCore;
using Scorpio.Modularity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Uow.EntityFrameworkCore.Uow
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(UnitOfWorkModule))]
    [DependsOn(typeof(EntityFrameworkCoreModule))]
    public sealed class UnitOfWorkEntityFrameworkCoreModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.ReplaceTransient<IUnitOfWork, EfUnitOfWork>();
            context.Services.AddTransient<IEfTransactionStrategy, UnitOfWorkEfTransactionStrategy>();
            context.Services.ReplaceOrAdd(ServiceDescriptor.Transient(typeof(IDbContextProvider<>), typeof(UnitOfWorkDbContextProvider<>)),true);
            context.Services.RegisterAssemblyByConvention();
        }
    }
}
