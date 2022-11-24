using System;
using System.Collections.Generic;
using System.Linq;

using NSubstitute;

using Scorpio.Application.Services;
using Scorpio.Entities;
using Scorpio.Repositories;

using Shouldly;

using Xunit;

namespace Scorpio.Application.Dtos
{
    public class ListRequest_Tests
    {
        public class Entity : Entity<int>
        {
            public string Name { get; set; }

            public bool IsDeleted { get; set; }
        }

        public class Dto : EntityDto<int>
        {
            public string Name { get; set; }
        }

        private ICrudApplicationService<Dto, int> GetService()
        {
            var list = new List<Entity>
            {
                new Entity{Id=1, IsDeleted=false,Name="Tom1"},
                new Entity{Id=2,IsDeleted=false,Name="Tom2"},
                new Entity{Id=3,IsDeleted=false,Name="Tom3"},
                new Entity{Id=4,IsDeleted=false,Name="Tom4"},
                new Entity{Id=5,IsDeleted=true,Name="Tom5"},
                new Entity{Id=6,IsDeleted=true,Name="Tom6"},
                new Entity{Id=7,IsDeleted=true,Name="Tom7"},
                new Entity{Id=8,IsDeleted=true,Name="Tom8"},
                new Entity{Id=9,IsDeleted=true,Name="Tom9"},
                new Entity{Id=10,IsDeleted=false,Name="Tom10"},
            }.AsQueryable();
            var repo = Substitute.For<IRepository<Entity, int>>();
            repo.Expression.Returns(list.Expression);
            repo.Provider.Returns(list.Provider);
            repo.ElementType.Returns(list.ElementType);
            var service = Substitute.ForPartsOf<CrudApplicationService<Entity, Dto, int>>(repo);
            return service;
        }

        [Fact]
        public void Paging()
        {
            var service = GetService();
            Should.NotThrow(() => service.GetList(new ListRequest<Dto>(1, 2)))
                              .Action(r => r.Count.ShouldBe(2))
                              .Action(r => r.TotalCount.ShouldBe(10))
                              .Action(r => r.First().Id.ShouldBe(2));
            Should.NotThrow(() => service.GetList(new ListRequest<Dto>()))
                              .Action(r => r.Count.ShouldBe(10))
                              .Action(r => r.TotalCount.ShouldBe(10))
                              .Action(r => r.First().Id.ShouldBe(1));
        }

        [Fact]
        public void Sorting()
        {
            var service = GetService();
            Should.NotThrow(() => service.GetList(new ListRequest<Dto>(1, 2).Sort("id desc")))
                  .Action(r => r.Count.ShouldBe(2))
                  .Action(r => r.TotalCount.ShouldBe(10))
                  .Action(r => r.First().Id.ShouldBe(9));
            Should.NotThrow(() => service.GetList(new ListRequest<Dto>().Sort("id desc")))
                  .Action(r => r.Count.ShouldBe(10))
                  .Action(r => r.TotalCount.ShouldBe(10))
                  .Action(r => r.First().Id.ShouldBe(10));
            Should.NotThrow(() => service.GetList(new ListRequest<Dto>().Sort("IsDeleted desc").AddSort("id desc")))
                  .Action(r => r.Count.ShouldBe(10))
                  .Action(r => r.TotalCount.ShouldBe(10))
                  .Action(r => r.First().Id.ShouldBe(9));
        }

        [Fact]
        public void Where()
        {
            var service = GetService();
            Should.NotThrow(() => service.GetList(new ListRequest<Dto>(1, 2).Where("id >@0", 4)))
                  .Action(r => r.Count.ShouldBe(2))
                  .Action(r => r.TotalCount.ShouldBe(6))
                  .Action(r => r.First().Id.ShouldBe(6));
            Should.NotThrow(() => service.GetList(new ListRequest<Dto>().Where("id >@0", 4)))
                  .Action(r => r.Count.ShouldBe(6))
                  .Action(r => r.TotalCount.ShouldBe(6))
                  .Action(r => r.First().Id.ShouldBe(5));
        }

    }
}
