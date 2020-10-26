using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Scorpio.Data;
using Scorpio.Modularity;

using Z.EntityFramework.Extensions;

namespace Scorpio.EntityFrameworkCore
{
    [DependsOn(typeof(EntityFrameworkCoreModule))]
    public sealed class TestModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddScorpioDbContext<TestDbContext>();
            context.Services.Configure<ScorpioDbContextOptions>(opt =>
            {
                opt.PreConfigure<TestDbContext>(c => c.DbContextOptions.ConfigureWarnings(w => w.Default(WarningBehavior.Ignore)));
                opt.Configure<TestDbContext>(c => c.DbContextOptions.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            });
            context.Services.PreConfigure<DataFilterOptions>(options =>
            {
                options.Configure<IStringValue>(f => f.Expression(d => d.StringValue != f.FilterContext.GetParameter<IStringValueProvider>().Value));
            });
        }

        public override void Initialize(ApplicationInitializationContext context)
        {
            EntityFrameworkManager.ContextFactory = c => c;
            base.Initialize(context);
        }
    }
}
