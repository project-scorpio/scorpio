using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Scorpio.Quartz
{
    class ConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.RegisterConventionalDependencyInject(config =>
            {
                config.Where(t => t.IsStandardType()).Where(t => t.IsAssignableTo<IJob>()).AsSelf().Lifetime(ServiceLifetime.Transient);
            });
        }
    }
}
