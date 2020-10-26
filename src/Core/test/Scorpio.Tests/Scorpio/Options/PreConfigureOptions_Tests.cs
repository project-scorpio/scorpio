
using NSubstitute;

using Scorpio.Modularity;

using Shouldly;

using Xunit;

using static Microsoft.Extensions.Options.Options;

namespace Scorpio.Options
{
    public class PreConfigureOptions_Tests
    {
        [Fact]
        public void PreConfigure_T0()
        {
            var options = Substitute.For<ExtensibleOptions>();
            var namedConfigure = new PreConfigureOptions<ExtensibleOptions>("Named", o => o.SetOption("named", "value"));
            var Configure = new PreConfigureOptions<ExtensibleOptions>(DefaultName, o => o.SetOption("default", "value"));
            namedConfigure.PreConfigure(options);
            Configure.PreConfigure(options);
            options.GetOption<string>("named").ShouldBeNull();
            options.GetOption<string>("default").ShouldBe("value");
            var options2 = Substitute.For<ExtensibleOptions>();
            namedConfigure.PreConfigure("Named", options2);
            Configure.PreConfigure("Named", options2);
            options2.GetOption<string>("named").ShouldBe("value");
            options2.GetOption<string>("default").ShouldBeNull();
        }

        [Fact]
        public void PreConfigure_T1()
        {
            var options = Substitute.For<ExtensibleOptions>();
            var namedConfigure = new PreConfigureOptions<ExtensibleOptions, object>("Named", default, (o, d) => o.SetOption("named", "value"));
            var Configure = new PreConfigureOptions<ExtensibleOptions, object>(DefaultName, default, (o, d) => o.SetOption("default", "value"));
            namedConfigure.PreConfigure(options);
            Configure.PreConfigure(options);
            options.GetOption<string>("named").ShouldBeNull();
            options.GetOption<string>("default").ShouldBe("value");
            var options2 = Substitute.For<ExtensibleOptions>();
            namedConfigure.PreConfigure("Named", options2);
            Configure.PreConfigure("Named", options2);
            options2.GetOption<string>("named").ShouldBe("value");
            options2.GetOption<string>("default").ShouldBeNull();
        }

        [Fact]
        public void PreConfigure_T2()
        {
            var options = Substitute.For<ExtensibleOptions>();
            var namedConfigure = new PreConfigureOptions<ExtensibleOptions, object, object>("Named", default, default, (o, d1, d2) => o.SetOption("named", "value"));
            var Configure = new PreConfigureOptions<ExtensibleOptions, object, object>(DefaultName, default, default, (o, d1, d2) => o.SetOption("default", "value"));
            namedConfigure.PreConfigure(options);
            Configure.PreConfigure(options);
            options.GetOption<string>("named").ShouldBeNull();
            options.GetOption<string>("default").ShouldBe("value");
            var options2 = Substitute.For<ExtensibleOptions>();
            namedConfigure.PreConfigure("Named", options2);
            Configure.PreConfigure("Named", options2);
            options2.GetOption<string>("named").ShouldBe("value");
            options2.GetOption<string>("default").ShouldBeNull();
        }

        [Fact]
        public void PreConfigure_T3()
        {
            var options = Substitute.For<ExtensibleOptions>();
            var namedConfigure = new PreConfigureOptions<ExtensibleOptions, object, object, object>("Named", default, default, default, (o, d1, d2, d3) => o.SetOption("named", "value"));
            var Configure = new PreConfigureOptions<ExtensibleOptions, object, object, object>(DefaultName, default, default, default, (o, d1, d2, d3) => o.SetOption("default", "value"));
            namedConfigure.PreConfigure(options);
            Configure.PreConfigure(options);
            options.GetOption<string>("named").ShouldBeNull();
            options.GetOption<string>("default").ShouldBe("value");
            var options2 = Substitute.For<ExtensibleOptions>();
            namedConfigure.PreConfigure("Named", options2);
            Configure.PreConfigure("Named", options2);
            options2.GetOption<string>("named").ShouldBe("value");
            options2.GetOption<string>("default").ShouldBeNull();
        }

        [Fact]
        public void PreConfigure_T4()
        {
            var options = Substitute.For<ExtensibleOptions>();
            var namedConfigure = new PreConfigureOptions<ExtensibleOptions, object, object, object, object>("Named", default, default, default, default, (o, d1, d2, d3, d4) => o.SetOption("named", "value"));
            var Configure = new PreConfigureOptions<ExtensibleOptions, object, object, object, object>(DefaultName, default, default, default, default, (o, d1, d2, d3, d4) => o.SetOption("default", "value"));
            namedConfigure.PreConfigure(options);
            Configure.PreConfigure(options);
            options.GetOption<string>("named").ShouldBeNull();
            options.GetOption<string>("default").ShouldBe("value");
            var options2 = Substitute.For<ExtensibleOptions>();
            namedConfigure.PreConfigure("Named", options2);
            Configure.PreConfigure("Named", options2);
            options2.GetOption<string>("named").ShouldBe("value");
            options2.GetOption<string>("default").ShouldBeNull();
        }

        [Fact]
        public void PreConfigure_T5()
        {
            var options = Substitute.For<ExtensibleOptions>();
            var namedConfigure = new PreConfigureOptions<ExtensibleOptions, object, object, object, object, object>("Named", default, default, default, default, default, (o, d1, d2, d3, d4, d5) => o.SetOption("named", "value"));
            var Configure = new PreConfigureOptions<ExtensibleOptions, object, object, object, object, object>(DefaultName, default, default, default, default, default, (o, d1, d2, d3, d4, d5) => o.SetOption("default", "value"));
            namedConfigure.PreConfigure(options);
            Configure.PreConfigure(options);
            options.GetOption<string>("named").ShouldBeNull();
            options.GetOption<string>("default").ShouldBe("value");
            var options2 = Substitute.For<ExtensibleOptions>();
            namedConfigure.PreConfigure("Named", options2);
            Configure.PreConfigure("Named", options2);
            options2.GetOption<string>("named").ShouldBe("value");
            options2.GetOption<string>("default").ShouldBeNull();
        }
    }

    public class PreConfigureOptionsModule : ScorpioModule
    {

    }
}
