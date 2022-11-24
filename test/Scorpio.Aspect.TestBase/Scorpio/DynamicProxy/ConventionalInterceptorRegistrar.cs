using System.Reflection;

using Scorpio.Conventional;
using Scorpio.DynamicProxy;
using Scorpio.DynamicProxy.TestClasses;

namespace Scorpio
{
    public class TestConventionalInterceptorRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context) =>
            context.RegisterConventionalInterceptor(c => c.Where(t => t.IsAssignableTo<IProxiedService>()).Intercept<TestInterceptor>());
    }
}
