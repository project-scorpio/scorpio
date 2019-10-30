using System;
using System.Collections.Generic;
using System.Text;

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
