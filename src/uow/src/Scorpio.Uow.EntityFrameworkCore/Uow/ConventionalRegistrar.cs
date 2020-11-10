using System.Reflection;

using Scorpio.Conventional;
using Scorpio.DynamicProxy;
using Scorpio.Repositories;

namespace Scorpio.Uow
{
    internal class ConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context) => context.RegisterConventionalInterceptor(c => c.Where(t => t.IsAssignableTo<IRepository>()).Intercept<UnitOfWorkInterceptor>());
    }
}
