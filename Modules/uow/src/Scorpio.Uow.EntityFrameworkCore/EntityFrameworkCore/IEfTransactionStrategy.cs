using System;

using Scorpio.Uow;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEfTransactionStrategy : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        void InitOptions(UnitOfWorkOptions options);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        TDbContext CreateDbContext<TDbContext>(string connectionString)
            where TDbContext : ScorpioDbContext<TDbContext>;

        /// <summary>
        /// 
        /// </summary>
        void Commit();


    }
}
