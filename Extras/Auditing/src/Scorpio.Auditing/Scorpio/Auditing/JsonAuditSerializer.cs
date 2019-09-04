using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonAuditSerializer : IAuditSerializer, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        protected AuditingOptions Options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public JsonAuditSerializer(IOptions<AuditingOptions> options)
        {
            Options = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Serialize(object obj)
        {
            var options = new JsonSerializerSettings
            {
                ContractResolver = new AuditingContractResolver(Options.IgnoredTypes)
            };

            return JsonConvert.SerializeObject(obj, options);
        }
    }
}
