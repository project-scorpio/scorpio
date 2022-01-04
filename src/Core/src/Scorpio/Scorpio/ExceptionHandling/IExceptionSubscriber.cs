using System.Threading.Tasks;

namespace Scorpio.ExceptionHandling
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExceptionSubscriber
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task HandleAsync(ExceptionNotificationContext context);
    }
}
