using System.Reflection;

using Scorpio.Conventional;

namespace Scorpio.BackgroundJobs
{
    internal class BackgroundJobsConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context) => context.DoConventionalAction<BackgroundJobsConventionalAction>(c => c.Where(t => t.IsStandardType() && (t.IsAssignableToGenericType(typeof(IBackgroundJob<>)) || t.IsAssignableToGenericType(typeof(IAsyncBackgroundJob<>)))));
    }
}
