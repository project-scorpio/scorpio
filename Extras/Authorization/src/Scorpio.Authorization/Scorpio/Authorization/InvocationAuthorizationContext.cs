using System;
using System.Reflection;

namespace Scorpio.Authorization
{
    internal class InvocationAuthorizationContext:IInvocationAuthorizationContext
    {
        public InvocationAuthorizationContext(string[] permissions, bool requireAllPermissions,MethodBase method)
        {
            Permissions = permissions;
            RequireAllPermissions = requireAllPermissions;
            Method = method;
        }

        public string[] Permissions { get;}

        public bool RequireAllPermissions { get; }

        public MethodBase Method { get; }
    }
}
