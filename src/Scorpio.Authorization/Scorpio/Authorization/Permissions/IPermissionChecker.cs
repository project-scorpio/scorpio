using System.Security.Principal;
using System.Threading.Tasks;

namespace Scorpio.Authorization.Permissions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPermissionChecker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> CheckAsync(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> CheckAsync(IPrincipal claimsPrincipal, string name);

    }
}
