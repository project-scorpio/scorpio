using System;
using System.Collections.Generic;
using System.Text;

using Shouldly;

using Xunit;

namespace Scorpio.Aspects
{
    public class CrossCuttingConcerns_Tests : AspectIntegratedTest<AspectTestModule>
    {
        [Fact]
        public void Applying()
        {
            var service = GetRequiredService<IInterceptorTestService>();
            var service2 = GetRequiredService<INonInterceptorTestService>();
            CrossCuttingConcerns.IsApplied(service, "CustomConcern").ShouldBeFalse();
            Should.Throw<ArgumentNullException>(() => CrossCuttingConcerns.Applying(null));
            Should.Throw<ArgumentNullException>(() => CrossCuttingConcerns.Applying(service));
            using (CrossCuttingConcerns.Applying(service, "CustomConcern"))
            {
                CrossCuttingConcerns.IsApplied(service, "CustomConcern").ShouldBeTrue();
                CrossCuttingConcerns.IsApplied(service2, "CustomConcern").ShouldBeFalse();
            }
            CrossCuttingConcerns.IsApplied(service, "CustomConcern").ShouldBeFalse();

            CrossCuttingConcerns.IsApplied(service2, "CustomConcern").ShouldBeFalse();
            using (CrossCuttingConcerns.Applying(service2, "CustomConcern"))
            {
                CrossCuttingConcerns.IsApplied(service, "CustomConcern").ShouldBeFalse();
                CrossCuttingConcerns.IsApplied(service2, "CustomConcern").ShouldBeFalse();
            }
            CrossCuttingConcerns.IsApplied(service, "CustomConcern").ShouldBeFalse();
        }

        [Fact]
        public void AddApplied()
        {
            var service = GetRequiredService<IInterceptorTestService>();
            var service2 = GetRequiredService<INonInterceptorTestService>();
            ((Action)(() => CrossCuttingConcerns.AddApplied(null))).ShouldThrow<ArgumentNullException>();
            ((Action)(() => CrossCuttingConcerns.AddApplied(service))).ShouldThrow<ArgumentNullException>();
            ((Action)(() => CrossCuttingConcerns.AddApplied(service, "CustomConcern"))).ShouldNotThrow();
            CrossCuttingConcerns.GetApplieds(service).ShouldNotBeEmpty();
            CrossCuttingConcerns.GetApplieds(service2).ShouldBeEmpty();
            CrossCuttingConcerns.RemoveApplied(service, "CustomConcern");
        }

        [Fact]
        public void RemoveApplied()
        {
            var service = GetRequiredService<IInterceptorTestService>();
            var service2 = GetRequiredService<INonInterceptorTestService>();
            CrossCuttingConcerns.AddApplied(service, "CustomConcern");
            CrossCuttingConcerns.GetApplieds(service).ShouldNotBeEmpty();
            CrossCuttingConcerns.GetApplieds(service2).ShouldBeEmpty();
            Should.Throw<ArgumentNullException>(() => CrossCuttingConcerns.RemoveApplied(null));
            Should.Throw<ArgumentNullException>(() => CrossCuttingConcerns.RemoveApplied(service));
            Should.NotThrow(() => CrossCuttingConcerns.RemoveApplied(service, "CustomConcern"));
            Should.NotThrow(() => CrossCuttingConcerns.RemoveApplied(service2, "CustomConcern"));
            CrossCuttingConcerns.GetApplieds(service).ShouldBeEmpty();
            CrossCuttingConcerns.GetApplieds(service2).ShouldBeEmpty();
            Should.NotThrow(() => CrossCuttingConcerns.RemoveApplied(service, "CustomConcern"));
        }

        [Fact]
        public void IsApplied()
        {
            var service = GetRequiredService<IInterceptorTestService>();
            var service2 = GetRequiredService<INonInterceptorTestService>();
            CrossCuttingConcerns.AddApplied(service, "CustomConcern");
            CrossCuttingConcerns.AddApplied(service2, "CustomConcern");
            Should.Throw<ArgumentNullException>(() => CrossCuttingConcerns.IsApplied(null, "CustomConcern"));
            Should.Throw<ArgumentNullException>(() => CrossCuttingConcerns.IsApplied(service, null));
            CrossCuttingConcerns.IsApplied(service, "CustomConcern").ShouldBeTrue();
            CrossCuttingConcerns.IsApplied(service, "").ShouldBeFalse();
            CrossCuttingConcerns.IsApplied(service2, "CustomConcern").ShouldBeFalse();
            CrossCuttingConcerns.RemoveApplied(service, "CustomConcern");

        }
    }
}
