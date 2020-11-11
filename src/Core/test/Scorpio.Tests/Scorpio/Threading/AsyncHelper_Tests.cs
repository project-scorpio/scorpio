using System;
using System.Threading;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace Scorpio.Threading
{
    public class AsyncHelper_Tests
    {
        [Fact]
        public void IsAsync()
        {
            ((Func<int, int>)IntFunction).IsAsync().ShouldBeFalse();
            ((Func<int, int>)IntFunction).Method.IsAsync().ShouldBeFalse();
            ((Func<Task>)TaskFunctionAsync).IsAsync().ShouldBeTrue();
            ((Func<Task>)TaskFunctionAsync).Method.IsAsync().ShouldBeTrue();
            ((Func<int, Task<int>>)TaskFunctionAsync).IsAsync().ShouldBeTrue();
            ((Func<int, Task<int>>)TaskFunctionAsync).Method.IsAsync().ShouldBeTrue();
            ((Func<ValueTask>)ValueTaskFunctionAsync).IsAsync().ShouldBeTrue();
            ((Func<ValueTask>)ValueTaskFunctionAsync).Method.IsAsync().ShouldBeTrue();
            ((Func<int, ValueTask<int>>)ValueTaskFunctionAsync).IsAsync().ShouldBeTrue();
            ((Func<int, ValueTask<int>>)ValueTaskFunctionAsync).Method.IsAsync().ShouldBeTrue();
        }

        [Fact]
        public void IsTask()
        {
            typeof(Task).IsTask().ShouldBeTrue();
            typeof(Task<>).IsTask().ShouldBeTrue();
            typeof(ValueTask).IsTask().ShouldBeTrue();
            typeof(ValueTask<>).IsTask().ShouldBeTrue();
            typeof(object).IsTask().ShouldBeFalse();
        }

        [Fact]
        public void UnwrapTask()
        {
            typeof(Task).UnwrapTask().ShouldBe(typeof(void));
            typeof(Task<int>).UnwrapTask().ShouldBe(typeof(int));
            typeof(ValueTask).UnwrapTask().ShouldBe(typeof(void));
            typeof(ValueTask<int>).UnwrapTask().ShouldBe(typeof(int));
            typeof(object).UnwrapTask().ShouldBe(typeof(object));
        }

        [Fact]
        public void RunSync()
        {
            var threadLocal = new ThreadLocal<int>
            {
                Value = 0
            };
            ((Func<Task>)(async () => { threadLocal.Value = 1; await Task.CompletedTask; })).RunSync();
            threadLocal.Value.ShouldBe(1);
            ((Func<Task<int>>)(() => { threadLocal.Value = 2; return Task.FromResult(threadLocal.Value); })).RunSync().ShouldBe(2);
            threadLocal.Value.ShouldBe(2);
        }

        public int IntFunction(int value) => value;

        internal async Task TaskFunctionAsync() => await Task.CompletedTask;
        public Task<int> TaskFunctionAsync(int value) => Task.FromResult(value);

        public async ValueTask ValueTaskFunctionAsync() => await Task.CompletedTask;
        public async ValueTask<int> ValueTaskFunctionAsync(int value) => await Task.FromResult(value);

    }
}
