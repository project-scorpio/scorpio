using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Threading
{
    /// <summary>
    /// 
    /// </summary>
    public static class RunnableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="runnable"></param>
        public static void Start(this IRunnable runnable)
        {
            Check.NotNull(runnable, nameof(runnable));

            AsyncHelper.RunSync(() => runnable.StartAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runnable"></param>
        public static void Stop(this IRunnable runnable)
        {
            Check.NotNull(runnable, nameof(runnable));

            AsyncHelper.RunSync(() => runnable.StopAsync());
        }
    }
}
