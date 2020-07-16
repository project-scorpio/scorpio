using System.Threading.Tasks;

namespace Scorpio.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthorizationManager
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requireAllPermissions"></param>
        /// <param name="permissions"></param>
        Task AuthorizeAsync(bool requireAllPermissions, params string[] permissions);
    }
}
