using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Scorpio.Repositories.EntityFrameworkCore;

using Shouldly;

using Xunit;

namespace Scorpio.EntityFrameworkCore.DependencyInjection
{
    public class ScorpioDbContextOptionsBuilder_Tests
    {
        [Fact]
        public void AddRepository()
        {
            var services = new ServiceCollection();
            var builder = new ScorpioDbContextOptionsBuilder<TestDbContext>(services);
            Should.Throw<ScorpioException>(() => builder.AddRepository(typeof(object), typeof(object)));
            Should.Throw<ScorpioException>(() => builder.AddRepository(typeof(object), typeof(EfCoreRepository<,>)));
            Should.Throw<ScorpioException>(() => builder.AddRepository(typeof(TestTable), typeof(object)));
            Should.NotThrow(() => builder.AddRepository(typeof(TestTable), typeof(EfCoreRepository<,>)));
        }

        [Fact]
        public void AddRepository_T()
        {
            var services = new ServiceCollection();
            var builder = new ScorpioDbContextOptionsBuilder<TestDbContext>(services);
            Should.NotThrow(() => builder.AddRepository<TestTable, EfCoreRepository<TestDbContext, TestTable>>());

        }


        [Fact]
        public void UseLoggerFactory()
        {
            var services = new ServiceCollection();
            var builder = new ScorpioDbContextOptionsBuilder<TestDbContext>(services);
            var b = new DbContextOptionsBuilder<TestDbContext>();
            Should.NotThrow(() => builder.UseLoggerFactory(null));
            b.Options.Extensions.IsNullOrEmpty();
            builder.OptionsActions.ForEach(a => a(b));
            b.Options.Extensions.ShouldHaveSingleItem();
        }

        [Fact]
        public void UseMemoryCache()
        {
            var services = new ServiceCollection();
            var builder = new ScorpioDbContextOptionsBuilder<TestDbContext>(services);
            var b = new DbContextOptionsBuilder<TestDbContext>();
            Should.NotThrow(() => builder.UseMemoryCache(null));
            b.Options.Extensions.IsNullOrEmpty();
            builder.OptionsActions.ForEach(a => a(b));
            b.Options.Extensions.ShouldHaveSingleItem();
        }

        [Fact]
        public void SetDefaultRepository()
        {
            var services = new ServiceCollection();
            var builder = new ScorpioDbContextOptionsBuilder<TestDbContext>(services);
            Should.Throw<ScorpioException>(() => builder.SetDefaultRepository(typeof(object)));
            Should.NotThrow(() => builder.SetDefaultRepository(typeof(EfCoreRepository<,,>)));
        }
    }
}
