using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Data
{
    public class DataFilterDescriptor_Tests
    {
        [Fact]
        public void CtorEnable()
        {
            var mock = new Moq.Mock<IServiceProvider>();
            var options = new DataFilterOptions();
            options.Configure<ISoftDelete>(d => d.Expression(t => t.IsDeleted == false));
            var descriptor = options.Filter<ISoftDelete>();
            mock.Setup(s => s.GetService(Moq.It.IsAny<Type>())).Returns(new DataFilter<ISoftDelete>(new OptionsWrapper<DataFilterOptions>(options)));
            var filter = new DataFilter(mock.Object);
            var filterContext = new FakeFilterContext();
            descriptor.BuildFilterExpression<SoftDeleteEntity>(filter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeFalse();
            descriptor.BuildFilterExpression<SoftDeleteEntity>(filter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
            using (filter.Disable<ISoftDelete>())
            {
                descriptor.BuildFilterExpression<SoftDeleteEntity>(filter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeTrue();
                descriptor.BuildFilterExpression<SoftDeleteEntity>(filter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
            }
        }

        [Fact]
        public void CtorDisable()
        {
            var mock = new Moq.Mock<IServiceProvider>();
            var options = new DataFilterOptions();
            options.Configure<ISoftDelete>(d =>
            {
                d.Disable();
                d.Expression(t => t.IsDeleted == false);
            });
            var descriptor = options.Filter<ISoftDelete>();
            mock.Setup(s => s.GetService(Moq.It.IsAny<Type>())).Returns(new DataFilter<ISoftDelete>(new OptionsWrapper<DataFilterOptions>(options)));
            var filter = new DataFilter(mock.Object);
            var filterContext = new FakeFilterContext();
            descriptor.BuildFilterExpression<SoftDeleteEntity>(filter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeTrue();
            descriptor.BuildFilterExpression<SoftDeleteEntity>(filter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
            using (filter.Enable<ISoftDelete>())
            {
                descriptor.BuildFilterExpression<SoftDeleteEntity>(filter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeFalse();
                descriptor.BuildFilterExpression<SoftDeleteEntity>(filter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
            }
        }

    }
}
