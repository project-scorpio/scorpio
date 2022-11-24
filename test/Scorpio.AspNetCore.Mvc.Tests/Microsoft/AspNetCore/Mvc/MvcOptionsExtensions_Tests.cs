using System.Linq;

using Scorpio.AspNetCore.Mvc.Filters;

using Shouldly;

using Xunit;

namespace Microsoft.AspNetCore.Mvc
{
    public class MvcOptionsExtensions_Tests
    {
        [Fact]
        public void AddScorpio()
        {
            var opt = new MvcOptions();
            opt.AddScorpio();
            opt.Filters.OfType<ServiceFilterAttribute>().Where(t => t.ServiceType == typeof(AuditingActionFilter)).ShouldHaveSingleItem();
            opt.Filters.OfType<ServiceFilterAttribute>().Where(t => t.ServiceType == typeof(AuditingPageFilter)).ShouldHaveSingleItem();
        }
    }
}
