using System;
using System.Collections.Generic;

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
        public DbSet<TestTableDetail>  TableDetails { get; set; }

        public DbSet<SimpleTable>  SimpleTables { get; set; }
    }

    public class TestTable : Entity<int>,ISoftDelete,IHasExtraProperties,IStringValue
    {

        public TestTable()
        {
            Details = new HashSet<TestTableDetail>();
        }
        public string StringValue { get; set; }
        public bool IsDeleted { get; set; }

        public IDictionary<string, object> ExtraProperties { get; set; }

        public virtual ICollection<TestTableDetail>  Details { get; set; }
    }

    public class TestTableDetail : Entity<int>
    {
        public string DetailValue { get; set; }
        public virtual TestTable TestTable { get; set; }
    }

    public class SimpleTable:Entity<int>
    {
        public string StringValue { get; set; }

    }
}
