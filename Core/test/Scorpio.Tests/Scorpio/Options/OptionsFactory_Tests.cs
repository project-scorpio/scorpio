using System.Collections.Generic;

using Microsoft.Extensions.Options;

using NSubstitute;

using Shouldly;

using Xunit;

using static Microsoft.Extensions.Options.Options;

namespace Scorpio.Options
{
    public class OptionsFactory_Tests
    {
        [Fact]
        public void Create_Default()
        {
            var preList = Substitute.For<IEnumerable<IPreConfigureOptions<TestExtensibleOptions>>>();
            preList.GetEnumerator().MoveNext().Returns(true, true, false);
            preList.GetEnumerator().Current.Returns(
                new PreConfigureOptions<TestExtensibleOptions>(DefaultName, o => o.SetOption("pre-default", "value")),
                new PreConfigureOptions<TestExtensibleOptions>("Named", o => o.SetOption("pre-named", "value")));
            var list = Substitute.For<IEnumerable<IConfigureOptions<TestExtensibleOptions>>>();
            list.GetEnumerator().MoveNext().Returns(true, true, false);
            list.GetEnumerator().Current.Returns(
                new ConfigureOptions<TestExtensibleOptions>(o => o.SetOption("default", "value")),
                new ConfigureNamedOptions<TestExtensibleOptions>("Named", o => o.SetOption("named", "value"))
                );
            var postList = Substitute.For<IEnumerable<IPostConfigureOptions<TestExtensibleOptions>>>();
            postList.GetEnumerator().MoveNext().Returns(true, true, false);
            postList.GetEnumerator().Current.Returns(
                new PostConfigureOptions<TestExtensibleOptions>(DefaultName, o => o.SetOption("post-default", "value")),
                new PostConfigureOptions<TestExtensibleOptions>("Named", o => o.SetOption("post-named", "value"))
                );
            var factory = new OptionsFactory<TestExtensibleOptions>(list, postList, preList);
            var opt = factory.Create(DefaultName);
            opt.ShouldNotBeNull();
            opt.GetOption<string>("pre-default").ShouldBe("value");
            opt.GetOption<string>("default").ShouldBe("value");
            opt.GetOption<string>("post-default").ShouldBe("value");
            opt.GetOption<string>("pre-named").ShouldBeNull();
            opt.GetOption<string>("named").ShouldBeNull();
            opt.GetOption<string>("post-named").ShouldBeNull();
        }

        [Fact]
        public void Create_Named()
        {
            var preList = Substitute.For<IEnumerable<IPreConfigureOptions<TestExtensibleOptions>>>();
            preList.GetEnumerator().MoveNext().Returns(true, true, false);
            preList.GetEnumerator().Current.Returns(
                new PreConfigureOptions<TestExtensibleOptions>(DefaultName, o => o.SetOption("pre-default", "value")),
                new PreConfigureOptions<TestExtensibleOptions>("Named", o => o.SetOption("pre-named", "value")));
            var list = Substitute.For<IEnumerable<IConfigureOptions<TestExtensibleOptions>>>();
            list.GetEnumerator().MoveNext().Returns(true, true, false);
            list.GetEnumerator().Current.Returns(
                new ConfigureOptions<TestExtensibleOptions>(o => o.SetOption("default", "value")),
                new ConfigureNamedOptions<TestExtensibleOptions>("Named", o => o.SetOption("named", "value"))
                );
            var postList = Substitute.For<IEnumerable<IPostConfigureOptions<TestExtensibleOptions>>>();
            postList.GetEnumerator().MoveNext().Returns(true, true, false);
            postList.GetEnumerator().Current.Returns(
                new PostConfigureOptions<TestExtensibleOptions>(DefaultName, o => o.SetOption("post-default", "value")),
                new PostConfigureOptions<TestExtensibleOptions>("Named", o => o.SetOption("post-named", "value"))
                );
            var factory = new OptionsFactory<TestExtensibleOptions>(list, postList, preList);
            var opt = factory.Create("Named");
            opt.ShouldNotBeNull();
            opt.GetOption<string>("pre-default").ShouldBeNull();
            opt.GetOption<string>("default").ShouldBeNull();
            opt.GetOption<string>("post-default").ShouldBeNull();
            opt.GetOption<string>("pre-named").ShouldBe("value");
            opt.GetOption<string>("named").ShouldBe("value");
            opt.GetOption<string>("post-named").ShouldBe("value");
        }

        public class TestExtensibleOptions : ExtensibleOptions
        {
            public TestExtensibleOptions()
            {

            }
        }
    }
}
