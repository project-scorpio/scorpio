using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IScorpioDbContextOptionsBuilder<TDbContext> UseOptions<TDbContext>(this IScorpioDbContextOptionsBuilder<TDbContext> optionsBuilder, Action<DbContextOptionsBuilder<TDbContext>> optionsAction)
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
