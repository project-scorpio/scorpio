using Scorpio.Application.Services;
using Scorpio.Conventional;
using Scorpio.DynamicProxy;
using Scorpio.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
