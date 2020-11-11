using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Conventional;

namespace Scorpio.BackgroundJobs
{
    internal class BackgroundJobsConventionalAction : ConventionalActionBase
    {
        public BackgroundJobsConventionalAction(IConventionalConfiguration configuration) : base(configuration)
        {
        }

        protected override void Action(IConventionalContext context)
        {
            var types = context.Types.ToArray();
            context.Services.PreConfigure<BackgroundJobOptions>(opts => types.ForEach(t => opts.AddJob(t)));
        }
    }
}
