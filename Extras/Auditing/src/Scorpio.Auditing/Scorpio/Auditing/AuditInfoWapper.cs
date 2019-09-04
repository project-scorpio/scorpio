using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AuditInfoWapper
    {
        private readonly AuditInfo _auditInfo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditInfo"></param>
        internal protected AuditInfoWapper(AuditInfo auditInfo)
        {
            _auditInfo = auditInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        public string CurrentUser { get=>_auditInfo.CurrentUser; set=>_auditInfo.CurrentUser=value; }

        /// <summary>
        /// 
        /// </summary>
        public string ImpersonatorUser { get=>_auditInfo.ImpersonatorUser; set=>_auditInfo.ImpersonatorUser=value; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ExecutionTime { get => _auditInfo.ExecutionTime; set => _auditInfo.ExecutionTime = value; }

        /// <summary>
        /// 
        /// </summary>
        public IList<AuditActionInfo> Actions => _auditInfo.Actions;

        /// <summary>
        /// 
        /// </summary>
        public IList<Exception> Exceptions => _auditInfo.Exceptions;

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, object> ExtraProperties => _auditInfo.ExtraProperties;

        /// <summary>
        /// 
        /// </summary>
        public IList<string> Comments => _auditInfo.Comments;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        protected T GetExtraProperty<T>(string name)
        {
            return (T)(ExtraProperties.GetOrDefault(name) ?? default(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        protected void SetExtraProperty(string name, object value)
        {
            ExtraProperties[name] = value;
        }

    }
}
