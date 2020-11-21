using System;
using System.Linq.Expressions;

using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.DynamicProxy
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ProxyConventionalActionContext
    {
        /// <summary>
        /// 
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// 
        /// </summary>
        public Expression<Func<Type, bool>> TypePredicate { get; }

        /// <summary>
        /// 
        /// </summary>
        public ITypeList<IInterceptor> Interceptors { get; }

        internal ProxyConventionalActionContext(IServiceCollection services, Expression<Func<Type, bool>> typePredicate, ITypeList<IInterceptor> interceptors)
        {
            Services = services;
            TypePredicate = typePredicate;
            Interceptors = interceptors;
        }

    }
}
