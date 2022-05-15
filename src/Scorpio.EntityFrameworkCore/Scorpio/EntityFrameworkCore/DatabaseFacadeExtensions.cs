using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.EntityFrameworkCore
{
#if NETSTANDARD2_0
    /// <summary>
    /// 
    /// </summary>
    public static class DatabaseFacadeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        public static bool IsRelational(this DatabaseFacade database) => database.GetInfrastructure().GetService<IRelationalConnection>() != null;
    }

#endif
}
