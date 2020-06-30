using System.Reflection;

using Scorpio.Conventional;

namespace Scorpio.DynamicProxy
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
