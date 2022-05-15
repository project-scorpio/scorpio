using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

using Castle.DynamicProxy;

using Scorpio.DynamicProxy;

namespace Scorpio.Castle.DynamicProxy
{
    /// <summary>
    /// 
    /// </summary>
    internal abstract class CastleMethodInvocationAdapterBase : IMethodInvocation
    {
        /// <summary>
        /// 
        /// </summary>
        public object[] Arguments => Invocation.Arguments;

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyDictionary<string, object> ArgumentsDictionary => _lazyArgumentsDictionary.Value;

        private readonly Lazy<IReadOnlyDictionary<string, object>> _lazyArgumentsDictionary;

        /// <summary>
        /// 
        /// </summary>
        public Type[] GenericArguments => Invocation.GenericArguments;

        /// <summary>
        /// 
        /// </summary>
        public object TargetObject => Invocation.InvocationTarget ?? Invocation.MethodInvocationTarget;

        /// <summary>
        /// 
        /// </summary>
        public MethodInfo Method => Invocation.MethodInvocationTarget ?? Invocation.Method;

        /// <summary>
        /// 
        /// </summary>
        public object ReturnValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IInvocation Invocation { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        protected CastleMethodInvocationAdapterBase(IInvocation invocation)
        {
            Invocation = invocation;
            _lazyArgumentsDictionary = new Lazy<IReadOnlyDictionary<string, object>>(GetArgumentsDictionary);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract Task ProceedAsync();

        private IReadOnlyDictionary<string, object> GetArgumentsDictionary()
        {
            var dict = new Dictionary<string, object>();

            var methodParameters = Method.GetParameters();
            for (var i = 0; i < methodParameters.Length; i++)
            {
                dict[methodParameters[i].Name] = Invocation.Arguments[i];
            }

            return dict;
        }
    }
}
