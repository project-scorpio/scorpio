using System;

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
        public static void Set(this IConventionalContext context, string name, object value)
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetOrAdd<T>(this IConventionalContext context, string name, T value)
        {
            var result = (context as ConventionalContext).GetItem<T>(name);
            if (Equals(result, default(T)))
            {
                context.Set(name, value);
                result = value;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static T GetOrAdd<T>(this IConventionalContext context, string name, Func<string, T> factory)
        {
            var result = (context as ConventionalContext).GetItem<T>(name);
            if (Equals(result, default(T)))
            {
                var value = factory(name);
                context.Set(name, value);
                result = value;
            }
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetOrDefault<T>(this IConventionalContext context, string name, T value)
        {
            var result = (context as ConventionalContext).GetItem<T>(name);
            if (Equals(result, default(T)))
            {
                result = value;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static T GetOrDefault<T>(this IConventionalContext context, string name, Func<string, T> factory)
        {
            var result = (context as ConventionalContext).GetItem<T>(name);
            if (Equals(result, default(T)))
            {
                result = factory(name);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IConventionalContext<TAction> Where<TAction>(this IConventionalContext<TAction> context, Predicate<Type> predicate)
        {

            (context as ConventionalContext).AddPredicate(predicate);
            return context;
        }

    }
}
