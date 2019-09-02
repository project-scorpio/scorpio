using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Scorpio.Threading
{
    /// <summary>
    /// 
    /// </summary>
    public class NoneCancellationTokenProvider : ICancellationTokenProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public static NoneCancellationTokenProvider Instance { get; } = new NoneCancellationTokenProvider();

        /// <summary>
        /// 
        /// </summary>
        public CancellationToken Token { get; } = CancellationToken.None;

        private NoneCancellationTokenProvider()
        {

        }
    }
}
