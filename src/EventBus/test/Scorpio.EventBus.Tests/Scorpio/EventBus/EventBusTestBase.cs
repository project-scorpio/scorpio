﻿namespace Scorpio.EventBus
{
    public abstract class EventBusTestBase : TestBase.IntegratedTest<EventBusTestModule>
    {
        protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options)
        {
            base.SetBootstrapperCreationOptions(options);
            options.UseAspectCore();
        }
    }
}
