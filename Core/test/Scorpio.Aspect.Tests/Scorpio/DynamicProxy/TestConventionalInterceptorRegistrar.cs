using Microsoft.Extensions.DependencyInjection;
using Scorpio.Conventional;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Scorpio.DynamicProxy
{
    class TestConventionalInterceptorRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.RegisterConventionalInterceptor(c =>
            {
                c.Where(t => t.IsAssignableTo<IInterceptorable>()).Intercept<TestInterceptor>();
            });
        }
    }
}
