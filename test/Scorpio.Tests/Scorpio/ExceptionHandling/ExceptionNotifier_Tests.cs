using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.Extensions;

using Scorpio.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio.ExceptionHandling
{
    public class ExceptionNotifier_Tests
    {
        [Fact]
        public void NotifyAsync()
        {
            Should.Throw<ArgumentNullException>(() => new ExceptionNotifier(null));
            var factory = Substitute.For<IHybridServiceScopeFactory>();
            var scope = Substitute.For<IServiceScope>();
            var subscriber = Substitute.For<IExceptionSubscriber>();
            var provider = Substitute.For<IServiceProvider>();
            scope.Configure().ServiceProvider.Returns(provider);
            provider.Configure().GetService(typeof(IEnumerable<IExceptionSubscriber>)).Returns(new List<IExceptionSubscriber> { subscriber });
            factory.Configure().CreateScope().Returns(scope);
            var notifier = Should.NotThrow(() => new ExceptionNotifier(factory));
            var context = new ExceptionNotificationContext(new ScorpioException());
            Should.NotThrow(() => notifier.NotifyAsync(context));
            subscriber.Received(1).HandleAsync(context);
        }

        [Fact]
        public void NotifyAsync_Exception()
        {
            var factory = Substitute.For<IHybridServiceScopeFactory>();
            var scope = Substitute.For<IServiceScope>();
            var subscriber = Substitute.For<IExceptionSubscriber>();
            subscriber.Configure().HandleAsync(Arg.Any<ExceptionNotificationContext>()).ThrowsForAnyArgs<ScorpioException>();
            var provider = Substitute.For<IServiceProvider>();
            scope.Configure().ServiceProvider.Returns(provider);
            provider.Configure().GetService(typeof(IEnumerable<IExceptionSubscriber>)).Returns(new List<IExceptionSubscriber> { subscriber });
            factory.Configure().CreateScope().Returns(scope);
            var notifier = Should.NotThrow(() => new ExceptionNotifier(factory));
            var logger=Substitute.For<ILogger<ExceptionNotifier>>();
            notifier.Logger=logger;
            var context = new ExceptionNotificationContext(new ScorpioException());
            Should.NotThrow(() => notifier.NotifyAsync(context));
            subscriber.Received(1).HandleAsync(context);
        }
    }
}
