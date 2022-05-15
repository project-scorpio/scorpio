using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Scorpio.Data;
using Scorpio.Entities;

namespace Scorpio.EntityFrameworkCore
{
    internal class TestDbContext : ScorpioDbContext<TestDbContext>
    {
        public TestDbContext(DbContextOptions<TestDbContext> contextOptions) : base(contextOptions)
        {
        }

        public DbSet<TestTable> TestTables { get; set; }
        public DbSet<TestTableDetail> TableDetails { get; set; }

        public DbSet<SimpleTable> SimpleTables { get; set; }
    }

    public class TestTable : Entity<int>, ISoftDelete, IHasExtraProperties, IStringValue
    {

        public TestTable() => Details = new HashSet<TestTableDetail>();
        public string StringValue { get; set; }
        public bool IsDeleted { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; set; }

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
