using System;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public class DisposeAction_Tests
    {
        [Fact]
        public void Dispose()
        {
            var number = 0;
            var obj = new DisposeAction(() => number += 1);
            obj.Dispose();
            number.ShouldBe(1);
            obj.Dispose();
            number.ShouldBe(1);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class AsyncDisposeAction_Tests
    {
        [Fact]
        public async ValueTask DisposeAsync()
        {
            var number = 0;
            var obj = new AsyncDisposeAction(() =>
            {
                number += 1;
                return new ValueTask();
            });
            await obj.DisposeAsync();
            number.ShouldBe(1);
            await obj.DisposeAsync();
            number.ShouldBe(1);
        }


    }
}
