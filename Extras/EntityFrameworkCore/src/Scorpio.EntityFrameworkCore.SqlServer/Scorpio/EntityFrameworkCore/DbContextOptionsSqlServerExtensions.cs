using System;

using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public static class DbContextOptionsSqlServerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="sqlServerOptionsAction"></param>
        public static void UseSqlServer(
             this ScorpioDbContextOptions options,
             Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction = null)
        {
            options.Configure(context =>
            {
                context.UseSqlServer(sqlServerOptionsAction);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="options"></param>
        /// <param name="sqlServerOptionsAction"></param>
        public static void UseSqlServer<TDbContext>(
             this ScorpioDbContextOptions options,
             Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction = null)
            where TDbContext : ScorpioDbContext<TDbContext>
        {
            options.Configure<TDbContext>(context =>
            {
                context.UseSqlServer(sqlServerOptionsAction);
            });
        }
    }
}
