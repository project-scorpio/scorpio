using Scorpio.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditingOptions:ExtensibleOptions
    {
        /// <summary>
        /// Default: true.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// The name of the application or service writing audit logs.
        /// Default: null.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        public bool IsEnabledForAnonymousUsers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<IAuditContributor> Contributors { get; }

        /// <summary>
        /// 
        /// </summary>
        public List<Type> IgnoredTypes { get; }

        

        /// <summary>
        /// 
        /// </summary>
        public AuditingOptions()
        {
            IsEnabled = true;
            IsEnabledForAnonymousUsers = true;

            Contributors = new List<IAuditContributor>();

            IgnoredTypes = new List<Type>
            {
                typeof(Stream),
                typeof(Expression)
            };
        }
    }
}
