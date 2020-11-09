using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Scorpio.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Xunit;
using NSubstitute.ExceptionExtensions;
using System.Reflection;
using NSubstitute.Extensions;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// 
    /// </summary>
    public class BackgroundJobExecuter_Tests : BackgroundJobsAbstractionsTestBase
    {
        protected override Bootstrapper CreateBootstrapper(IServiceCollection services)
        {
            services.AddSingleton(Substitute.ForPartsOf<BackgroundJob<string>>());
            services.AddSingleton(Substitute.ForPartsOf<AsyncBackgroundJob<string>>());
            return base.CreateBootstrapper(services);
        }

        [Fact]
        public void ExecuteAsync()
        {
            var executor = ServiceProvider.GetRequiredService<IBackgroundJobExecuter>();
            Should.Throw<ScorpioException>(() => executor.ExecuteAsync(new JobExecutionContext(ServiceProvider, typeof(BackgroundJob<int>), 1)));
            Should.Throw<ScorpioException>(() => executor.ExecuteAsync(new JobExecutionContext(ServiceProvider, typeof(IServiceProvider), 1)));
            Should.NotThrow(async () =>
            {
                await executor.ExecuteAsync(new JobExecutionContext(ServiceProvider, typeof(BackgroundJob<string>), "Test"));
                ServiceProvider.GetRequiredService<BackgroundJob<string>>().ReceivedWithAnyArgs(1).Execute(Arg.Any<string>());
            });
            Should.NotThrow(async () =>
            {
                await executor.ExecuteAsync(new JobExecutionContext(ServiceProvider, typeof(AsyncBackgroundJob<string>), "Test"));
                await ServiceProvider.GetRequiredService<AsyncBackgroundJob<string>>().ReceivedWithAnyArgs(1).ExecuteAsync(Arg.Any<string>());
            });
            Should.Throw<BackgroundJobExecutionException>(async () =>
            {
                var job = ServiceProvider.GetRequiredService<BackgroundJob<string>>();
                job.WhenForAnyArgs(e => e.Execute(Arg.Any<string>())).Do(x => throw new NotImplementedException());
                await executor.ExecuteAsync(new JobExecutionContext(ServiceProvider, typeof(BackgroundJob<string>), "Test"));
            }).InnerException.ShouldBeOfType<TargetInvocationException>().InnerException.ShouldBeOfType<NotImplementedException>();

        }
    }
}