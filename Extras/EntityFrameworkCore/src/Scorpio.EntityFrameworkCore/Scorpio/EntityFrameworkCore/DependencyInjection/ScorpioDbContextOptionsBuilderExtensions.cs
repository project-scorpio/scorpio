using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

using Scorpio.Repositories;
using Scorpio.Repositories.EntityFrameworkCore;

namespace Scorpio.EntityFrameworkCore.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class ScorpioDbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="optionsBuilder"></param>
        /// <param name="defaultRepositoryType"></param>
        public static IScorpioDbContextOptionsBuilder<TDbContext> SetDefaultRepository<TDbContext>(this IScorpioDbContextOptionsBuilder<TDbContext> optionsBuilder, Type defaultRepositoryType)
            where TDbContext : ScorpioDbContext<TDbContext>
        {
            if (!defaultRepositoryType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEfCoreRepository<>)))
            {
                throw new ScorpioException($"Given repositoryType is not a repository. It must implement {typeof(IRepository<>).AssemblyQualifiedName}.");
            }
            if (optionsBuilder is ScorpioDbContextOptionsBuilder<TDbContext> builder)
            {
                builder.DefaultRepositoryType = defaultRepositoryType;
            }
            return optionsBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="optionsBuilder"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        private static IScorpioDbContextOptionsBuilder<TDbContext> UseOptions<TDbContext>(this IScorpioDbContextOptionsBuilder<TDbContext> optionsBuilder, Action<DbContextOptionsBuilder<TDbContext>> optionsAction)
               where TDbContext : ScorpioDbContext<TDbContext>
        {
            if (optionsBuilder is ScorpioDbContextOptionsBuilder<TDbContext> builder)
            {
                builder.OptionsActions.AddIfNotContains(optionsAction);
            }
            return optionsBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="optionsBuilder"></param>
        /// <param name="loggerFactory"></param>
        /// <returns></returns>
        public static IScorpioDbContextOptionsBuilder<TDbContext> UseLoggerFactory<TDbContext>(this IScorpioDbContextOptionsBuilder<TDbContext> optionsBuilder, ILoggerFactory loggerFactory)
       where TDbContext : ScorpioDbContext<TDbContext>
        {
            return optionsBuilder.UseOptions(b => b.UseLoggerFactory(loggerFactory));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="optionsBuilder"></param>
        /// <param name="memoryCache"></param>
        /// <returns></returns>
        public static IScorpioDbContextOptionsBuilder<TDbContext> UseMemoryCache<TDbContext>(this IScorpioDbContextOptionsBuilder<TDbContext> optionsBuilder, IMemoryCache memoryCache)
       where TDbContext : ScorpioDbContext<TDbContext>
        {
            return optionsBuilder.UseOptions(b => b.UseMemoryCache(memoryCache));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="optionsBuilder"></param>
        /// <returns></returns>
        public static IScorpioDbContextOptionsBuilder AddSaveChangeHandler<THandler>(this IScorpioDbContextOptionsBuilder optionsBuilder)
                        where THandler : class, IOnSaveChangeHandler
        {
            if (optionsBuilder is ScorpioDbContextOptionsBuilder builder)
            {
                builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IOnSaveChangeHandler, THandler>());
            }
            return optionsBuilder;
        }
    }
}
