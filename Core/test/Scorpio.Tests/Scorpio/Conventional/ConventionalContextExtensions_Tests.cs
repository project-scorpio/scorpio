using System;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.DependencyInjection.Conventional;

using Shouldly;

using Xunit;

namespace Scorpio.Conventional
{
    public class ConventionalContextExtensions_Tests
    {
        private static IConventionalContext<ConventionalDependencyAction> GetContext()
        {
            var services = new ServiceCollection();
            var config = new ConventionalConfiguration<ConventionalDependencyAction>(services, new Type[] { });
            var context = config.GetContext().As<IConventionalContext<ConventionalDependencyAction>>();
            return context;
        }

        [Fact]
        public void SetAndGet()
        {
            var context = GetContext();
            context.Get<string>("key").ShouldBeNull();
            context.Set("key", "value");
            context.Get<string>("key").ShouldBe("value");
        }


        [Fact]
        public void GetOrAdd()
        {
            var context = GetContext();
            context.Get<string>("key").ShouldBeNull();
            context.GetOrAdd("key", "value").ShouldBe("value");
            context.Get<string>("key").ShouldBe("value");
        }
        [Fact]
        public void GetOrDefault()
        {
            var context = GetContext();
            context.Get<string>("key").ShouldBeNull();
            context.GetOrDefault("key", "value").ShouldBe("value");
            context.Get<string>("key").ShouldBeNull();
        }
    }
}
