using System;
using System.Threading.Tasks;

namespace Scorpio.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorizationContext"></param>
        /// <returns></returns>
        Task CheckAsync(IInvocationAuthorizationContext authorizationContext);
    }
}
