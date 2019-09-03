using Microsoft.Extensions.Options;
using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultConnectionStringResolver : IConnectionStringResolver, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        protected DbConnectionOptions Options { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public DefaultConnectionStringResolver(IOptionsSnapshot<DbConnectionOptions> options)
        {
            Options = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public virtual string Resolve(string connectionStringName = null)
        {
            //Get module specific value if provided
            if (!connectionStringName.IsNullOrEmpty())
            {
                var moduleConnString = Options.ConnectionStrings.GetOrDefault(connectionStringName);
                if (!moduleConnString.IsNullOrEmpty())
                {
                    return moduleConnString;
                }
            }

            //Get default value
            return Options.ConnectionStrings.Default;
        }
    }
}
