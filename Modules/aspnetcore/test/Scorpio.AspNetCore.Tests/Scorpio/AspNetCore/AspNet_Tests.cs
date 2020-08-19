using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

using Scorpio.AspNetCore.TestBase;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore
{
    public class AspNet_Tests: AspNetCoreTestBase
    {
        protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options)
        {
            options.UseAspectCore();
        }

        [Fact]
        public async System.Threading.Tasks.Task HostAsync()
        {
            var act = await Should.NotThrow(() => Client.GetAsync("")).Content.ReadAsStringAsync();
            act.ShouldBe("test");
        }
    }
}
