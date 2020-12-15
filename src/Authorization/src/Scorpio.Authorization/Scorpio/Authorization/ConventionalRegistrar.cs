using System;
using System.Linq;
using System.Reflection;

using Scorpio.Authorization;
using Scorpio.Conventional;
using Scorpio.DynamicProxy;

namespace Scorpio.Authorization
{
    internal class ConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context) => context.RegisterConventionalInterceptor(c => c.Where(t => ShouldIntercept(t)).Intercept<AuthorizationInterceptor>());

        private static bool ShouldIntercept(Type type)
        {

            if (ShouldAuditTypeByDefaultOrNull(type) == true)
            {
                return true;
            }

            if (type.GetMethods().Any(m => m.IsDefined(typeof(AuthorizeAttribute), true)))
            {
                return true;
            }

            return false;
        }

        //TODO: Move to a better place
        public static bool? ShouldAuditTypeByDefaultOrNull(Type type)
        {
            //TODO: In an inheritance chain, it would be better to check the attributes on the top class first.

            if (type.IsDefined(typeof(AuthorizeAttribute), true))
            {
                return true;
            }

            return null;
        }
    }
}
