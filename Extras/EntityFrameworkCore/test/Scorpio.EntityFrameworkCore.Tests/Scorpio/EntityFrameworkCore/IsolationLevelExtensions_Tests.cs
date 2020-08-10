using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

using Shouldly;

using Xunit;

using SysIsolationLevel = System.Data.IsolationLevel;
namespace Scorpio.EntityFrameworkCore
{
    public class IsolationLevelExtensions_Tests
    {
        [Theory]
        [InlineData(IsolationLevel.Chaos, SysIsolationLevel.Chaos)]
        [InlineData(IsolationLevel.ReadCommitted, SysIsolationLevel.ReadCommitted)]
        [InlineData(IsolationLevel.ReadUncommitted, SysIsolationLevel.ReadUncommitted)]
        [InlineData(IsolationLevel.RepeatableRead, SysIsolationLevel.RepeatableRead)]
        [InlineData(IsolationLevel.Serializable, SysIsolationLevel.Serializable)]
        [InlineData(IsolationLevel.Snapshot, SysIsolationLevel.Snapshot)]
        [InlineData(IsolationLevel.Unspecified, SysIsolationLevel.Unspecified)]
        public void ToSystemDataIsolationLevel(IsolationLevel  isolationLevel, SysIsolationLevel  sysIsolationLevel)
        {
            isolationLevel.ToSystemDataIsolationLevel().ShouldBe(sysIsolationLevel);

        }
    }
}
