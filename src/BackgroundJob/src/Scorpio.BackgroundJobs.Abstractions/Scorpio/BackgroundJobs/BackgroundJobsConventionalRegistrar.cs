using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using Scorpio.Conventional;

namespace Scorpio.BackgroundJobs
{
    class BackgroundJobsConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.DoConventionalAction<BackgroundJobsConventionalAction>(c =>
            {
                c.Where(t=>t.IsStandardType() && (t.IsAssignableToGenericType(typeof(IBackgroundJob<>))|| t.IsAssignableToGenericType(typeof(IAsyncBackgroundJob<>))));
            });
        }
    }
}
