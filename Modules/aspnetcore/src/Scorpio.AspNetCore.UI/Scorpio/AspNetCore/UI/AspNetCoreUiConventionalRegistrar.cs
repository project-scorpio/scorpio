using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;

namespace Scorpio.AspNetCore.UI
{
    internal class AspNetCoreUiConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.RegisterConventionalDependencyInject(config =>
           {
               config.Where(t => t.IsStandardType() && t.IsAssignableTo<TagHelpers.ITagHelperService>()).AsSelf().Lifetime(ServiceLifetime.Transient);
           });
        }
    }
}
