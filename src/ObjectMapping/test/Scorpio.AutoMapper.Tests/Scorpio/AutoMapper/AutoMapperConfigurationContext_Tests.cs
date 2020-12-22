using System;
using System.Collections.Generic;
using System.Text;

using AutoMapper;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.AutoMapper
{
    public class AutoMapperConfigurationContext_Tests
    {
        [Fact]
        public void Ctor()
        {
            var expression = Substitute.For<IMapperConfigurationExpression>();
            var serviceProvider = Substitute.For<IServiceProvider>();
            Should.Throw<ArgumentNullException>(() => new AutoMapperConfigurationContext(default, default)).ParamName.ShouldBe("mapperConfigurationExpression");
            Should.Throw<ArgumentNullException>(() => new AutoMapperConfigurationContext(expression, default)).ParamName.ShouldBe("serviceProvider");
            Should.Throw<ArgumentNullException>(() => new AutoMapperConfigurationContext(default, serviceProvider)).ParamName.ShouldBe("mapperConfigurationExpression");
            Should.NotThrow(() => new AutoMapperConfigurationContext(expression, serviceProvider)).Action(c =>
            {
                c.ShouldNotBeNull();
                c.MapperConfiguration.ShouldBe(expression);
                c.ServiceProvider.ShouldBe(serviceProvider);
            });
        }
    }
}
