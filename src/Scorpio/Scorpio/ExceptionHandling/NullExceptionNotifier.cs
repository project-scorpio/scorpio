using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Scorpio.ExceptionHandling
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class NullExceptionNotifier : IExceptionNotifier
    {
        /// <summary>
        /// 
        /// </summary>
        public static NullExceptionNotifier Instance { get; } = new NullExceptionNotifier();

        private NullExceptionNotifier()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task NotifyAsync(ExceptionNotificationContext context) => Task.CompletedTask;
    }
}