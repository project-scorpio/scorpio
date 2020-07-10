using System.Reflection;

using Scorpio.Application.Services;
using Scorpio.Conventional;
using Scorpio.DynamicProxy;
using Scorpio.Uow;

namespace Scorpio.Application
{
    class ConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.RegisterConventionalInterceptor(c =>
            {
                c.Where(t => t.IsAssignableTo<IApplicationService>()).Intercept<UnitOfWorkInterceptor>();
            });
        }
    }
}
