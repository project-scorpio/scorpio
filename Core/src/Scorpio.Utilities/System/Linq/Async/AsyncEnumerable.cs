using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Async
{
    /// <summary>
    /// 
    /// </summary>
    public static class AsyncEnumerable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static async Task<bool> AnyAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, Task< bool>> predicate)
        {
            foreach (var item in source)
            {
                if (await predicate(item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static async Task<bool> AllAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, Task<bool>> predicate)
        {
            foreach (var item in source)
            {
                if (!(await predicate(item)))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
