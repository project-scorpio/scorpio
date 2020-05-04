using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Scorpio.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EntityFrameworkCore.DependencyInjection
{
    internal static class DbContextOptionsFactory
    {
        public static DbContextOptions<TDbContext> Create<TDbContext>(IServiceProvider serviceProvider, ScorpioDbContextOptionsBuilder<TDbContext> optionsBuilder)
            where TDbContext : ScorpioDbContext<TDbContext>
        {
            var creationContext = GetCreationContext<TDbContext>(serviceProvider);

            var context = new DbContextConfigurationContext<TDbContext>(
                creationContext.ConnectionString,
                serviceProvider,
                creationContext.ExistingConnection
            );
            context.DbContextOptions.UseApplicationServiceProvider(serviceProvider);

            var options = GetDbContextOptions<TDbContext>(serviceProvider);
            PreConfigure(options, context);
            Configure(options, context);
            optionsBuilder.OptionsActions.ForEach(action => action?.Invoke(context.DbContextOptions));
            return context.DbContextOptions.Options;
        }

        private static void PreConfigure<TDbContext>(
            ScorpioDbContextOptions options,
            DbContextConfigurationContext<TDbContext> context)
            where TDbContext : ScorpioDbContext<TDbContext>
        {
            foreach (var defaultPreConfigureAction in options.DefaultPreConfigureActions)
            {
                defaultPreConfigureAction.Invoke(context);
            }

            var preConfigureActions = options.PreConfigureActions.GetOrDefault(typeof(TDbContext));
            if (!preConfigureActions.IsNullOrEmpty())
            {
                foreach (var preConfigureAction in preConfigureActions)
                {
                    ((Action<DbContextConfigurationContext<TDbContext>>)preConfigureAction).Invoke(context);
                }
            }
        }

        private static void Configure<TDbContext>(
            ScorpioDbContextOptions options,
            DbContextConfigurationContext<TDbContext> context)
            where TDbContext : ScorpioDbContext<TDbContext>
        {
            var configureAction = options.ConfigureActions.GetOrDefault(typeof(TDbContext));
            if (configureAction != null)
            {
                ((Action<DbContextConfigurationContext<TDbContext>>)configureAction).Invoke(context);
            }
            else if (options.DefaultConfigureAction != null)
            {
                options.DefaultConfigureAction.Invoke(context);
            }
            else
            {
                throw new ScorpioException(
                    $"No configuration found for {typeof(DbContext).AssemblyQualifiedName}! Use services.Configure<ScorpioDbContextOptions>(...) to configure it.");
            }
        }

        private static ScorpioDbContextOptions GetDbContextOptions<TDbContext>(IServiceProvider serviceProvider)
            where TDbContext : ScorpioDbContext<TDbContext>
        {
            return serviceProvider.GetRequiredService<IOptions<ScorpioDbContextOptions>>().Value;
        }

        private static DbContextCreationContext GetCreationContext<TDbContext>(IServiceProvider serviceProvider)
            where TDbContext : ScorpioDbContext<TDbContext>
        {
            var context = DbContextCreationContext.Current;
            if (context != null)
            {
                return context;
            }

            var connectionStringName = ConnectionStringNameAttribute.GetConnStringName<TDbContext>();
            var connectionString = serviceProvider.GetRequiredService<IConnectionStringResolver>().Resolve(connectionStringName);

            return new DbContextCreationContext(
                connectionString
            );
        }
    }
}
