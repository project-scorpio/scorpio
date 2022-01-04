using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Conventional;

namespace Scorpio.Initialization
{
    internal class InitializationConventionalAction : ConventionalActionBase
    {
        public InitializationConventionalAction(IConventionalConfiguration configuration) : base(configuration)
        {
        }

        protected override void Action(IConventionalContext context) => context.Types.ForEach(t => context.Services.Configure<InitializationOptions>(opts => opts.AddInitializable(t, InitializationOrderAttribute.GetOrder(t))));
    }
}
