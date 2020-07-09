using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Conventional;
using Scorpio.DependencyInjection;
using Scorpio.DependencyInjection.Conventional;
using Scorpio.EventBus.Conventional;
namespace Scorpio.EventBus
{
    internal class EventBusConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.RegisterConventionalDependencyInject(config =>
            {
                config.Where(t => t.IsAssignableTo<IEventHandler>() && t.IsAssignableTo<ISingletonDependency>()).AsSelf().Lifetime(ServiceLifetime.Singleton);
                config.Where(t => t.IsAssignableTo<IEventHandler>() && t.IsAssignableTo<ITransientDependency>()).AsSelf().Lifetime(ServiceLifetime.Transient);
                config.Where(t => t.IsAssignableTo<IEventHandler>() && t.IsAssignableTo<IScopedDependency>()).AsSelf().Lifetime(ServiceLifetime.Scoped);
            });
            context.RegisterEventHandler(c =>
           {
               c.Where(t => t.IsAssignableTo<IEventHandler>()).AutoActivation();
           });
        }
    }
}
