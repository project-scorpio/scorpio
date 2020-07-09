using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Shouldly;

using Xunit;

namespace Scorpio.Data
{
    public abstract class DataFilter_Integrated_Tests<TModule> : TestBase.IntegratedTest<TModule>
        where TModule : Modularity.ScorpioModule
    {
        public IDataFilter DataFilter { get; set; }

        public DataFilterOptions DataFilterOptions { get; set; }

        public IDataFilterDescriptor DataFilterDescriptor { get; set; }
        protected DataFilter_Integrated_Tests()
        {
            DataFilter = ServiceProvider.GetService<IDataFilter>();
            DataFilterOptions = ServiceProvider.GetService<IOptions<DataFilterOptions>>().Value;
            DataFilterDescriptor = DataFilterOptions.Descriptors.GetValueOrDefault(typeof(ISoftDelete));
        }
    }

    public class DataFilter_Integrated_Enable_Tests : DataFilter_Integrated_Tests<DataFilterEnableModule>
    {
        [Fact]
        public void Test()
        {
            DataFilter.IsEnabled<ISoftDelete>().ShouldBeTrue();
            var filterContext = new FakeFilterContext();
            DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeFalse();
            DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
            using (DataFilter.Disable<ISoftDelete>())
            {
                DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeTrue();
                DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
                using (DataFilter.Enable<ISoftDelete>())
                {
                    DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeFalse();
                    DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
                }
                DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeTrue();
                DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
            }
            DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeFalse();
            DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
        }

    }

    public class DataFilter_Integrated_Disable_Tests : DataFilter_Integrated_Tests<DataFilterDisableModule>
    {
        [Fact]
        public void Test()
        {
            DataFilter.IsEnabled<ISoftDelete>().ShouldBeFalse();
            var filterContext = new FakeFilterContext();
            DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeTrue();
            DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
            using (DataFilter.Enable<ISoftDelete>())
            {
                DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeFalse();
                DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
                using (DataFilter.Disable<ISoftDelete>())
                {
                    DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeTrue();
                    DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
                }
                DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeFalse();
                DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
            }
            DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeTrue();
            DataFilterDescriptor.BuildFilterExpression<SoftDeleteEntity>(DataFilter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
        }

    }

}
