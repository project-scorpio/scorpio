using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Authorization.Permissions
{
    /// <summary>
    /// 
    /// </summary>
   public interface IPermissionDefinitionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void Define(IPermissionDefinitionContext context);
    }
}
