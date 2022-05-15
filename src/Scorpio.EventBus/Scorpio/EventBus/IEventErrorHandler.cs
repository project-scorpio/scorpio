using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventErrorHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task HandleAsync(EventExecutionErrorContext context);
    }
}
