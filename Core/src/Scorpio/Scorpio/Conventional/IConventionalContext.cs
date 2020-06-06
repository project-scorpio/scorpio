using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Scorpio.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConventionalContext
    {
        /// <summary>
        /// 
        /// </summary>
        IServiceCollection  Services{ get; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<Type> Types { get; }

        /// <summary>
        /// 
        /// </summary>
        Expression<Func<Type, bool>> TypePredicate { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAction"></typeparam>
    public interface IConventionalContext<out TAction> : IConventionalContext
    {

    }
}
