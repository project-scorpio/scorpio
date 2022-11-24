using System;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.Entities
{
    public class EntityHelper_Tests
    {
        [Fact]
        public void IsEntity()
        {
            EntityHelper.IsEntity(typeof(object)).ShouldBeFalse();
            EntityHelper.IsEntity(typeof(Entity<>)).ShouldBeTrue();
        }

        [Fact]
        public void HasDefaultId()
        {
            EntityHelper.HasDefaultId(Substitute.For<Entity<Guid>>()).ShouldBeTrue();
            EntityHelper.HasDefaultId(Substitute.For<Entity<int>>()).ShouldBeTrue();
            EntityHelper.HasDefaultId(Substitute.For<Entity<int>>(-1)).ShouldBeTrue();
            EntityHelper.HasDefaultId(Substitute.For<Entity<long>>()).ShouldBeTrue();
            EntityHelper.HasDefaultId(Substitute.For<Entity<long>>(-1)).ShouldBeTrue();
            EntityHelper.HasDefaultId(Substitute.For<Entity<Guid>>(Guid.NewGuid())).ShouldBeFalse();
            EntityHelper.HasDefaultId(Substitute.For<Entity<int>>(1)).ShouldBeFalse();
            EntityHelper.HasDefaultId(Substitute.For<Entity<long>>(1)).ShouldBeFalse();

        }

        [Fact]
        public void FindPrimaryKeyType()
        {
            EntityHelper.FindPrimaryKeyType<Entity<int>>().ShouldBe(typeof(int));
            EntityHelper.FindPrimaryKeyType<Entity>().ShouldBeNull();
            Should.Throw<ScorpioException>(() => EntityHelper.FindPrimaryKeyType(typeof(object)));
        }

        [Fact]
        public void CreateEqualityExpressionForId() => EntityHelper.CreateEqualityExpressionForId<Entity<int>, int>(12).Compile()(Substitute.For<Entity<int>>(12)).ShouldBeTrue();
    }
}
