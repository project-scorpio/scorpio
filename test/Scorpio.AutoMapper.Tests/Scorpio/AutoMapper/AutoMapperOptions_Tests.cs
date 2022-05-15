using System;
using System.Collections.Generic;

using AutoMapper;

using NSubstitute;
using NSubstitute.Extensions;

using Shouldly;

using Xunit;

namespace Scorpio.AutoMapper
{
    public class AutoMapperOptions_Tests
    {
        [Fact]
        public void AddMaps()
        {
            var opt = new AutoMapperOptions();
            opt.AddMaps<AutoMapperTestModule>();
            var configure = Substitute.For<IAutoMapperConfigurationContext>();
            var expression = Substitute.For<IMapperConfigurationExpression>();
            configure.Configure().MapperConfiguration.Returns(expression);
            opt.Configurators.ShouldHaveSingleItem()(configure);
            configure.Received(1).MapperConfiguration.AddMaps(typeof(AutoMapperTestModule).Assembly);
        }

        [Fact]
        public void AddMapsAndValidation()
        {
            var opt = new AutoMapperOptions();
            opt.AddMaps<AutoMapperTestModule>(true);
            var configure = Substitute.For<IAutoMapperConfigurationContext>();
            var expression = Substitute.For<IMapperConfigurationExpression>();
            configure.Configure().MapperConfiguration.Returns(expression);
            opt.Configurators.ShouldHaveSingleItem()(configure);
            configure.Received(1).MapperConfiguration.AddMaps(typeof(AutoMapperTestModule).Assembly);
            opt.ValidatingProfiles.ShouldHaveSingleItem().ShouldBe(typeof(TestProfile));
        }

        [Fact]
        public void AddProfile()
        {
            var opt = new AutoMapperOptions();
            opt.AddProfile<TestProfile>();
            var configure = Substitute.For<IAutoMapperConfigurationContext>();
            var expression = Substitute.For<IMapperConfigurationExpression>();
            configure.Configure().MapperConfiguration.Returns(expression);
            opt.Configurators.ShouldHaveSingleItem()(configure);
            configure.Received(1).MapperConfiguration.AddProfile(typeof(TestProfile));
        }

        [Fact]
        public void AddProfileAndValidation()
        {
            var opt = new AutoMapperOptions();
            opt.AddProfile<TestProfile>(true);
            var configure = Substitute.For<IAutoMapperConfigurationContext>();
            var expression = Substitute.For<IMapperConfigurationExpression>();
            configure.Configure().MapperConfiguration.Returns(expression);
            opt.Configurators.ShouldHaveSingleItem()(configure);
            configure.Received(1).MapperConfiguration.AddProfile(typeof(TestProfile));
            opt.ValidatingProfiles.ShouldHaveSingleItem().ShouldBe(typeof(TestProfile));
        }

        [Fact]
        public void ValidateProfile()
        {
            var opt = new AutoMapperOptions();
            opt.ValidateProfile<TestProfile>(false);
            opt.ValidatingProfiles.IsNullOrEmpty();
            opt.ValidateProfile<TestProfile>();
            opt.ValidateProfile<TestProfile>(true);
            opt.ValidatingProfiles.ShouldHaveSingleItem().ShouldBe(typeof(TestProfile));
            opt.ValidateProfile<TestProfile>();
            opt.ValidatingProfiles.ShouldHaveSingleItem().ShouldBe(typeof(TestProfile));
            opt.ValidatingProfiles.ShouldHaveSingleItem().ShouldBe(typeof(TestProfile));
            opt.ValidateProfile<TestProfile>(false);
            opt.ValidatingProfiles.IsNullOrEmpty();
            opt.ValidateProfile<TestProfile>(true);
            opt.ValidatingProfiles.ShouldHaveSingleItem().ShouldBe(typeof(TestProfile));
            opt.ValidateProfile<TestProfile>(true);
            opt.ValidatingProfiles.ShouldHaveSingleItem().ShouldBe(typeof(TestProfile));
            opt.ValidateProfile<TestProfile>();
            opt.ValidatingProfiles.ShouldHaveSingleItem().ShouldBe(typeof(TestProfile));

        }
    }
}
