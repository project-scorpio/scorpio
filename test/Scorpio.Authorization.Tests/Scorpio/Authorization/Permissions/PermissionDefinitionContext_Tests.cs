
using Shouldly;

using Xunit;

namespace Scorpio.Authorization.Permissions
{
    public class PermissionDefinitionContext_Tests
    {
        [Fact]
        public void AddGroup()
        {
            var context = new PermissionDefinitionContext();
            Should.NotThrow(() => context.AddGroup("Group1")).Name.ShouldBe("Group1");
            context.Groups.ShouldHaveSingleItem().Key.ShouldBe("Group1");
            Should.Throw<ScorpioException>(() => context.AddGroup("Group1"));
            context.Groups.ShouldHaveSingleItem().Key.ShouldBe("Group1");
        }

        [Fact]
        public void GetGroupOrNull()
        {
            var context = new PermissionDefinitionContext();
            Should.NotThrow(() => context.AddGroup("Group1")).Name.ShouldBe("Group1");
            context.GetGroupOrNull("Group1").ShouldNotBeNull();
            context.GetGroupOrNull("Group2").ShouldBeNull();
        }
    }
}
