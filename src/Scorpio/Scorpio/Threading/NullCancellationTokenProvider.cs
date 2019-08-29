using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Scorpio.Threading
{
    /// <summary>
    /// 
    /// </summary>
    public class NullCancellationTokenProvider : ICancellationTokenProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public static NullCancellationTokenProvider Instance { get; } = new NullCancellationTokenProvider();

        /// <summary>
        /// 
        /// </summary>
        public CancellationToken Token { get; } = default;

        private NullCancellationTokenProvider()
        {

        }
    }
}
