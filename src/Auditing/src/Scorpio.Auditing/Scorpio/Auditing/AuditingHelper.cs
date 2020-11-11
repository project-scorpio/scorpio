using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

using Scorpio.DependencyInjection;
using Scorpio.Security;
using Scorpio.Timing;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditingHelper : IAuditingHelper, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        protected ILogger<AuditingHelper> Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IAuditingStore AuditingStore { get; }

        /// <summary>
        /// 
        /// </summary>
        public IClock Clock { get; set;}

        /// <summary>
        /// 
        /// </summary>
        protected AuditingOptions Options{get; }

        /// <summary>
        /// 
        /// </summary>
        protected IAuditSerializer AuditSerializer{get; }

        /// <summary>
        /// 
        /// </summary>
        private readonly ICurrentPrincipalAccessor _principalAccessor;

        /// <summary>
        /// 
        /// </summary>
        protected IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditSerializer"></param>
        /// <param name="options"></param>
        /// <param name="auditingStore"></param>
        /// <param name="principalAccessor"></param>
        public AuditingHelper(
            IAuditSerializer auditSerializer,
            IOptions<AuditingOptions> options,
            IAuditingStore auditingStore,
            ICurrentPrincipalAccessor principalAccessor
            )
        {
            Options = options.Value;
            AuditSerializer = auditSerializer;
            AuditingStore = auditingStore;
            Logger = NullLogger<AuditingHelper>.Instance;
            _principalAccessor = principalAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public virtual bool ShouldSaveAudit(MethodInfo methodInfo, bool defaultValue = false)
        {
            if (methodInfo == null)
            {
                return false;
            }

            if (!methodInfo.IsPublic)
            {
                return false;
            }

            if (methodInfo.IsDefined(typeof(AuditedAttribute), true))
            {
                return true;
            }

            if (methodInfo.IsDefined(typeof(DisableAuditingAttribute), true))
            {
                return false;
            }

            var classType = methodInfo.DeclaringType;
            if (classType != null)
            {
                return ShouldAuditTypeByDefault(classType, defaultValue);
            }
            return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ShouldAuditTypeByDefault(Type type, bool defaultValue = false)
        {
            if (type.IsDefined(typeof(AuditedAttribute), true))
            {
                return true;
            }

            if (type.IsDefined(typeof(DisableAuditingAttribute), true))
            {
                return false;
            }
            return defaultValue;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual AuditInfo CreateAuditInfo()
        {
            var auditInfo = new AuditInfo
            {
                ApplicationName = Options.ApplicationName,
                CurrentUser = _principalAccessor.Principal?.Identity?.Name,
                ExecutionTime = Clock.Now
            };

            ExecutePreContributors(auditInfo);

            return auditInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="implementationMethod"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual AuditActionInfo CreateAuditAction(Type type, MethodInfo implementationMethod, object[] parameters) => CreateAuditAction(type, implementationMethod, CreateArgumentsDictionary(implementationMethod, parameters));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="implementationMethod"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual AuditActionInfo CreateAuditAction(Type type, MethodInfo implementationMethod, IDictionary<string, object> parameters)
        {
            var actionInfo = new AuditActionInfo
            {
                ServiceName = type != null
                    ? type.FullName
                    : "",
                MethodName = implementationMethod.Name,
                Parameters = SerializeConvertArguments(parameters),
                ExecutionTime = Clock.Now
            };

            return actionInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditLogInfo"></param>
        protected virtual void ExecutePreContributors(AuditInfo auditLogInfo)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var context = new AuditContributionContext(scope.ServiceProvider, auditLogInfo);

                foreach (var contributor in Options.Contributors)
                {
                    try
                    {
                        contributor.PreContribute(context);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogException(ex, LogLevel.Warning);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        protected virtual string SerializeConvertArguments(IDictionary<string, object> arguments)
        {
            try
            {
                if (arguments.IsNullOrEmpty())
                {
                    return "{}";
                }

                var dictionary = new Dictionary<string, object>();

                foreach (var argument in arguments)
                {
                    if (argument.Value != null && Options.IgnoredTypes.Any(t => t.IsInstanceOfType(argument.Value)))
                    {
                        dictionary[argument.Key] = null;
                    }
                    else
                    {
                        dictionary[argument.Key] = argument.Value;
                    }
                }

                return AuditSerializer.Serialize(dictionary);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogLevel.Warning);
                return "{}";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        protected virtual Dictionary<string, object> CreateArgumentsDictionary(MethodInfo method, object[] arguments)
        {
            var parameters = method.GetParameters();
            var dictionary = new Dictionary<string, object>();

            for (var i = 0; i < parameters.Length; i++)
            {
                dictionary[parameters[i].Name] = arguments[i];
            }

            return dictionary;
        }
    }
}
