using System.Globalization;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// Extension methods for all objects.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Used to simplify and beautify casting an object to a type. 
        /// </summary>
        /// <typeparam name="T">Type to be casted</typeparam>
        /// <param name="obj">Object to cast</param>
        /// <returns>Casted object</returns>
        public static T As<T>(this object obj)
            where T : class => obj as T;

        /// <summary>
        /// Converts given object to a value type using <see cref="Convert.ChangeType(object,System.Type)"/> method.
        /// </summary>
        /// <param name="obj">Object to be converted</param>
        /// <typeparam name="T">Type of the target object</typeparam>
        /// <returns>Converted object</returns>
        public static T To<T>(this object obj)
            where T : struct => (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T Action<T>(this T obj, Action<T> action)
        {
            action(obj);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async ValueTask<T> Action<T>(this T obj, Func<T, ValueTask> action)
        {
            await action(obj);
            return obj;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T Action<T>(this T obj, bool condition, Action<T> action)
        {
            if (condition)
            {
                action(obj);
            }
            return obj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async ValueTask<T> Action<T>(this T obj, bool condition, Func<T, ValueTask> action)
        {
            if (condition)
            {
                await action(obj);
            }
            return obj;
        }

        /// <summary>
        /// Invoke <see cref="IDisposable.Dispose"/> method if obj is <see cref="IDisposable"/>,otherwise do nothing.
        /// </summary>
        /// <param name="obj"></param>
        public static void SafelyDispose(this object obj)
        {
            if (obj is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static async ValueTask SafelyDisposeAsync(this object obj)
        {
            if (obj is IAsyncDisposable disposable)
            {
                await disposable.DisposeAsync();
            }
        }
    }
}
