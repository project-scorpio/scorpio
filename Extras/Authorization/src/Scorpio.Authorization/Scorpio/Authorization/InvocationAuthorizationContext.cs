using System;
using System.Reflection;

namespace Scorpio.Authorization
{
    internal class InvocationAuthorizationContext:IInvocationAuthorizationContext
    {
        public InvocationAuthorizationContext(string[] permissions, bool requireAllPermissions,MethodInfo method)
        {
            Permissions = permissions;
            RequireAllPermissions = requireAllPermissions;
            Method = method;
        }

        public string[] Permissions { get;}

        public bool RequireAllPermissions { get; }

        public MethodInfo Method { get; }
    }
}
