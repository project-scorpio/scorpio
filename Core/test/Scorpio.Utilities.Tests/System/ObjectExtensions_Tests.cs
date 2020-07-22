using System;
using System.Collections.Generic;
using System.Text;

using Scorpio.Middleware.Pipeline;

using Shouldly;

using Xunit;

namespace System
{
    public class ObjectExtensions_Tests
    {
        [Fact]
        public void As()
        {
            object act = new TestPipelineBuilder(null);
            act.As<PipelineBuilder<TestPipelineContext>>().ShouldNotBeNull();
            act.As<IPipelineBuilder<TestPipelineContext>>().ShouldNotBeNull();
            act.As<String>().ShouldBeNull();
        }

        [Fact]
        public void To()
        {
            var act = 3.14159;
            act.To<int>().ShouldBe(3);
            act.To<bool>().ShouldBeTrue();
        }

        [Fact]
        public void Action()
        {
            var act = "test";
            var exp = "exp";
            act.Action(a => exp = a).ShouldBe(act);
            exp.ShouldBe(act);
        }
    }
}
