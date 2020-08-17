using System;

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using Shouldly;

using Xunit;

namespace Scorpio.Auditing
{
    public class AuditingHelper_Tests : Scorpio.TestBase.IntegratedTest<AuditingTestModule>
    {
        private readonly IAuditingHelper _auditingHelper;

        public AuditingHelper_Tests()
        {
            _auditingHelper = ServiceProvider.GetService<IAuditingHelper>();
        }

        [Theory]
        [InlineData(typeof(NonAuditingClassWithAuditedAttribute), false)]
        [InlineData(typeof(AuditingClassWithAuditedAttribute), true)]
        [InlineData(typeof(AuditingMethodWithAuditedAttribute), true)]
        [InlineData(typeof(DisableAuditingClassWithAuditedAttribute), false)]
        [InlineData(typeof(DisableAuditingMethodWithAuditedAttribute), false)]
        [InlineData(typeof(AuditingClassDisableAuditingMethodWithAuditedAttribute), false)]
        [InlineData(typeof(DisableAuditingClassAuditingMethodWithAuditedAttribute), true)]
        public void ShouldSaveAuditForAttribute(Type type, bool result)
        {
            var method = type.GetMethod("Method");
            _auditingHelper.ShouldSaveAudit(method).ShouldBe(result);
        }

        [Fact]
        public void CreateAuditInfo()
        {
            var actual = _auditingHelper.CreateAuditInfo();
            actual.ShouldBeOfType<AuditInfo>().ShouldNotBeNull();
            actual.CurrentUser.ShouldBe("TestUser");
        }

        [Fact]
        public void CreateAuditAction()
        {
            var type = typeof(NonAuditingClassWithAuditedAttribute);
            var method = type.GetMethod("Action");
            var actual = _auditingHelper.CreateAuditAction(type, method, new object[] { "Test", new TestClass { Name="Test",Age=18 ,Descript="Descript",IgnoreClass=new IgnoreClass(),Data=new object()} });
            actual.ServiceName.ShouldBe(type.FullName);
            actual.MethodName.ShouldBe(method.Name);
            actual.Parameters.ShouldBe("{\"name\":\"Test\",\"age\":{\"name\":\"Test\",\"age\":18}}");

        }
        class TestClass
        {
            public string Name { get; set; }

            public int Age { get; set; }

            [DisableAuditing]
            public string Descript { get; set; }

            [JsonIgnore]
            public object Data { get; set; }

            public IgnoreClass  IgnoreClass { get; set; }
        }

        internal class IgnoreClass
        {

        }

    }


    class NonAuditingClassWithAuditedAttribute
    {
        public void Method()
        {
            // Method intentionally left empty.
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:删除未使用的参数", Justification = "<挂起>")]
        public void Action(string name, int age)
        {
            // Method intentionally left empty.
        }
    }

    [Audited]
    class AuditingClassWithAuditedAttribute
    {
        public void Method()
        {
            // Method intentionally left empty.
        }
    }

    class AuditingMethodWithAuditedAttribute
    {
        [Audited]
        public void Method()
        {
            // Method intentionally left empty.
        }
    }

    [DisableAuditing]
    class DisableAuditingClassWithAuditedAttribute
    {
        public void Method()
        {
            // Method intentionally left empty.
        }
    }

    class DisableAuditingMethodWithAuditedAttribute
    {
        [DisableAuditing]
        public void Method()
        {
            // Method intentionally left empty.
        }
    }


    [Audited]
    class AuditingClassDisableAuditingMethodWithAuditedAttribute
    {
        [DisableAuditing]
        public void Method()
        {
            // Method intentionally left empty.
        }
    }

    [DisableAuditing]
    class DisableAuditingClassAuditingMethodWithAuditedAttribute
    {
        [Audited]
        public void Method()
        {
            // Method intentionally left empty.
        }
    }

}
