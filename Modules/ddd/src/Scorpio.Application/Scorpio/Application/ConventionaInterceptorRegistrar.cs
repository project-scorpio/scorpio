using Scorpio.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Application
{
    class ConventionaInterceptorRegistrar : IConventionaInterceptorRegistrar
    {
        public void Register(IConventionaInterceptorContext context)
        {
            context.Add<Services.IApplicationService,Uow.UnitOfWorkInterceptor>();
        }
    }
}
