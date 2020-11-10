using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Quartz;

using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;

namespace Scorpio.Quartz
{
    internal class ConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context) => context.RegisterConventionalDependencyInject(config => config.Where(t => t.IsStandardType()).Where(t => t.IsAssignableTo<IJob>()).AsSelf().Lifetime(ServiceLifetime.Transient));
    }
}
