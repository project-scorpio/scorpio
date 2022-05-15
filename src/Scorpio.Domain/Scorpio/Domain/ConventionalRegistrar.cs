using System.Reflection;

using Scorpio.Conventional;
using Scorpio.Domain.Services;
using Scorpio.DynamicProxy;
using Scorpio.Uow;

namespace Scorpio.Domain
{
    internal class ConventionalRegistrar : IConventionalRegistrar
    {

        public void Register(IConventionalRegistrationContext context) => context.RegisterConventionalInterceptor(c => c.Where(t => t.IsAssignableTo<IDomainService>()).Intercept<UnitOfWorkInterceptor>());
    }
}
