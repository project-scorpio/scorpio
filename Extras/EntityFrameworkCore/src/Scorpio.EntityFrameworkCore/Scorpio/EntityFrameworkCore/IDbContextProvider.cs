using Microsoft.EntityFrameworkCore;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IDbContextProvider<out TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TDbContext GetDbContext();

    }
}
