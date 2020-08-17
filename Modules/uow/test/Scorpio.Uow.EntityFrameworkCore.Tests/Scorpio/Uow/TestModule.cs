using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NSubstitute.Extensions;

using Scorpio.Data;
using Scorpio.EntityFrameworkCore;
using Scorpio.Modularity;

namespace Scorpio.Uow
{
    [DependsOn(typeof(UnitOfWorkEntityFrameworkCoreModule))]
    public class TestModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddScorpioDbContext<TestDbContext>();
            context.Services.Configure<ScorpioDbContextOptions>(opt =>
            {
                opt.PreConfigure<TestDbContext>(c => c.DbContextOptions.ConfigureWarnings(w =>
                {
                    w.Default(WarningBehavior.Ignore);
                    w.Ignore(InMemoryEventId.TransactionIgnoredWarning);
                }));

                opt.Configure<TestDbContext>(c => c.DbContextOptions.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            });

            context.Services.PreConfigure<DataFilterOptions>(options =>
            {
            });
            context.RegisterAssemblyByConvention();
        }
    }
}
