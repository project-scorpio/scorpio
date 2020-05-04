using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Scorpio.EntityFrameworkCore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public static class DbContextConfigurationContextSqlServerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sqlServerOptionsAction"></param>
        /// <returns></returns>
        public static DbContextOptionsBuilder UseSqlServer(
             this DbContextConfigurationContext context,
             Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction = null)
        {
            if (context.ExistingConnection != null)
            {
                return context.DbContextOptions.UseSqlServer(context.ExistingConnection, sqlServerOptionsAction);
            }
            else
            {
                return context.DbContextOptions.UseSqlServer(context.ConnectionString, sqlServerOptionsAction);
            }
        }
    }
}
