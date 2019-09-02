using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExceptionWithSelfLogging
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        void Log(ILogger logger);
    }
}
