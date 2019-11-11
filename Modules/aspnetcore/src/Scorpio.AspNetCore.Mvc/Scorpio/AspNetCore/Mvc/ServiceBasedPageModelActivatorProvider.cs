using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.Mvc
{
    /// <summary>
    /// <see cref="IPageActivatorProvider"/> that uses type activation to create Razor Page instances.
    /// </summary>
    internal class ServiceBasedPageModelActivatorProvider : IPageModelActivatorProvider
    {
        public Func<PageContext, object> CreateActivator(CompiledPageActionDescriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            var modelType = descriptor.ModelTypeInfo?.AsType();

            return context =>
            {
                return context.HttpContext.RequestServices.GetRequiredService(modelType);
            };
        }

        public Action<PageContext, object> CreateReleaser(CompiledPageActionDescriptor descriptor)
        {
            return null;
        }
    }
}
