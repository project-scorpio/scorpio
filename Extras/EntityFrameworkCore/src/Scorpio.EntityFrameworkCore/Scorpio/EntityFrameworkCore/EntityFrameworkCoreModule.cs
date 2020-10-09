using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Scorpio.Data;
using Scorpio.EntityFrameworkCore.DependencyInjection;
using Scorpio.Modularity;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(DataModule))]
    public sealed class EntityFrameworkCoreModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.Configure<ScorpioDbContextOptions>(options =>
            {
                options.PreConfigure(context => context.DbContextOptions.ReplaceService<IAsyncQueryProvider, QueryProvider>());
                options.PreConfigure(context => context.DbContextOptions.UseLazyLoadingProxies());
                options.AddModelCreatingContributor<DataModelCreatingContributor>();
                options.PreConfigure(dbConfigContext => dbConfigContext.DbContextOptions.ConfigureWarnings(
                    warnings => warnings.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning)
                    ));
            });
            context.Services.Configure<DbConnectionOptions>(context.Configuration);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.ScorpioDbContext(builder =>
            {
                builder.AddSaveChangeHandler<SoftDeleteSaveChangeHandler>();
                builder.AddSaveChangeHandler<HasExtraPropertiesSaveChangeHandler>();
            });
            context.Services.TryAddTransient<IOnSaveChangeHandlersFactory, OnSaveChangeHandlersFactory>();
            context.Services.TryAddTransient(typeof(IDbContextProvider<>), typeof(DefaultDbContextProvider<>));
            context.Services.RegisterAssemblyByConvention();
            base.ConfigureServices(context);
        }
    }
}
