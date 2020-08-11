using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Scorpio.TestBase;

using Shouldly;

using Xunit;
using Xunit.Abstractions;

namespace Scorpio.EntityFrameworkCore
{
    public class ScorpioDbContext_Tests : IntegratedTest<TestModule>
    {
        private readonly ITestOutputHelper _output;

        public ScorpioDbContext_Tests(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public void SaveChanges()
        {
            using (var context = ServiceProvider.GetService<TestDbContext>())
            {
                context.Set<TestTable>().AddRange(
                    new TestTable { Id = 1, StringValue = "Row1" },
                    new TestTable { Id = 2, StringValue = "Row2" },
                    new TestTable { Id = 3, StringValue = "Row3" },
                    new TestTable { Id = 4, StringValue = "Row4" },
                    new TestTable { Id = 5, StringValue = "Row5" },
                    new TestTable { Id = 6, StringValue = "Row6" }
                    );
                context.Set<TestTable>().Add(
                    new TestTable { Id = 7, StringValue = "Row6" }
                    );
                context.SaveChanges();
                context.TestTables.Count().ShouldBe(7);
            }
        }

        [Fact]
        public void SoftDelete()
        {
            using (var context = ServiceProvider.GetService<TestDbContext>())
            {
                context.TestTables.AddRange(
                    new TestTable { Id = 1, StringValue = "Row1" },
                    new TestTable { Id = 2, StringValue = "Row2" },
                    new TestTable { Id = 3, StringValue = "Row3" },
                    new TestTable { Id = 4, StringValue = "Row4" },
                    new TestTable { Id = 5, StringValue = "Row5" },
                    new TestTable { Id = 6, StringValue = "Row6" }
                    );
                context.SaveChanges();
                context.TestTables.Count().ShouldBe(6);
                var entity = context.TestTables.Single(t => t.Id == 6);
                context.TestTables.Remove(entity);
                context.SaveChanges();
                context.TestTables.Count().ShouldBe(5);
                context.TestTables.IgnoreQueryFilters().Count().ShouldBe(6);
                context.TestTables.IgnoreQueryFilters().SingleOrDefault(t => t.Id == 6).IsDeleted.ShouldBeTrue();
            }
        }

        [Fact]
        public void ContextFilter()
        {

            using (var context = ServiceProvider.GetService<TestDbContext>())
            {
                context.TestTables.AddRange(
                    new TestTable { Id = 1, StringValue = "Row1" },
                    new TestTable { Id = 2, StringValue = "Row2" },
                    new TestTable { Id = 3, StringValue = "Row3" },
                    new TestTable { Id = 4, StringValue = "Row4" },
                    new TestTable { Id = 5, StringValue = "Row5" },
                    new TestTable { Id = 6, StringValue = "Row6" }
                    );
                context.SaveChanges();
                context.TestTables.Count().ShouldBe(6);
                ServiceProvider.GetRequiredService<IStringValueProvider>().Value = "Row1";
                context.TestTables.Count().ShouldBe(5);
                context.TestTables.SingleOrDefault(t => t.Id == 1).ShouldBeNull();
                context.TestTables.IgnoreQueryFilters().Count().ShouldBe(6);
                context.TestTables.IgnoreQueryFilters().SingleOrDefault(t => t.Id == 1).ShouldNotBeNull();
            }
        }

    }
}
