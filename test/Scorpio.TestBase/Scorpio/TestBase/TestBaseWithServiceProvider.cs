using System;

using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.TestBase
{
    public abstract class TestBaseWithServiceProvider
    {
        public abstract IServiceProvider ServiceProvider { get; }

        protected virtual T GetService<T>() => ServiceProvider.GetService<T>();

        protected virtual T GetRequiredService<T>() => ServiceProvider.GetRequiredService<T>();
    }
}
