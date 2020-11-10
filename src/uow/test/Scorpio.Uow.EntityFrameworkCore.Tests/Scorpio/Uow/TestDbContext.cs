using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Scorpio.Data;
using Scorpio.Entities;
using Scorpio.EntityFrameworkCore;

namespace Scorpio.Uow
{
    internal class TestDbContext : ScorpioDbContext<TestDbContext>
    {
        public TestDbContext(IServiceProvider serviceProvider,
            DbContextOptions<TestDbContext> contextOptions,
            IOptions<DataFilterOptions> filterOptions) : base(serviceProvider, contextOptions, filterOptions)
        {
        }

        public DbSet<TestTable> TestTables { get; set; }
        public DbSet<TestTableDetail> TableDetails { get; set; }

        public DbSet<SimpleTable> SimpleTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TestTable>().HasData(
                    new TestTable { Id = 1, StringValue = "Row1" },
                    new TestTable { Id = 2, StringValue = "Row2" },
                    new TestTable { Id = 3, StringValue = "Row3" },
                    new TestTable { Id = 4, StringValue = "Row4" },
                    new TestTable { Id = 5, StringValue = "Row5" },
                    new TestTable { Id = 6, StringValue = "Row6" }
                    );
        }
    }

    public class TestTable : Entity<int>, ISoftDelete, IHasExtraProperties
    {

        public TestTable() => Details = new HashSet<TestTableDetail>();
        public string StringValue { get; set; }
        public bool IsDeleted { get; set; }

        public IDictionary<string, object> ExtraProperties { get; set; }

        public virtual ICollection<TestTableDetail> Details { get; set; }
    }

    public class TestTableDetail : Entity<int>
    {
        public string DetailValue { get; set; }
        public virtual TestTable TestTable { get; set; }
    }

    public class SimpleTable : Entity<int>
    {
        public string StringValue { get; set; }

    }
}
