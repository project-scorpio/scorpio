using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Shouldly;

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
            var actual = _auditingHelper.CreateAuditAction(type, method, new object[] { "Test", 18 });
            actual.ServiceName.ShouldBe(type.FullName);
            actual.MethodName.ShouldBe( method.Name);
            actual.Parameters.ShouldBe("{\"name\":\"Test\",\"age\":18}");
        }
    }


    class NonAuditingClassWithAuditedAttribute
    {
        public void Method()
        {

        }

        public void Action(string name, int age)
        {

        }
    }

    [Audited]
    class AuditingClassWithAuditedAttribute
    {
        public void Method()
        {

        }
    }

    class AuditingMethodWithAuditedAttribute
    {
        [Audited]
        public void Method()
        {

        }
    }

    [DisableAuditing]
    class DisableAuditingClassWithAuditedAttribute
    {
        public void Method()
        {

        }
    }

    class DisableAuditingMethodWithAuditedAttribute
    {
        [DisableAuditing]
        public void Method()
        {

        }
    }


    [Audited]
    class AuditingClassDisableAuditingMethodWithAuditedAttribute
    {
        [DisableAuditing]
        public void Method()
        {

        }
    }

    [DisableAuditing]
    class DisableAuditingClassAuditingMethodWithAuditedAttribute
    {
        [Audited]
        public void Method()
        {

        }
    }

}
