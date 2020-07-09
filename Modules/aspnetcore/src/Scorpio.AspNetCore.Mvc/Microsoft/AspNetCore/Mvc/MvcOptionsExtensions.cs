
using Scorpio.AspNetCore.Mvc.Filters;

namespace Microsoft.AspNetCore.Mvc
{
    internal static class MvcOptionsExtensions
    {
        public static void AddScorpio(this MvcOptions options)
        {
            AddFilters(options);
        }

        private static void AddFilters(MvcOptions options)
        {
            options.Filters.AddService<AuditingPageFilter>();
            options.Filters.AddService<AuditingActionFilter>();

        }
    }
}
