using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionRequirement: IAuthorizationRequirement
    {
        /// <summary>
        /// 
        /// </summary>
        public string PermissionName { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissionName"></param>
        public PermissionRequirement([NotNull]string permissionName)
        {
            Check.NotNull(permissionName, nameof(permissionName));

            PermissionName = permissionName;
        }

    }
}
