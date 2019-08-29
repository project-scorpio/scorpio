using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Scorpio.Threading
{
    /// <summary>
    /// 
    /// </summary>
    public static class CancellationTokenProviderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="prefferedValue"></param>
        /// <returns></returns>
        public static CancellationToken FallbackToProvider(this ICancellationTokenProvider provider, CancellationToken prefferedValue = default)
        {
            return prefferedValue == default || prefferedValue == CancellationToken.None
                ? provider.Token
                : prefferedValue;
        }
    }
}
