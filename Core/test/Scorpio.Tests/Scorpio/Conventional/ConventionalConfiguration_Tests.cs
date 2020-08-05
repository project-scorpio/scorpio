using System;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.DependencyInjection.Conventional;

using Shouldly;

using Xunit;

namespace Scorpio.Conventional
{
    public class ConventionalConfiguration_Tests
    {
        [Fact]
        public void GetContext()
        {
            var services = new ServiceCollection();
            var config = new ConventionalConfiguration<ConventionalDependencyAction>(services, new Type[] { });
            config.GetContext().ShouldBeOfType<ConventionalContext<ConventionalDependencyAction>>().ShouldNotBeNull();
        }
    }
}
