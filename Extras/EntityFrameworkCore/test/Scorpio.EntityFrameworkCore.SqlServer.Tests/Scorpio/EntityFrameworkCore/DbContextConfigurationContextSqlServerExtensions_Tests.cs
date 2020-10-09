using System.Linq;

using NSubstitute;

using Scorpio.EntityFrameworkCore.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public class DbContextConfigurationContextSqlServerExtensions_Tests
    {
        [Fact]
        public void UseSqlServer()
        {
            var context = new DbContextConfigurationContext("DefaultContext", null, null);
            context.DbContextOptions.Options.Extensions.Count().ShouldBe(0);
            context.UseSqlServer(a => { });
            context.DbContextOptions.Options.Extensions.Count().ShouldBe(2);
        }

        [Fact]
        public void UseSqlServer_E()
        {
            var dbContext = Substitute.ForPartsOf<System.Data.Common.DbConnection>();
            var context = new DbContextConfigurationContext("DefaultContext", null, dbContext);
            context.DbContextOptions.Options.Extensions.Count().ShouldBe(0);
            context.UseSqlServer(a => { });
            context.DbContextOptions.Options.Extensions.Count().ShouldBe(2);
        }
    }
}
