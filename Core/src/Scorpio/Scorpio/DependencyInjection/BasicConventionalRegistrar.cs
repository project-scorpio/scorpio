using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;

namespace Scorpio.DependencyInjection
{
    class BasicConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.Services.RegisterAssembly(context.Assembly, config =>
            {
                config.Where(t => t.IsAssignableTo<ISingletonDependency>()).AsDefault().Lifetime(ServiceLifetime.Singleton);
                config.Where(t => t.IsAssignableTo<ITransientDependency>()).AsDefault().Lifetime(ServiceLifetime.Transient);
                config.Where(t => t.IsAssignableTo<IScopedDependency>()).AsDefault().Lifetime(ServiceLifetime.Scoped);
                config.Where(t => t.AttributeExists<ExposeServicesAttribute>(false)).AsExposeService();
            });
        }
    }
}
