using System;
using System.Reflection;

namespace Scorpio.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInvocationAuthorizationContext
    {
        /// <summary>
        /// 
        /// </summary>
        string[] Permissions { get; }

        /// <summary>
        /// 
        /// </summary>
         bool RequireAllPermissions { get; }

        /// <summary>
        /// 
        /// </summary>
        MethodInfo Method { get; }

    }
}
