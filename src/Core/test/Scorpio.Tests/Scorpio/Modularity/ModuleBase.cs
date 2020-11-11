namespace Scorpio.Modularity
{
    public class ModuleBase : ScorpioModule
    {
        public bool PreConfigureServicesCalled { get; set; }
        public bool ConfigureServicesCalled { get; set; }
        public bool PostConfigureServicesCalled { get; set; }

        public bool PreInitializeCalled { get; set; }

        public bool InitializeCalled { get; set; }

        public bool PostInitializeCalled { get; set; }

        public bool ShutdownCalled { get; set; }
        public override void ConfigureServices(ConfigureServicesContext context) => ConfigureServicesCalled = true;

        public override void PreConfigureServices(ConfigureServicesContext context) => PreConfigureServicesCalled = true;

        public override void PostConfigureServices(ConfigureServicesContext context) => PostConfigureServicesCalled = true;

        public override void PreInitialize(ApplicationInitializationContext context) => PreInitializeCalled = true;

        public override void Initialize(ApplicationInitializationContext context) => InitializeCalled = true;

        public override void PostInitialize(ApplicationInitializationContext context) => PostInitializeCalled = true;

        public override void Shutdown(ApplicationShutdownContext context) => ShutdownCalled = true;
    }
}
