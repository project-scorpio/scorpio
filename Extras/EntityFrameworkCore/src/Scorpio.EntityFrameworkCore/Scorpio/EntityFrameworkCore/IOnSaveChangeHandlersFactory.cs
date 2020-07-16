using System.Collections.Generic;

namespace Scorpio.EntityFrameworkCore
{

    /// <summary>
    /// 
    /// </summary>
    public interface IOnSaveChangeHandlersFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<IOnSaveChangeHandler> CreateHandlers();
    }
}
