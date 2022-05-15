using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.DynamicProxy
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProxyConventionalActionContext
    {
        /// <summary>
        /// 
        /// </summary>
        ITypeList<IInterceptor> Interceptors { get; }

        /// <summary>
        /// 
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        /// 
        /// </summary>
        Expression<Func<Type, bool>> TypePredicate { get; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<Type> Types { get; }
    }
}