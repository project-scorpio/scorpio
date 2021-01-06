using System.Threading.Tasks;

using Scorpio.DependencyInjection;

namespace Scorpio.ExceptionHandling
{
    /// <summary>
    /// 
    /// </summary>
    [ExposeServices(typeof(IExceptionSubscriber))]
    public abstract class ExceptionSubscriber : IExceptionSubscriber, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract Task HandleAsync(ExceptionNotificationContext context);
    }
}