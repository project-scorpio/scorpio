using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using NSubstitute;

using Scorpio.Entities;

using Xunit;

namespace Scorpio.Repositories
{
    public class RepositoryExtensions_Tests
    {
        private (RoleEntity, IBasicRepository<RoleEntity, int> repo, ISupportsExplicitLoading<RoleEntity, int> loading) GetClasses()
        {
            var entity = new RoleEntity();
            var repo = Substitute.For<IBasicRepository<RoleEntity, int>, ISupportsExplicitLoading<RoleEntity, int>>();
            var loading = (ISupportsExplicitLoading<RoleEntity, int>)repo;
            loading.EnsureCollectionLoadedAsync(Arg.Any<RoleEntity>(),
           Arg.Any<Expression<Func<RoleEntity, IEnumerable<UserEntity>>>>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
            loading.EnsurePropertyLoadedAsync(Arg.Any<RoleEntity>(),
           Arg.Any<Expression<Func<RoleEntity, string>>>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
            return (entity, repo, loading);
        }

        [Fact]
        public void EnsureCollectionLoaded()
        {
            var (entity, repo, loading) = GetClasses();
            repo.EnsureCollectionLoaded(entity, e => e.Users);
            loading.Received(1).EnsureCollectionLoadedAsync(Arg.Any<RoleEntity>(),
                Arg.Any<Expression<Func<RoleEntity, IEnumerable<UserEntity>>>>(), Arg.Any<CancellationToken>());
        }

        [Fact]
        public void EnsurePropertyLoaded()
        {
            var (entity, repo, loading) = GetClasses();
            repo.EnsurePropertyLoaded(entity, e => e.Name);
            loading.Received(1).EnsurePropertyLoadedAsync(Arg.Any<RoleEntity>(),
                Arg.Any<Expression<Func<RoleEntity, string>>>(), Arg.Any<CancellationToken>());
        }


        public class UserEntity : Entity<int>
        {
            public string Name { get; set; }

            public RoleEntity Role { get; set; }
        }

        public class RoleEntity : Entity<int>
        {
            public string Name { get; set; }

            public ICollection<UserEntity> Users { get; set; }
        }
    }
}
