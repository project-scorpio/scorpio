using System.Reflection;

using Scorpio.Conventional;
using Scorpio.DynamicProxy;

namespace Scorpio
{
    class TestConventionalInterceptorRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.RegisterConventionalInterceptor(c =>
            {
                c.Where(t => t.IsAssignableTo<IInterceptorable>()).Intercept<TestInterceptor>();
            });
        }
    }
}
