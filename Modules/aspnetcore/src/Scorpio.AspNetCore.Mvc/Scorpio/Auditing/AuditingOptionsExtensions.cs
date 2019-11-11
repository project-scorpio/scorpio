using Scorpio.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public static class AuditingOptionsExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool IsAuditingController(this AuditingOptions options)
        {
            return options.GetOption<bool>(nameof(IsAuditingController));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public static void EnableAuditingController(this AuditingOptions options)
        {
            options.SetOption(nameof(IsAuditingController), true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public static void DisableAuditingController(this AuditingOptions options)
        {
            options.SetOption(nameof(IsAuditingController), false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool IsAuditingPage(this AuditingOptions options)
        {
            return options.GetOption<bool>(nameof(IsAuditingPage));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public static void EnableAuditingPage(this AuditingOptions options)
        {
            options.SetOption(nameof(IsAuditingPage), true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public static void DisableAuditingPage(this AuditingOptions options)
        {
            options.SetOption(nameof(IsAuditingPage), false);
        }

    }
}
