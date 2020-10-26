using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Shouldly;

using Xunit;

namespace Scorpio.Repositories
{
    public partial class RepositoryBase_Tests
    {
        [Fact]
        public void WithDetails()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            repo.WithDetails().SequenceEqual(repo).ShouldBeTrue();
        }

        [Fact]
        public void Query()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            IQueryable queryable = repo;
            queryable.ElementType.ShouldBe(typeof(User));
        }


        [Fact]
        public void Query_T()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            repo.First().Id.ShouldBe(10);
        }

        [Fact]
        public void GetEnumerator()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            var exp = new List<User>();
            repo.ForEach(u => exp.Add(u));
            list.SequenceEqual(exp).ShouldBeTrue();
            var enumerator = (repo as IEnumerable).GetEnumerator();
            enumerator.MoveNext().ShouldBeTrue();
            enumerator.Current.ShouldBeOfType<User>().Id.ShouldBe(10);
        }
    }
}
