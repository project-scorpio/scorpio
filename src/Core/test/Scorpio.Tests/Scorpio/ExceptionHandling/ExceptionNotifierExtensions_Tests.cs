using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NSubstitute;
using NSubstitute.Extensions;

using Scorpio.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio.ExceptionHandling
{
    public class ExceptionNotifierExtensions_Tests
    {
        [Fact]
        public void NotifyAsync()
        {
            Should.Throw<ArgumentNullException>(() => new ExceptionNotifier(null));
            var factory = Substitute.For<IHybridServiceScopeFactory>();
            var scope = Substitute.For<IServiceScope>();
            var subscriber = Substitute.For<IExceptionSubscriber>();
            subscriber.Configure().HandleAsync(Arg.Any<ExceptionNotificationContext>()).Returns(Task.CompletedTask).AndDoes(c =>
            {
                var context = c.Arg<ExceptionNotificationContext>();
                context.ShouldNotBeNull();
                context.Exception.ShouldBeOfType<ScorpioException>();
                context.LogLevel.ShouldBe(LogLevel.Error);
                context.Handled.ShouldBeTrue();
            });
            var provider = Substitute.For<IServiceProvider>();
            scope.Configure().ServiceProvider.Returns(provider);
            provider.Configure().GetService(typeof(IEnumerable<IExceptionSubscriber>)).Returns(new List<IExceptionSubscriber> { subscriber });
            factory.Configure().CreateScope().Returns(scope);
            var notifier = Should.NotThrow(() => new ExceptionNotifier(factory));
            var ex = new ScorpioException();
            Should.NotThrow(() => notifier.NotifyAsync(ex));
            subscriber.ReceivedWithAnyArgs(1).HandleAsync(Arg.Any<ExceptionNotificationContext>());
        }


    }
}
