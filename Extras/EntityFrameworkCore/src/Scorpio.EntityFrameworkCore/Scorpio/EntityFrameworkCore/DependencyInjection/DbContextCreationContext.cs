using System;
using System.Data.Common;
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
        public string ConnectionString { get; }

        /// <summary>
        /// 
        /// </summary>
        public DbConnection ExistingConnection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public DbContextCreationContext(string connectionString)
        {
            ConnectionString = connectionString;
        }


    }
}
