using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Scorpio.AspNetCore.UI
{
    internal class AspNetCoreUiConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.Services.RegisterAssembly(context.Assembly, config =>
            {
                config.Where(t => t.IsAssignableTo<TagHelpers.ITagHelperService>()).AsSelf().Lifetime(ServiceLifetime.Transient);
            });
        }
    }
}
