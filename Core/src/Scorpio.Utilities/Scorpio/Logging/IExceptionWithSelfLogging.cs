using Microsoft.Extensions.Logging;

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
