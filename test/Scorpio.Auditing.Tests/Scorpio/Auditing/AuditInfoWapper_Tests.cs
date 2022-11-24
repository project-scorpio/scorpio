using System;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.Auditing
{
    public class AuditInfoWapper_Tests
    {
        [Fact]
        public void Property()
        {
            var audit = new AuditInfo();
            var wapper = Substitute.ForPartsOf<AuditInfoWapper>(audit);
            audit.CurrentUser.ShouldBeNull();
            wapper.CurrentUser = "Current";
            audit.CurrentUser.ShouldBe("Current");
            wapper.CurrentUser.ShouldBe("Current");
            audit.ImpersonatorUser.ShouldBeNull();
            wapper.ImpersonatorUser = "Current";
            audit.ImpersonatorUser.ShouldBe("Current");
            wapper.ImpersonatorUser.ShouldBe("Current");
            audit.ExecutionTime.ShouldBe(default);
            wapper.ExecutionTime = DateTime.MaxValue;
            audit.ExecutionTime.ShouldBe(DateTime.MaxValue);
            wapper.ExecutionTime.ShouldBe(DateTime.MaxValue);

            audit.Actions.ShouldBeEmpty();
            wapper.Actions.Add(new AuditActionInfo());
            audit.Actions.ShouldHaveSingleItem();

            audit.Exceptions.ShouldBeEmpty();
            wapper.Exceptions.Add(new Exception());
            audit.Exceptions.ShouldHaveSingleItem();

            audit.ExtraProperties.ShouldBeEmpty();
            wapper.ExtraProperties.Add("Key", "Value");
            audit.ExtraProperties.ShouldHaveSingleItem();

            audit.Comments.ShouldBeEmpty();
            wapper.Comments.Add("Comment");
            audit.Comments.ShouldHaveSingleItem();

        }

        [Fact]
        public void GetExtraProperty()
        {
            var audit = new AuditInfo();
            var wapper = new FakeAuditInfoWapper(audit);
            audit.ExtraProperties.ShouldBeEmpty();
            wapper.SomeValue = "Value";
            audit.ExtraProperties.ShouldHaveSingleItem();
            wapper.SomeValue.ShouldBe("Value");
        }

        private class FakeAuditInfoWapper : AuditInfoWapper
        {
            public FakeAuditInfoWapper(AuditInfo auditInfo) : base(auditInfo)
            {
            }

            public string SomeValue { get => GetExtraProperty<string>(nameof(SomeValue)); set => SetExtraProperty(nameof(SomeValue), value); }
        }
    }
}
