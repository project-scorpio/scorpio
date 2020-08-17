using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using Scorpio.Conventional;
using Scorpio.DynamicProxy;
using Scorpio.Repositories;

namespace Scorpio.Uow
{
    class ConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.RegisterConventionalInterceptor(c =>
            {
                c.Where(t => t.IsAssignableTo<IRepository>()).Intercept<UnitOfWorkInterceptor>();
            });
        }
    }
}
