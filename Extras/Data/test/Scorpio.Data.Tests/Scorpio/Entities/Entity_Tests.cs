using System;
using System.Collections.Generic;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.Entities
{
    public class Entity_Tests
    {
        [Fact]
        public void GetKeys()
        {
            var entity = Substitute.For<Entity<int>>(123);
            entity.WhenForAnyArgs(x => x.GetKeys()).CallBase();
            entity.GetKeys().ShouldHaveSingleItem().ShouldBe(123);
        }
        [Fact]
        public void Entity_ToString()
        {
            var entity = Substitute.For<Entity<int>>(123);
            entity.WhenForAnyArgs(x => x.GetKeys()).CallBase();
            var exp = $"[ENTITY: {entity.GetType().Name}] Keys = {entity.GetKeys().ExpandToString(", ")}";
            entity.ToString().ShouldBe(exp);
        }

        [Fact]
        public void IsTransient_Int()
        {
            var entity = Substitute.For<Entity<int>>(123);
            entity.WhenForAnyArgs(x => x.IsTransient()).CallBase();
            entity.IsTransient().ShouldBeFalse();
        }

        [Fact]
        public void IsTransient_Long()
        {
            var entity = Substitute.For<Entity<long>>(123);
            entity.WhenForAnyArgs(x => x.IsTransient()).CallBase();
            entity.IsTransient().ShouldBeFalse();
        }

        [Fact]
        public void IsTransient()
        {
            var entity = Substitute.For<Entity<string>>(Guid.NewGuid().ToString());
            entity.WhenForAnyArgs(x => x.IsTransient()).CallBase();
            entity.IsTransient().ShouldBeFalse();
        }

        [Fact]
        public void IsTransient_Int_True()
        {
            var entity = Substitute.For<Entity<int>>();
            entity.WhenForAnyArgs(x => x.IsTransient()).CallBase();
            entity.IsTransient().ShouldBeTrue();
        }

        [Fact]
        public void IsTransient_Long_True()
        {
            var entity = Substitute.For<Entity<long>>();
            entity.WhenForAnyArgs(x => x.IsTransient()).CallBase();
            entity.IsTransient().ShouldBeTrue();
        }

        [Fact]
        public void IsTransient_True()
        {
            var entity = Substitute.For<Entity<string>>();
            entity.WhenForAnyArgs(x => x.IsTransient()).CallBase();
            entity.IsTransient().ShouldBeTrue();
        }
        [Fact]
        public void IsTransient_GUID_True()
        {
            var entity = Substitute.For<Entity<Guid>>();
            entity.WhenForAnyArgs(x => x.IsTransient()).CallBase();
            entity.IsTransient().ShouldBeTrue();
        }

        [Fact]
        public void Entity_Equals()
        {
            var entity = Substitute.For<Entity<int>>();
            var entity2 = Substitute.For<Entity<int>>();
            entity.When(x => x.Equals(Arg.Any<object>())).CallBase();
            entity.WhenForAnyArgs(x => x.IsTransient()).CallBase();
            entity2.When(x => x.Equals(Arg.Any<object>())).CallBase();
            entity2.WhenForAnyArgs(x => x.IsTransient()).CallBase();
            entity.Equals(new object()).ShouldBeFalse();
            entity.Equals(entity).ShouldBeTrue();
            entity.Equals(entity2).ShouldBeFalse();
            entity.Id = 1;
            entity2.Id = 1;
            entity.Equals(entity2).ShouldBeTrue();
        }

        [Fact]
        public void Entity_GetHashCode()
        {
            var entity = Substitute.For<Entity<int>>(123);
            entity.When(x => x.Equals(Arg.Any<object>())).CallBase();
            entity.WhenForAnyArgs(x => x.IsTransient()).CallBase();
            entity.GetHashCode().ShouldBe(HashCode.Combine(123));
        }
    }
}
