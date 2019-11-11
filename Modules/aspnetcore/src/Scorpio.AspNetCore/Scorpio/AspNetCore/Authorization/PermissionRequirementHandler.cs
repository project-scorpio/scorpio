using Microsoft.AspNetCore.Authorization;
using Scorpio.Authorization.Permissions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        public PermissionRequirementHandler(IPermissionChecker permissionChecker)
        {
            _permissionChecker = permissionChecker;
        }

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
            }
            catch (PermissionNotFondException)
            {
                context.Fail();
            }
        }
    }
}
