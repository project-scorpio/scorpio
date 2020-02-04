using Scorpio.EntityFrameworkCore;
using Scorpio.Uow;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEfTransactionStrategy
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

        /// <summary>
        /// </summary>
        void Dispose();
    }
}
