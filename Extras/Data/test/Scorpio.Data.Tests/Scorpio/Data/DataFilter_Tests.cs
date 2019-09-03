using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Data
{
    public class DataFilter_Tests
    {
        [Fact]
        public void Enable()
        {
            var mock = new Moq.Mock<IServiceProvider>();
            mock.Setup(s => s.GetService(Moq.It.IsAny<Type>())).Returns(new DataFilter<ITestFilter>());
            var filter = new DataFilter(mock.Object);
            filter.IsEnabled<ITestFilter>().ShouldBeTrue();
            filter.IsEnabled(typeof(ITestFilter)).ShouldBeTrue();
            using (filter.Enable<ITestFilter>())
            {
                filter.IsEnabled<ITestFilter>().ShouldBeTrue();
            }
            filter.IsEnabled<ITestFilter>().ShouldBeTrue();
            mock.Setup(s => s.GetService(Moq.It.IsAny<Type>())).Returns(new DataFilter<ITestFilter>(false));
            filter = new DataFilter(mock.Object);
            filter.IsEnabled<ITestFilter>().ShouldBeFalse();
            using (filter.Enable<ITestFilter>())
            {
                filter.IsEnabled<ITestFilter>().ShouldBeTrue();
            }
            filter.IsEnabled<ITestFilter>().ShouldBeFalse();

        }

        [Fact]
        public void Disable()
        {
            var mock = new Moq.Mock<IServiceProvider>();
            mock.Setup(s => s.GetService(Moq.It.IsAny<Type>())).Returns(new DataFilter<ITestFilter>());
            var filter = new DataFilter(mock.Object);
            filter.IsEnabled<ITestFilter>().ShouldBeTrue();
            using (filter.Disable<ITestFilter>())
            {
                filter.IsEnabled<ITestFilter>().ShouldBeFalse();
            }
            filter.IsEnabled<ITestFilter>().ShouldBeTrue();
            mock.Setup(s => s.GetService(Moq.It.IsAny<Type>())).Returns(new DataFilter<ITestFilter>(false));
            filter = new DataFilter(mock.Object);
            filter.IsEnabled<ITestFilter>().ShouldBeFalse();
            using (filter.Disable<ITestFilter>())
            {
                filter.IsEnabled<ITestFilter>().ShouldBeFalse();
            }
            filter.IsEnabled<ITestFilter>().ShouldBeFalse();
        }
    }
}
