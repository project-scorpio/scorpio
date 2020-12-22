using System;
using System.Linq;

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

        public static bool? ShouldAuditTypeByDefaultOrNull(Type type)
        {

            if (type.IsDefined(typeof(AuthorizeAttribute), true))
            {
                return true;
            }

            return null;
        }
    }
}
