using System.Linq;

using Scorpio;

namespace System.Collections.Generic
{
    /// <summary>
    /// Extension methods for Collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Checks whatever given collection object is null or has no item.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// Adds an item to the collection if it's not already in the collection.
        /// </summary>
        /// <param name="source">The collection</param>
        /// <param name="item">Item to check and add</param>
        /// <typeparam name="T">Type of the items in the collection</typeparam>
        /// <returns>Returns True if added, returns False if not.</returns>
        public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
        {
            Check.NotNull(source, nameof(source));

            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }

        /// <summary>
        /// Adds an item to the collection if it's not already in the collection.
        /// </summary>
        /// <param name="source">The collection</param>
        /// <param name="item">Item to check and add</param>
        /// <param name="comparer"></param>
        /// <typeparam name="T">Type of the items in the collection</typeparam>
        /// <returns>Returns True if added, returns False if not.</returns>
        public static bool AddIfNotContains<T>(this ICollection<T> source, T item, IEqualityComparer<T> comparer)
        {

            Check.NotNull(source, nameof(source));

            if (source.Contains(item, comparer))
            {
                return false;
            }

            source.Add(item);
            return true;
        }


        /// <summary>
        /// Adds an item to the collection if it's not already in the collection based on the given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="source">The collection</param>
        /// <param name="predicate">The condition to decide if the item is already in the collection</param>
        /// <param name="itemFactory">A factory that returns the item</param>
        /// <typeparam name="T">Type of the items in the collection</typeparam>
        /// <returns>Returns True if added, returns False if not.</returns>
        public static bool AddIfNotContains<T>(this ICollection<T> source, Func<T, bool> predicate, Func<T> itemFactory)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(source, nameof(predicate));
            Check.NotNull(itemFactory, nameof(itemFactory));
            if (source.Any(predicate))
            {
                return false;
            }

            source.Add(itemFactory());
            return true;
        }

        /// <summary>
        /// Removed all items from the collection those satisfy the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the items in the collection</typeparam>
        /// <param name="source">The collection</param>
        /// <param name="predicate">The condition to remove the items</param>
        /// <returns>List of removed items</returns>
        public static ICollection<T> RemoveAll<T>(this ICollection<T> source, Func<T, bool> predicate)
        {
            var items = source.Where(predicate).ToList();

            foreach (var item in items)
            {
                source.Remove(item);
            }

            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <param name="default"></param>
        /// <returns></returns>

        public static T GetOrDefault<T>(this ICollection<T> source, Func<T, bool> selector, T @default = default)
        {
            Check.NotNull(source, nameof(source));
            if (!source.Any(selector))
            {
                return @default;
            }
            var item = source.FirstOrDefault(selector) ?? @default;
            return item;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <param name="factory"></param>
        /// <returns></returns>

        public static T GetOrAdd<T>(this ICollection<T> source, Func<T, bool> selector, Func<T> factory)
        {
            Check.NotNull(source, nameof(source));
            if (!source.Any(selector))
            {
                var result = factory();
                source.Add(result);
                return result;
            }
            return source.First(selector);
        }
    }
}
