using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOnSaveChangeHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        Task PreSaveChangeAsync(IEnumerable<EntityEntry> entries);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        Task PostSaveChangeAsync(IEnumerable<EntityEntry> entries);
    }
}
