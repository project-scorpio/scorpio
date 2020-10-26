
using Scorpio.Conventional;

using Shouldly;

using Xunit;

namespace Scorpio.EventBus.Conventional
{
    public class RegisterAssemblyEventHandlerContextExtensions_Tests
    {
        [Fact]
        public void ActivationByType()
        {
            var context = new ConventionalContext(null, null);
            context.ActivationByType(EventHandlerActivationType.Singleton);
            context.Get<IEventHandlerActivationTypeSelector>("HandlerActivationTypeSelector").ShouldBeOfType<ActivationTypeSelector>().ShouldNotBeNull();
            context.Get<IEventHandlerActivationTypeSelector>("HandlerActivationTypeSelector").Select(default).ShouldBe(EventHandlerActivationType.Singleton);
        }
        [Fact]
        public void AutoActivation()
        {
            var context = new ConventionalContext(null, null);
            context.AutoActivation();
            context.Get<IEventHandlerActivationTypeSelector>("HandlerActivationTypeSelector").ShouldBeOfType<ExposeActivationTypeSelector>().ShouldNotBeNull();
            context.Get<IEventHandlerActivationTypeSelector>("HandlerActivationTypeSelector").Select(typeof(string)).ShouldBe(EventHandlerActivationType.Transient);
        }
    }
}
