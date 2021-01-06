using System.Threading.Tasks;

namespace Scorpio.ExceptionHandling
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExceptionNotifier
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task NotifyAsync(ExceptionNotificationContext context);
    }
}