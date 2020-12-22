using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.AutoMapper.TestClasses;
using Scorpio.ObjectMapping;

using Shouldly;

using Xunit;

namespace Scorpio.AutoMapper
{
    public class AutoMapperAutoObjectMappingProvider_Tests : AutoMapperTestBase
    {
        [Fact]
        public void Map()
        {
            var provider = ServiceProvider.GetService<IAutoObjectMappingProvider>();
            Should.NotThrow(
                () =>
                provider.Map<ProfiledMapSource, ProfiledMapDestination>(new ProfiledMapSource { Value = "Test", IgnoreValue = "Ignore" }))
                .ShouldBeOfType<ProfiledMapDestination>().Action(d =>
                {
                    d.ShouldNotBeNull();
                    d.Value.ShouldBe("Test");
                    d.IgnoreValue.ShouldBeNull();

                });
            Should.NotThrow(
                () =>
                provider.Map(new ProfiledMapSource { Value = "Test" }, new ProfiledMapDestination()))
                .ShouldBeOfType<ProfiledMapDestination>().Value.ShouldBe("Test");
        }
    }
}
