using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Scorpio.Threading
{

    /// <summary>
    /// 
    /// </summary>
  public  interface ICancellationTokenProvider
    {

        /// <summary>
        /// 
        /// </summary>
        CancellationToken Token { get; }
    }
}
