using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Scorpio.EntityFrameworkCore;

using Shouldly;

using Xunit;

namespace Scorpio.Repositories.EntityFrameworkCore
{
    public partial class RepositoryBase_Tests
    {
        [Fact]
        public void WithDetails()
        {
            var repo = GetUsers();
            repo.Insert(new TestTable
            {
                Id = 10,
                StringValue = "test",
                Details = new HashSet<TestTableDetail>
                {
                    new TestTableDetail{ Id=1, DetailValue="Test"}
                }
            });
            repo.WithDetails(r => r.Details).First().Details.Count.ShouldBe(1);
        }

        [Fact]
        public void Query_T()
        {
            var repo = GetUsers();
            repo.Insert(new TestTable { Id = 10, StringValue = "test" });
            repo.First().Id.ShouldBe(10);
        }


    }
}
