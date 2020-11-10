using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.Extensions;

using Scorpio.Threading;
using Scorpio.Timing;

using Shouldly;

using Xunit;

namespace Scorpio.BackgroundJobs
{
    public class BackgroundJobWorker_Tests
    {
        private static (IScorpioTimer, IBackgroundJobStore, BackgroundJobWorker, IBackgroundJobExecuter, BackgroundJobWorkerOptions) Getclasses()
        {
            var timer = Substitute.For<IScorpioTimer>();
            var objOptions = Substitute.For<IOptions<BackgroundJobOptions>>();
            objOptions.Configure().Value.Returns(new BackgroundJobOptions().Action(opt => opt.AddJob<FakeBackgroundJob>()));
            var workOptions = Substitute.For<IOptions<BackgroundJobWorkerOptions>>();
            var workOpt = new BackgroundJobWorkerOptions();
            workOptions.Configure().Value.Returns(workOpt);
            var factory = Substitute.For<IServiceScopeFactory>();
            var store = Substitute.For<IBackgroundJobStore>();
            factory.Configure().CreateScope().ServiceProvider.GetService(typeof(IBackgroundJobStore)).Returns(store);
            var executor = Substitute.For<IBackgroundJobExecuter>();
            factory.Configure().CreateScope().ServiceProvider.GetService(typeof(IBackgroundJobExecuter)).Returns(executor);
            var clock = Substitute.For<IClock>();
            clock.Configure().Now.Returns(DateTime.MinValue);
            factory.Configure().CreateScope().ServiceProvider.GetService(typeof(IClock)).Returns(clock);
            var serializer = Substitute.For<IBackgroundJobSerializer>();
            serializer.Configure().Deserialize(Arg.Any<string>(), Arg.Any<Type>()).Returns("");
            factory.Configure().CreateScope().ServiceProvider.GetService(typeof(IBackgroundJobSerializer)).Returns(serializer);
            var worker = new BackgroundJobWorker(timer, objOptions, workOptions, factory);
            return (timer, store, worker, executor, workOpt);
        }
        [Fact]
        public void Correct()
        {

            (var timer, var store, var worker, var executor, _) = Getclasses();
            var info = new BackgroundJobInfo
            {
                Id = Guid.NewGuid(),
                JobName = typeof(string).FullName,
                JobArgs = "",
                Priority = BackgroundJobPriority.Normal,
                CreationTime = DateTime.MinValue,
                NextTryTime = DateTime.MinValue
            };
            store.Configure().GetWaitingJobsAsync(Arg.Any<int>()).ReturnsForAnyArgs(new List<BackgroundJobInfo> { info }
                );
            Should.NotThrow(() => worker.StartAsync());
            timer.Elapsed += Raise.Event();
            info.TryCount.ShouldBe<short>(1);
            info.LastTryTime.ShouldBe(DateTime.MinValue);
            executor.ReceivedWithAnyArgs(1).ExecuteAsync(Arg.Any<JobExecutionContext>());
            store.ReceivedWithAnyArgs(1).DeleteAsync(Arg.Any<Guid>());
        }

        [Fact]
        public void EmptyJob()
        {
            (var timer, var store, var worker, var executor, _) = Getclasses();
            store.Configure().GetWaitingJobsAsync(Arg.Any<int>()).ReturnsForAnyArgs(new List<BackgroundJobInfo> { });
            Should.NotThrow(() => worker.StartAsync());
            timer.Elapsed += Raise.Event();
            executor.ReceivedWithAnyArgs(0).ExecuteAsync(Arg.Any<JobExecutionContext>());
            store.ReceivedWithAnyArgs(0).DeleteAsync(Arg.Any<Guid>());
        }

        [Fact]
        public void BackgroundJobExecutionException()
        {
            (var timer, var store, var worker, var executor, _) = Getclasses();
            var info = new BackgroundJobInfo
            {
                Id = Guid.NewGuid(),
                JobName = typeof(string).FullName,
                JobArgs = "",
                Priority = BackgroundJobPriority.Normal,
                CreationTime = DateTime.MinValue,
                NextTryTime = DateTime.MinValue
            };
            store.Configure().GetWaitingJobsAsync(Arg.Any<int>()).ReturnsForAnyArgs(new List<BackgroundJobInfo> { info }
                );
            executor.Configure().ExecuteAsync(Arg.Any<JobExecutionContext>()).ThrowsForAnyArgs<BackgroundJobExecutionException>();
            Should.NotThrow(() => worker.StartAsync());
            timer.Elapsed += Raise.Event();
            info.TryCount.ShouldBe<short>(1);
            info.LastTryTime.ShouldBe(DateTime.MinValue);
            executor.ReceivedWithAnyArgs(1).ExecuteAsync(Arg.Any<JobExecutionContext>());
            store.ReceivedWithAnyArgs(0).DeleteAsync(Arg.Any<Guid>());
            info.NextTryTime.ShouldBe(DateTime.MinValue.AddSeconds(60));
        }

        [Fact]
        public void BackgroundJobExecutionExceptionAndTimeOut()
        {
            (var timer, var store, var worker, var executor, var opt) = Getclasses();
            var info = new BackgroundJobInfo
            {
                Id = Guid.NewGuid(),
                JobName = typeof(string).FullName,
                JobArgs = "",
                Priority = BackgroundJobPriority.Normal,
                CreationTime = DateTime.MinValue,
                NextTryTime = DateTime.MinValue,
            };
            store.Configure().GetWaitingJobsAsync(Arg.Any<int>()).ReturnsForAnyArgs(new List<BackgroundJobInfo> { info }
                );
            executor.Configure().ExecuteAsync(Arg.Any<JobExecutionContext>()).ThrowsForAnyArgs<BackgroundJobExecutionException>();
            opt.DefaultTimeout = 0;
            Should.NotThrow(() => worker.StartAsync());
            timer.Elapsed += Raise.Event();
            info.TryCount.ShouldBe<short>(1);
            info.LastTryTime.ShouldBe(DateTime.MinValue);
            executor.ReceivedWithAnyArgs(1).ExecuteAsync(Arg.Any<JobExecutionContext>());
            store.ReceivedWithAnyArgs(0).DeleteAsync(Arg.Any<Guid>());
            info.IsAbandoned.ShouldBeTrue();
        }

        [Fact]
        public void Exception()
        {
            (var timer, var store, var worker, var executor, _) = Getclasses();
            var info = new BackgroundJobInfo
            {
                Id = Guid.NewGuid(),
                JobName = typeof(string).FullName,
                JobArgs = "",
                Priority = BackgroundJobPriority.Normal,
                CreationTime = DateTime.MinValue,
                NextTryTime = DateTime.MinValue
            };
            store.Configure().GetWaitingJobsAsync(Arg.Any<int>()).ReturnsForAnyArgs(new List<BackgroundJobInfo> { info }
                );
            executor.Configure().ExecuteAsync(Arg.Any<JobExecutionContext>()).ThrowsForAnyArgs<NotImplementedException>();
            Should.NotThrow(() => worker.StartAsync());
            timer.Elapsed += Raise.Event();
            info.TryCount.ShouldBe<short>(1);
            info.LastTryTime.ShouldBe(DateTime.MinValue);
            executor.ReceivedWithAnyArgs(1).ExecuteAsync(Arg.Any<JobExecutionContext>());
            store.ReceivedWithAnyArgs(0).DeleteAsync(Arg.Any<Guid>());
            info.IsAbandoned.ShouldBeTrue();
        }
    }
}
