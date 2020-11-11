using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using NSubstitute;
using NSubstitute.Extensions;

using Scorpio.Application.Dtos;
using Scorpio.Entities;
using Scorpio.Linq;
using Scorpio.Repositories;

using Shouldly;

using Xunit;

namespace Scorpio.Application.Services
{
    public class AsyncCrudApplicationService_Tests
    {
        [Fact]
        public void Create()
        {
            var repo = Substitute.For<IRepository<Entity, int>>();
            var service = Substitute.ForPartsOf<AsyncCrudApplicationService<Entity, Dto, int>>( repo);
            repo.InsertAsync(default).ReturnsForAnyArgs(c => Task.FromResult(c.Arg<Entity>()));
            Should.NotThrow(() => service.CreateAsync(new Dto { Id = 10, Name = "Tom" })).ShouldNotBeNull();
            repo.ReceivedWithAnyArgs(1).InsertAsync(default);
        }

        [Fact]
        public void Update()
        {
            var repo = Substitute.For<IRepository<Entity, int>>();
            var service = Substitute.ForPartsOf<AsyncCrudApplicationService<Entity, Dto, int>>( repo);
            repo.UpdateAsync(default).ReturnsForAnyArgs(c => Task.FromResult(c.Arg<Entity>()));
            repo.GetAsync(default).ReturnsForAnyArgs(Task.FromResult(new Entity()));
            Should.NotThrow(() => service.UpdateAsync(10, new Dto { Id = 10, Name = "Tom" })).ShouldNotBeNull();
            repo.ReceivedWithAnyArgs(1).UpdateAsync(default);
            repo.ReceivedWithAnyArgs(1).GetAsync(default);
        }

        [Fact]
        public void Get()
        {
            var repo = Substitute.For<IRepository<Entity, int>>();
            var service = Substitute.ForPartsOf<AsyncCrudApplicationService<Entity, Dto, int>>( repo);
            repo.GetAsync(default).ReturnsForAnyArgs(Task.FromResult(new Entity { Id = 10, Name = "Tom" }));
            Should.NotThrow(() => service.GetAsync(10)).Action(d => d.ShouldNotBeNull()).Action(d => d.Name.ShouldBe("Tom"));
            repo.ReceivedWithAnyArgs(1).GetAsync(default);
        }

        [Fact]
        public void Delete()
        {
            var repo = Substitute.For<IRepository<Entity, int>>();
            var service = Substitute.ForPartsOf<AsyncCrudApplicationService<Entity, Dto, int>>( repo);
            Should.NotThrow(() => service.DeleteAsync(10));
            repo.ReceivedWithAnyArgs(1).DeleteAsync(1);
        }

        [Fact]
        public void GetList()
        {
            var list = new List<Entity>
            {
                new Entity{Id=1,Name="Tom1"},
                new Entity{Id=2,Name="Tom2"},
                new Entity{Id=3,Name="Tom3"},
                new Entity{Id=4,Name="Tom4"},
                new Entity{Id=5,Name="Tom5"},
                new Entity{Id=6,Name="Tom6"},
                new Entity{Id=7,Name="Tom7"},
                new Entity{Id=8,Name="Tom8"},
                new Entity{Id=9,Name="Tom9"},
                new Entity{Id=10,Name="Tom10"},
            }.AsQueryable();
            var repo = Substitute.For<IRepository<Entity, int>>();
            var executer = Substitute.For<IAsyncQueryableExecuter>();
            repo.Expression.Returns(list.Expression);
            repo.Provider.Returns(list.Provider);
            executer.CountAsync<Entity>(default).ReturnsForAnyArgs(c => Task.FromResult(c.Arg<IQueryable<Entity>>().Count()));
            executer.ToListAsync<Dto>(default).ReturnsForAnyArgs(c => Task.FromResult(c.Arg<IQueryable<Dto>>().ToList()));
            var service = Substitute.ForPartsOf<AsyncCrudApplicationService<Entity, Dto, int>>( repo);
            service.AsyncQueryableExecuter=executer;

            Should.NotThrow(() => service.GetListAsync(new ListRequest<Dto>(1, 2)))
                  .Action(r => r.Count.ShouldBe(2))
                  .Action(r => r.TotalCount.ShouldBe(10));

        }

        public class Entity : Entity<int>
        {
            public string Name { get; set; }
        }

        public class Dto : EntityDto<int>
        {
            public string Name { get; set; }
        }
    }
}
