using System.Reflection;

using Scorpio.Conventional;
using Scorpio.DynamicProxy;
using Scorpio.Uow;

namespace Scorpio.Application
{
    internal class ConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context) => 
            context.RegisterConventionalInterceptor(c => c.Where(t =>  t.AttributeExists<UnitOfWorkAttribute>()).Intercept<UnitOfWorkInterceptor>());
    }
}
