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
    public class DefaultObjectMapper_Context_Tests : AutoMapperTestBase
    {
        [Fact]
        public void Map()
        {
            var mapper = ServiceProvider.GetRequiredService<IObjectMapper<AutoMapperContext>>();
            Should.NotThrow(() => mapper.Map<ProfiledMapSource, ProfiledMapDestination>(null)).ShouldBeNull();
        }

        [Fact]
        public void MapExists()
        {
            var mapper = ServiceProvider.GetRequiredService<IObjectMapper<AutoMapperContext>>();
            Should.NotThrow(() => mapper.Map(new ProfiledMapSource(), new ProfiledMapDestination())).ShouldNotBeNull();
        }
    }
}
