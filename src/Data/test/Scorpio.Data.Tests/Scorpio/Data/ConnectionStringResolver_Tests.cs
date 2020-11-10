using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity;

using Shouldly;

using Xunit;

namespace Scorpio.Data
{
    public class ConnectionStringResolver_Tests : TestBase.IntegratedTest<ConnectionStringResolverModule>
    {
        public IConnectionStringResolver Resolver { get; set; }
        public ConnectionStringResolver_Tests() => Resolver = ServiceProvider.GetRequiredService<IConnectionStringResolver>();

        [Fact]
        public void ResolveDefault() => Resolver.Resolve().ShouldBe("DefaultConnection");

        [Fact]
        public void ResolveName() => Resolver.Resolve("Connection1").ShouldBe("ConnectionString1");
        [Fact]
        public void ResolveDefaultObject() => Resolver.Resolve<DefaultResolveable>().ShouldBe("DefaultConnection");

        [Fact]
        public void ResolveNameObject() => Resolver.Resolve<NamedResolveable>().ShouldBe("ConnectionString1");
    }

    [DependsOn(typeof(DataModule))]
    public class ConnectionStringResolverModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.Configure<DbConnectionOptions>(o =>
            {
                o.ConnectionStrings.Default = "DefaultConnection";
                o.ConnectionStrings.Add("Connection1", "ConnectionString1");
            });
        }
    }

    [ConnectionStringName]
    public class DefaultResolveable
    {

    }

    [ConnectionStringName("Connection1")]
    public class NamedResolveable
    {

    }

}
