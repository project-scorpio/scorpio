using Scorpio.Conventional;
using Scorpio.Domain.Services;
using Scorpio.DynamicProxy;
using Scorpio.Uow;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Scorpio.Domain
{
    class ConventionalRegistrar : IConventionalRegistrar
    {

        public void Register(IConventionalRegistrationContext context)
        {
            context.RegisterConventionalInterceptor(c =>
            {
                c.Where(t => t.IsAssignableTo<IDomainService>()).Intercept<UnitOfWorkInterceptor>();
            });
        }
    }
}
