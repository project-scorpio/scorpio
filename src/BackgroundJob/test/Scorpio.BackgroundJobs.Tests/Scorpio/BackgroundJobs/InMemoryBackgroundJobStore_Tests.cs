using System;
using System.Collections.Generic;
using System.Text;

using NSubstitute;
using NSubstitute.Extensions;

using Scorpio.Timing;

using Shouldly;

using Xunit;

namespace Scorpio.BackgroundJobs
{
    public class InMemoryBackgroundJobStore_Tests
    {
        [Fact]
        public void Test()
        {
            var clock = Substitute.For<IClock>();
            clock.Configure().Now.Returns(DateTime.MinValue);
            var store = new InMemoryBackgroundJobStore(clock);
            store.InsertAsync(new BackgroundJobInfo());
            Should.NotThrow(() => store.GetWaitingJobsAsync(10)).ShouldHaveSingleItem();
            var job = Should.NotThrow(() => store.FindAsync(Guid.Empty)).Action(j => j.ShouldNotBeNull());
            Should.NotThrow(() => store.UpdateAsync(job));
            Should.NotThrow(() => store.FindAsync(Guid.Empty)).ShouldNotBeNull();
            job.IsAbandoned = true;
            Should.NotThrow(() => store.UpdateAsync(job));
            Should.NotThrow(() => store.FindAsync(Guid.Empty)).ShouldBeNull();

        }
    }
}
