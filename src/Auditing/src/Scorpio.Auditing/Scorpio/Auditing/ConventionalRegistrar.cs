using System;
using System.Linq;
using System.Reflection;

using Scorpio.Conventional;
using Scorpio.DynamicProxy;

namespace Scorpio.Auditing
{
    internal class ConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context) => context.RegisterConventionalInterceptor(c => c.Where(t => ShouldIntercept(t)).Intercept<AuditingInterceptor>());

        private static bool ShouldIntercept(Type type)
        {

            if (ShouldAuditTypeByDefaultOrNull(type) == true)
            {
                return true;
            }

            if (type.GetMethods().Any(m => m.IsDefined(typeof(AuditedAttribute), true)))
            {
                return true;
            }

            return false;
        }

        //TODO: Move to a better place
        public static bool? ShouldAuditTypeByDefaultOrNull(Type type)
        {
            //TODO: In an inheritance chain, it would be better to check the attributes on the top class first.

            if (type.IsDefined(typeof(AuditedAttribute), true))
            {
                return true;
            }

            if (type.IsDefined(typeof(DisableAuditingAttribute), true))
            {
                return false;
            }

            if (typeof(IAuditingEnabled).IsAssignableFrom(type))
            {
                return true;
            }

            return null;
        }
    }
}
