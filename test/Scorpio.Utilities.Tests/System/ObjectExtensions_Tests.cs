
using System.Threading.Tasks;

using Moq;

using NSubstitute;
using NSubstitute.ClearExtensions;
using NSubstitute.ReceivedExtensions;

using Scorpio.Middleware.Pipeline;

using Shouldly;

using Xunit;

using static System.Collections.Specialized.BitVector32;

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
            act.As<string>().ShouldBeNull();
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
            var action = Substitute.For<Action<string>>();
            "test".Action(action);
            action.Received(1);
            action.ClearSubstitute();
            "test".Action(false, action);
            action.Received(0);
            "test".Action(true, action);
            action.Received(1);
            action.ClearSubstitute();
            "test".Action(s => s != "test", action);
            action.Received(0);
            "test".Action(s => s == "test", action);
            action.Received(1);
        }
        [Fact]
        public async Task ActionAsync()
        {
            var action = Substitute.For<Func<string, ValueTask>>();
            await "test".Action(action);
            action.Received(1);
            action.ClearSubstitute();
            await "test".Action(false, action);
            action.Received(0);
            await "test".Action(true, action);
            action.Received(1);
            action.ClearSubstitute();
            await "test".Action(s => s != "test", action);
            action.Received(0);
            await "test".Action(s => s == "test", action);
            action.Received(1);
        }

        [Fact]
        public void ActionTrueFalse()
        {
            var actionTrue = Substitute.For<Action<string>>();
            var actionFalse = Substitute.For<Action<string>>();
            "test".Action(true, actionTrue, actionFalse);
            actionTrue.Received(1);
            actionFalse.Received(0);
            actionTrue.ClearSubstitute();
            actionFalse.ClearSubstitute();
            "test".Action(false, actionTrue, actionFalse);
            actionTrue.Received(0);
            actionFalse.Received(1);
            actionTrue.ClearSubstitute();
            actionFalse.ClearSubstitute();
            "test".Action(s => s == "test", actionTrue, actionFalse);
            actionTrue.Received(1);
            actionFalse.Received(0);
            actionTrue.ClearSubstitute();
            actionFalse.ClearSubstitute();
            "test".Action(s => s != "test", actionTrue, actionFalse);
            actionTrue.Received(0);
            actionFalse.Received(1);

        }

        [Fact]
        public async Task ActionTrueFalseAsync()
        {
            var actionTrue = Substitute.For<Func<string, ValueTask>>();
            var actionFalse = Substitute.For<Func<string, ValueTask>>();
            await "test".Action(true, actionTrue, actionFalse);
            actionTrue.Received(1);
            actionFalse.Received(0);
            actionTrue.ClearSubstitute();
            actionFalse.ClearSubstitute();
            await "test".Action(false, actionTrue, actionFalse);
            actionTrue.Received(0);
            actionFalse.Received(1);
            actionTrue.ClearSubstitute();
            actionFalse.ClearSubstitute();
            await "test".Action(s => s == "test", actionTrue, actionFalse);
            actionTrue.Received(1);
            actionFalse.Received(0);
            actionTrue.ClearSubstitute();
            actionFalse.ClearSubstitute();
            await "test".Action(s => s != "test", actionTrue, actionFalse);
            actionTrue.Received(0);
            actionFalse.Received(1);

        }

        [Fact]
        public async Task SafelyDisposeAsync()
        {
            var obj = Substitute.For<IAsyncDisposable>();
            await obj.SafelyDisposeAsync();
            await obj.Received(1).DisposeAsync();
        }

        [Fact]
        public void SafelyDispose()
        {
            var obj = Substitute.For<IDisposable>();
            obj.SafelyDispose();
            obj.Received(1).Dispose();
        }

    }
}
