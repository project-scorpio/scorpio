using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;

using Scorpio.Authorization.Permissions;

namespace Scorpio.AspNetCore.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionChecker _permissionChecker;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissionChecker"></param>
        public PermissionRequirementHandler(IPermissionChecker permissionChecker) => _permissionChecker = permissionChecker;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            try
            {
                if (await _permissionChecker.CheckAsync(context.User, requirement.PermissionName))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            catch (PermissionNotFondException)
            {
                context.Fail();
            }
        }
    }
}
