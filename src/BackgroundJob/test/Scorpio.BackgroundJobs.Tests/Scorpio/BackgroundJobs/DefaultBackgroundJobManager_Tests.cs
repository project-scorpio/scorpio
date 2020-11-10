using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using NSubstitute;
using NSubstitute.Extensions;

using Scorpio.Timing;

using Shouldly;

using Xunit;

namespace Scorpio.BackgroundJobs
{
    public class DefaultBackgroundJobManager_Tests
    {
        [Fact]
        public void EnqueueAsync()
        {
            var store = Substitute.For<IBackgroundJobStore>();
            var json = Substitute.For<IBackgroundJobSerializer>();
            var clock = Substitute.For<IClock>();
            clock.Configure().Now.Returns(DateTime.MinValue);
            json.Configure().Serialize(Arg.Any<string>()).ReturnsForAnyArgs("");
            store.Configure().WhenForAnyArgs(s => s.InsertAsync(Arg.Any<BackgroundJobInfo>())).Do(c => c.Arg<BackgroundJobInfo>().Action(i =>
            {
                i.JobArgs.ShouldBe("");
                i.NextTryTime.ShouldBe(DateTime.MinValue);
                i.CreationTime.ShouldBe(DateTime.MinValue);
                i.JobName.ShouldBe(typeof(string).FullName);
                i.Priority.ShouldBe(BackgroundJobPriority.Normal);
            }));
            var manager = new DefaultBackgroundJobManager(clock, json, store);
            Should.NotThrow(() => manager.EnqueueAsync("Test"));
            store.ReceivedWithAnyArgs(1).InsertAsync(Arg.Any<BackgroundJobInfo>());
        }

          [Fact]
        public void EnqueueAsyncWithDelay()
        {
            var store = Substitute.For<IBackgroundJobStore>();
            var json = Substitute.For<IBackgroundJobSerializer>();
            var clock = Substitute.For<IClock>();
            clock.Configure().Now.Returns(DateTime.MinValue);
            json.Configure().Serialize(Arg.Any<string>()).ReturnsForAnyArgs("");
            store.Configure().WhenForAnyArgs(s => s.InsertAsync(Arg.Any<BackgroundJobInfo>())).Do(c => c.Arg<BackgroundJobInfo>().Action(i =>
            {
                i.JobArgs.ShouldBe("");
                i.NextTryTime.ShouldBe(DateTime.MinValue.AddSeconds(1));
                i.CreationTime.ShouldBe(DateTime.MinValue);
                i.JobName.ShouldBe(typeof(string).FullName);
                i.Priority.ShouldBe(BackgroundJobPriority.Normal);
            }));
            var manager = new DefaultBackgroundJobManager(clock, json, store);
            Should.NotThrow(() => manager.EnqueueAsync("Test",delay:TimeSpan.FromSeconds(1)));
            store.ReceivedWithAnyArgs(1).InsertAsync(Arg.Any<BackgroundJobInfo>());
        }

          [Fact]
        public void EnqueueAsyncWithPriority()
        {
            var store = Substitute.For<IBackgroundJobStore>();
            var json = Substitute.For<IBackgroundJobSerializer>();
            var clock = Substitute.For<IClock>();
            clock.Configure().Now.Returns(DateTime.MinValue);
            json.Configure().Serialize(Arg.Any<string>()).ReturnsForAnyArgs("");
            store.Configure().WhenForAnyArgs(s => s.InsertAsync(Arg.Any<BackgroundJobInfo>())).Do(c => c.Arg<BackgroundJobInfo>().Action(i =>
            {
                i.JobArgs.ShouldBe("");
                i.NextTryTime.ShouldBe(DateTime.MinValue);
                i.CreationTime.ShouldBe(DateTime.MinValue);
                i.JobName.ShouldBe(typeof(string).FullName);
                i.Priority.ShouldBe(BackgroundJobPriority.High);
            }));
            var manager = new DefaultBackgroundJobManager(clock, json, store);
            Should.NotThrow(() => manager.EnqueueAsync("Test",priority:BackgroundJobPriority.High));
            store.ReceivedWithAnyArgs(1).InsertAsync(Arg.Any<BackgroundJobInfo>());
        }
    }
}
