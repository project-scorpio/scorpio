﻿using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;

namespace Scorpio.AspNetCore.Mvc
{
    internal class AspNetCoreConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.RegisterConventionalDependencyInject(config =>
           {
               config.Where(t => t.IsAssignableTo<Controller>() || t.AttributeExists<ControllerAttribute>()).AsSelf().Lifetime(ServiceLifetime.Transient);
               config.Where(t => t.IsAssignableTo<PageModel>() || t.AttributeExists<PageModelAttribute>()).AsSelf().Lifetime(ServiceLifetime.Transient);
               config.Where(t => t.IsAssignableTo<ViewComponent>() || t.AttributeExists<ViewComponentAttribute>()).AsSelf().Lifetime(ServiceLifetime.Transient);
           });
        }
    }
}
