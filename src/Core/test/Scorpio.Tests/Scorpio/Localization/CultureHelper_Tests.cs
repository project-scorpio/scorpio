using System.Globalization;

using Shouldly;

using Xunit;

namespace Scorpio.Localization
{
    public class CultureHelper_Tests
    {
        [Fact]
        public void Use()
        {
            var origin = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = origin;
            CultureInfo.CurrentUICulture = origin;
            using (CultureHelper.Use("zh-CN"))
            {
                CultureInfo.CurrentCulture.Name.ShouldBe("zh-CN");
                CultureInfo.CurrentUICulture.Name.ShouldBe("zh-CN");
            }
            CultureInfo.CurrentCulture.Name.ShouldBe("en-US");
            CultureInfo.CurrentUICulture.Name.ShouldBe("en-US");
            using (CultureHelper.Use("zh-CN", "zh-TW"))
            {
                CultureInfo.CurrentCulture.Name.ShouldBe("zh-CN");
                CultureInfo.CurrentUICulture.Name.ShouldBe("zh-TW");
            }
            CultureInfo.CurrentCulture.Name.ShouldBe("en-US");
            CultureInfo.CurrentUICulture.Name.ShouldBe("en-US");
        }

        [Fact]
        public void IsRtl()
        {
            var origin = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = origin;
            CultureInfo.CurrentUICulture = origin;
            CultureHelper.IsRtl.ShouldBeFalse();
            using (CultureHelper.Use("ar-XA"))
            {
                CultureHelper.IsRtl.ShouldBeTrue();
            }
            CultureHelper.IsRtl.ShouldBeFalse();
        }

        [Fact]
        public void IsValidCultureCode()
        {
            CultureHelper.IsValidCultureCode("zh-CN").ShouldBeTrue();
            CultureHelper.IsValidCultureCode("").ShouldBeFalse();
            CultureHelper.IsValidCultureCode("tttttt").ShouldBeFalse();
        }

        [Fact]
        public void GetBaseCultureName()
        {
            CultureHelper.GetBaseCultureName("zh-CN").ShouldBe("zh");
            CultureHelper.GetBaseCultureName("zh").ShouldBe("zh");
            CultureHelper.GetBaseCultureName("en-US").ShouldBe("en");
        }
    }
}
