using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Scorpio;

namespace System.Linq.Async
{
    public partial class AsyncEnumerable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async ValueTask ForEachAsync<T>(this IAsyncEnumerable<T> enumerable, Action<T> action, CancellationToken cancellationToken = default)
        {
            Check.NotNull(enumerable, nameof(enumerable));
            Check.NotNull(action, nameof(action));
            await Core(enumerable, action, cancellationToken);
            static async ValueTask Core(IAsyncEnumerable<T> enumerable, Action<T> action, CancellationToken cancellationToken)
            {

                await foreach (var item in enumerable.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    action(item);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async ValueTask ForEachAsync<T>(this IAsyncEnumerable<T> enumerable, Action<T, int> action, CancellationToken cancellationToken = default)
        {
            Check.NotNull(enumerable, nameof(enumerable));
            Check.NotNull(action, nameof(action));

            await Core(enumerable, action, cancellationToken);
            static async ValueTask Core(IAsyncEnumerable<T> enumerable, Action<T, int> action, CancellationToken cancellationToken)
            {
                var index = 0;
                await foreach (var item in enumerable.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    action(item, checked(index++));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async ValueTask ForEachAsync<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task> action, CancellationToken cancellationToken = default)
        {
            Check.NotNull(enumerable, nameof(enumerable));
            Check.NotNull(action, nameof(action));

            await Core(enumerable, action, cancellationToken);
            static async ValueTask Core(IAsyncEnumerable<T> enumerable, Func<T, Task> action, CancellationToken cancellationToken)
            {
                await foreach (var item in enumerable.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    await action(item).ConfigureAwait(false);
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static ValueTask ForEachAsync<T>(this IAsyncEnumerable<T> enumerable, Func<T, CancellationToken, Task> action, CancellationToken cancellationToken = default)
        {
            Check.NotNull(enumerable, nameof(enumerable));
            Check.NotNull(action, nameof(action));
            
            return Core(enumerable, action, cancellationToken);
            static async ValueTask Core(IAsyncEnumerable<T> enumerable, Func<T, CancellationToken, Task> action, CancellationToken cancellationToken)
            {
                await foreach (var item in enumerable.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    await action(item, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static  ValueTask ForEachAsync<T>(this IAsyncEnumerable<T> enumerable, Func<T, int, CancellationToken, Task> action, CancellationToken cancellationToken = default)
        {
            Check.NotNull(enumerable, nameof(enumerable));
            Check.NotNull(action, nameof(action));

            return Core(enumerable, action, cancellationToken);
            static async ValueTask Core(IAsyncEnumerable<T> enumerable, Func<T, int, CancellationToken, Task> action, CancellationToken cancellationToken)
            {
                var index = 0;
                await foreach (var item in enumerable.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    await action(item, checked(index++), cancellationToken).ConfigureAwait(false);
                }
            }
        }

    }
}
