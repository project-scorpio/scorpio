using System.Collections.Concurrent;
using System.Reflection;
using System.Threading.Tasks;

namespace Scorpio.DynamicProxy
{
    /// <summary>
    /// 
    /// </summary>
    public static class IMethodInvocationRuntimeExtensions
    {
        private static readonly ConcurrentDictionary<MethodInfo, bool> _isAsyncCache = new ConcurrentDictionary<MethodInfo, bool>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        public static bool IsAsync(this IMethodInvocation invocation)
        {

            Check.NotNull(invocation,nameof(invocation));

            var isAsyncFromMetaData = _isAsyncCache.GetOrAdd(invocation.Method, IsAsyncFromMetaData);
            if (isAsyncFromMetaData)
            {
                return true;
            }

            if (invocation.ReturnValue != null)
            {
                return IsAsyncType(invocation.ReturnValue.GetType().GetTypeInfo());
            }

            return false;
        }

        private static bool IsAsyncFromMetaData(MethodInfo method)
        {
            if (IsAsyncType(method.ReturnType.GetTypeInfo()))
            {
                return true;
            }

            return false;
        }

        private static bool IsAsyncType(TypeInfo typeInfo)
        {
            if (typeInfo.AsType() == typeof(Task))
            {
                return true;
            }

            if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Task<>))
            {
                return true;
            }

            if (typeInfo.AsType() == typeof(ValueTask))
            {
                return true;
            }

            if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(ValueTask<>))
            {
                return true;
            }

            return false;
        }
    }
}
