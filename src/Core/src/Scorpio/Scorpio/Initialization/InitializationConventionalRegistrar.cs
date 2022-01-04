using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using Scorpio.Conventional;

namespace Scorpio.Initialization
{
    internal class InitializationConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.DoConventionalAction<InitializationConventionalAction>(config =>
            {
                config.Where(t => t.IsStandardType()).Where(t => t.IsAssignableTo<IInitializable>());
            });
        }
    }
}
