using System.Security.Principal;

namespace Scorpio.Authorization.Permissions
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionGrantingContext
    {
        /// <summary>
        /// 
        /// </summary>
        public PermissionDefinition Permission { get; }

        /// <summary>
        /// 
        /// </summary>
        public IPrincipal Principal { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permission"></param>
        /// <param name="principal"></param>
        public PermissionGrantingContext(PermissionDefinition permission, IPrincipal principal)
        {
            Check.NotNull(permission, nameof(permission));

            Permission = permission;
            Principal = principal;
        }

    }
}