using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.DynamicProxy
{
    class TestConventionalInterceptorRegistrar : IConventionaInterceptorRegistrar
    {
        public void Register(IConventionaInterceptorContext context)
        {
            context.Add<IInterceptorable, TestInterceptor>();
        }
    }
}
