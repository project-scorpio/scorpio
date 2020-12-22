using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.DynamicProxy
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class ProxyConventionalActionContext : IProxyConventionalActionContext
    {
        /// <summary>
        /// 
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Type> Types { get; }

        /// <summary>
        /// 
        /// </summary>
        public Expression<Func<Type, bool>> TypePredicate { get; }

        /// <summary>
        /// 
        /// </summary>
        public ITypeList<IInterceptor> Interceptors { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="types"></param>
        /// <param name="typePredicate"></param>
        /// <param name="interceptors"></param>
        public ProxyConventionalActionContext(IServiceCollection services, System.Collections.Generic.IEnumerable<Type> types, Expression<Func<Type, bool>> typePredicate, ITypeList<IInterceptor> interceptors)
        {
            Services = services;
            Types = types;
            TypePredicate = typePredicate;
            Interceptors = interceptors;
        }

    }
}
