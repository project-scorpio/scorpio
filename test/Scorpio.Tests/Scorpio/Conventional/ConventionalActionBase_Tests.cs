using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio.Conventional
{
    public class ConventionalActionBase_Tests
    {


        private class FakeConventionalAction : ConventionalActionBase
        {
            public int ActionTime { get; set; } = 0;
            public FakeConventionalAction(IConventionalConfiguration configuration) : base(configuration)
            {
            }

            protected override void Action(IConventionalContext context) => ActionTime += 1;
        }

        [Fact]
        public void Action()
        {
            var config = new ConventionalConfiguration<FakeConventionalAction>(null, null);
            config.CreateContext();
            var action = new FakeConventionalAction(config);
            action.ActionTime.ShouldBe(0);
            Should.NotThrow(() => action.Action());
            action.ActionTime.ShouldBe(1);
            action.ActionTime= 0;
            config.CreateContext();
            action.ActionTime.ShouldBe(0);
            Should.NotThrow(() => action.Action());
            action.ActionTime.ShouldBe(2);

        }
    }
}
