using System;
using System.Collections.Generic;
using System.Linq;

using NSubstitute;

using Scorpio.Application.Dtos;
using Scorpio.Entities;
using Scorpio.Repositories;

using Shouldly;

using Xunit;

namespace Scorpio.Application.Services
{
    public class CrudApplicationService_Tests
    {
        [Fact]
        public void Create()
        {
            var repo = Substitute.For<IRepository<Entity, int>>();
            var service = Substitute.ForPartsOf<CrudApplicationService<Entity, Dto, int>>( repo);
            repo.Insert(default).ReturnsForAnyArgs(c => c.Arg<Entity>());
            Should.NotThrow(() => service.Create(new Dto { Id = 10, Name = "Tom" })).ShouldNotBeNull();
            repo.ReceivedWithAnyArgs(1).Insert(default);
        }

        [Fact]
        public void Update()
        {
            var repo = Substitute.For<IRepository<Entity, int>>();
            var service = Substitute.ForPartsOf<CrudApplicationService<Entity, Dto, int>>( repo);
            repo.Update(default).ReturnsForAnyArgs(c => c.Arg<Entity>());
            repo.Get(default).ReturnsForAnyArgs(new Entity());
            Should.NotThrow(() => service.Update(10, new Dto { Id = 10, Name = "Tom" })).ShouldNotBeNull();
            repo.ReceivedWithAnyArgs(1).Update(default);
        }

        [Fact]
        public void Get()
        {
            var repo = Substitute.For<IRepository<Entity, int>>();
            var service = Substitute.ForPartsOf<CrudApplicationService<Entity, Dto, int>>( repo);
            repo.Get(default).ReturnsForAnyArgs(new Entity { Id = 10, Name = "Tom" });
            Should.NotThrow(() => service.Get(10))
                  .Action(d => d.ShouldNotBeNull())
                  .Action(d => d.Name.ShouldBe("Tom"))
                  .Action(d => d.ToString().ShouldBe($"[DTO: {typeof(Dto).Name}] Id = {10}"));
            repo.ReceivedWithAnyArgs(1).Get(default);
        }

        [Fact]
        public void Delete()
        {
            var repo = Substitute.For<IRepository<Entity, int>>();
            var service = Substitute.ForPartsOf<CrudApplicationService<Entity, Dto, int>>( repo);
            Should.NotThrow(() => service.Delete(10));
            repo.ReceivedWithAnyArgs(1).Delete(1);
        }

        [Fact]
        public void CommonPostfixes()
        {
            ApplicationService.CommonPostfixes.Action(c => c.ShouldContain("AppService"))
                                              .Action(c => c.ShouldContain("ApplicationService"))
                                              .Action(c => c.ShouldContain("Service"));
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
            repo.Expression.Returns(list.Expression);
            repo.Provider.Returns(list.Provider);
            var service = Substitute.ForPartsOf<CrudApplicationService<Entity, Dto, int>>( repo);
            Should.NotThrow(() => service.GetList(new ListRequest<Dto>(1, 2)))
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
