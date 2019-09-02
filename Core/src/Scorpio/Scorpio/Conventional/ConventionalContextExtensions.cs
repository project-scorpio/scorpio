using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Scorpio.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConventionalContextExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void Set(this IConventionalContext context,string name,object value)
        {
            (context as ConventionalContext).SetItem(name, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Get<T>(this IConventionalContext context, string name)
        {
            return (context as ConventionalContext).GetItem<T>(name);
        }        /// <summary>
                 /// 
                 /// </summary>
                 /// <typeparam name="T"></typeparam>
                 /// <param name="context"></param>
                 /// <param name="name"></param>
                 /// <param name="value"></param>
                 /// <returns></returns>
        public static T GetOrAdd<T>(this IConventionalContext context, string name,T value)
        {
            var result= (context as ConventionalContext).GetItem<T>(name);
            if (result==default)
            {
                context.Set(name, value);
                result = value;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Expression<Predicate<Type>> GetTypePredicate(this IConventionalContext context)
        {
            return (context as ConventionalContext).TypePredicate;
        }
    }
}
