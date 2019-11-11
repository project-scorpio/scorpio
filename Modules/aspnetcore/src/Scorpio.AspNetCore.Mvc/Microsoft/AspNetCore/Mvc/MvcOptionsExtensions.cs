using Scorpio.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Mvc
{
    internal static class MvcOptionsExtensions
    {
        public static void AddScorpio(this MvcOptions options,IServiceProvider serviceProvider)
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
