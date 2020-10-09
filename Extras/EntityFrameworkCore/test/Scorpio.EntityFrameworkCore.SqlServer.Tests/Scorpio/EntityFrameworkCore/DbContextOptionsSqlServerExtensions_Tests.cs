using System;
using System.Linq;

using Scorpio.EntityFrameworkCore.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public class DbContextOptionsSqlServerExtensions_Tests
    {
        [Fact]
        public void UseSqlServer()
        {
            var context = new DbContextConfigurationContext("DefaultContext", null, null);
            var options = new ScorpioDbContextOptions();
            options.UseSqlServer(b => { });
            context.DbContextOptions.Options.Extensions.Count().ShouldBe(0);
            options.DefaultConfigureAction.Invoke(context);
            context.DbContextOptions.Options.Extensions.Count().ShouldBe(2);
        }

        [Fact]
        public void UseSqlServer_T()

        {
            var context = new DbContextConfigurationContext<TestDbContext>("DefaultContext", null, null);
            var options = new ScorpioDbContextOptions();
            options.UseSqlServer<TestDbContext>(b => { });
            context.DbContextOptions.Options.Extensions.Count().ShouldBe(0);
            options.ConfigureActions[typeof(TestDbContext)].ShouldBeOfType<Action<DbContextConfigurationContext<TestDbContext>>>().Invoke(context);
            context.DbContextOptions.Options.Extensions.Count().ShouldBe(2);

        }
    }
}
