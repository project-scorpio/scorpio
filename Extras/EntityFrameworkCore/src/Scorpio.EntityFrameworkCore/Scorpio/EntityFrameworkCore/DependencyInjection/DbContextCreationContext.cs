using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;

namespace Scorpio.EntityFrameworkCore.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public class DbContextCreationContext
    {
        /// <summary>
        /// 
        /// </summary>
        public static DbContextCreationContext Current => _current.Value;
        private static readonly AsyncLocal<DbContextCreationContext> _current = new AsyncLocal<DbContextCreationContext>();

        /// <summary>
        /// 
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// 
        /// </summary>
        public DbConnection ExistingConnection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public DbContextCreationContext( string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IDisposable Use(DbContextCreationContext context)
        {
            var previousValue = Current;
            _current.Value = context;
            return new DisposeAction(() => _current.Value = previousValue);
        }
    }
}
