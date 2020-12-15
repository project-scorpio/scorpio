using System;
using System.Collections.Generic;
using System.Text;

using Scorpio.TestBase;

namespace Scorpio.Auditing
{
    public class AuditingTestBase:IntegratedTest<AuditingTestModule>
    {
                protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options) => options.UseAspectCore();

    }
}
