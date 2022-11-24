using System;

using Microsoft.Extensions.Localization;

using NSubstitute;
using NSubstitute.Extensions;

using Shouldly;

using Xunit;

namespace Scorpio.Localization
{
    public class LocalizationContext_Tests
    {
        [Fact]
        public void Ctor()
        {
            Should.Throw<ArgumentNullException>(()=>new LocalizationContext(null)).ParamName.ShouldBe("serviceProvider");
            var servieProvider=Substitute.For<IServiceProvider>();
            Should.Throw<InvalidOperationException>(()=>new LocalizationContext(servieProvider));
            var factory=Substitute.For<IStringLocalizerFactory>();
            servieProvider.Configure().GetService(typeof(IStringLocalizerFactory)).Returns(factory);
            Should.NotThrow(()=>new LocalizationContext(servieProvider)).Action(c =>
            {
                c.ServiceProvider.ShouldBe(servieProvider);
                c.LocalizerFactory.ShouldBe(factory);
            });
        }
    }
}
