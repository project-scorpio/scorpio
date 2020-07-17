using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Scorpio.Data;
using Scorpio.Entities;

namespace Scorpio.EntityFrameworkCore
{
    class TestDbContext : ScorpioDbContext<TestDbContext>
    {
        public TestDbContext(IServiceProvider serviceProvider, DbContextOptions<TestDbContext> contextOptions, IOptions<DataFilterOptions> filterOptions) : base(serviceProvider, contextOptions, filterOptions)
        {
        }

        public DbSet<TestTable> TestTables { get; set; }
    }

    public class TestTable : Entity<int>
    {
        public string StringValue { get; set; }
    }
}
