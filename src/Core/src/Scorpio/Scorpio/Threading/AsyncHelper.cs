using System;
using System.Reflection;
using System.Threading.Tasks;

using Nito.AsyncEx;

namespace Scorpio.Threading
{
    /// <summary>
    /// Provides some helper methods to work with async methods.
    /// </summary>
    public static class AsyncHelper
    {
        /// <summary>
        /// Checks if given method is an async method.
        /// </summary>
        /// <param name="method">A method to check</param>
        public static bool IsAsync(this MethodInfo method)
        {
            Check.NotNull(method, nameof(method));
            return method.ReturnType.IsTask();
        }


        /// <summary>
        /// Checks if given method is an async method.
        /// </summary>
        /// <param name="delegate">A method to check</param>
        /// <returns></returns>
        public static bool IsAsync(this Delegate @delegate)
        {
            Check.NotNull(@delegate, nameof(@delegate));
            return IsAsync(@delegate.Method);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsTask(this Type type)
        {
            return type switch
            {
                Type t when t == typeof(Task) => true,
                Type t when t == typeof(ValueTask) => true,
                Type t when t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Task<>) => true,
                Type t when t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ValueTask<>) => true,
                _ => false
            };
        }

        /// <summary>
        /// Returns void if given type is Task.
        /// Return T, if given type is Task{T}.
        /// Returns given type otherwise.
        /// </summary>
        public static Type UnwrapTask(this Type type)
        {
            Check.NotNull(type, nameof(type));
            return type switch
            {
                Type t when t == typeof(Task) => typeof(void),
                Type t when t == typeof(ValueTask) => typeof(void),
                Type t when t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Task<>) => t.GenericTypeArguments[0],
                Type t when t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ValueTask<>) => t.GenericTypeArguments[0],
                Type t => t
            };
        }

        /// <summary>
        /// Runs a async method synchronously.
        /// </summary>
        /// <param name="func">A function that returns a result</param>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <returns>Result of the async operation</returns>
        public static TResult RunSync<TResult>(this Func<Task<TResult>> func) => AsyncContext.Run(func);

        /// <summary>
        /// Runs a async method synchronously.
        /// </summary>
        /// <param name="action">An async action</param>
        public static void RunSync(this Func<Task> action) => AsyncContext.Run(action);
    }
}
