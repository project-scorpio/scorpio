using System;
using System.Data.Common;

using Microsoft.EntityFrameworkCore;

namespace Scorpio.EntityFrameworkCore.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public class DbContextConfigurationContext
    {
        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// 
        /// </summary>
        public DbConnection ExistingConnection { get; }

        /// <summary>
        /// 
        /// </summary>
        public DbContextOptionsBuilder DbContextOptions { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="existingConnection"></param>
        public DbContextConfigurationContext(
            string connectionString,
            IServiceProvider serviceProvider,
            DbConnection existingConnection)
        {
            ConnectionString = connectionString;
            ServiceProvider = serviceProvider;
            ExistingConnection = existingConnection;

            DbContextOptions = new DbContextOptionsBuilder();

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public class DbContextConfigurationContext<TDbContext> : DbContextConfigurationContext
        where TDbContext : ScorpioDbContext<TDbContext>
    {
        /// <summary>
        /// 
        /// </summary>
        public new DbContextOptionsBuilder<TDbContext> DbContextOptions => (DbContextOptionsBuilder<TDbContext>)base.DbContextOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="existingConnection"></param>
        public DbContextConfigurationContext(
            string connectionString,
            IServiceProvider serviceProvider,
            DbConnection existingConnection)
            : base(
                  connectionString,
                  serviceProvider,
                  existingConnection)
        {
            base.DbContextOptions = new DbContextOptionsBuilder<TDbContext>();
        }
    }
}
