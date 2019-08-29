using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Logging
{
    /// <summary>
    /// Interface to define a <see cref="LogLevel"/> property (see <see cref="LogLevel"/>).
    /// </summary>
    public interface IHasLogLevel
    {
        /// <summary>
        /// Log severity.
        /// </summary>
        LogLevel LogLevel { get; set; }
    }
}
