using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public static class DbContextExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static bool HasRelationalTransactionManager(this DbContext dbContext)
        {
            return dbContext.Database.GetService<IDbContextTransactionManager>() is IRelationalTransactionManager;
        }

    }
}
