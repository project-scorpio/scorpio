using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio.Middleware.Pipeline
{
    public class PipelineBuilder_T_Tests
    {
        [Fact]
        public void Use_Build()
        {
            var descriptors = new ServiceCollection();
            var serviceProvider = descriptors.BuildServiceProvider();
            var builder = new TestPipelineBuilder(serviceProvider);
            builder.ApplicationServices.ShouldBe(serviceProvider);
            Should.Throw<ArgumentNullException>(() => builder.Use(null));
            Should.NotThrow(() => builder.Use(next => context => { context.PipelineInvoked = true;return next(context); }));
            var context = new TestPipelineContext();
            context.PipelineInvoked.ShouldBeFalse();
            Should.NotThrow(() => builder.Build()(context));
            context.PipelineInvoked.ShouldBeTrue();
        }
    }
}
