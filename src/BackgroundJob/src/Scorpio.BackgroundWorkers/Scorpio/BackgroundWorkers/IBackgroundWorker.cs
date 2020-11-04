using System;
using System.Collections.Generic;
using System.Text;

using Scorpio.DependencyInjection;
using Scorpio.Runtime;

namespace Scorpio.BackgroundWorkers
{
 /// <summary>
    /// Interface for a worker (thread) that runs on background to perform some tasks.
    /// </summary>
    public interface IBackgroundWorker : IRunnable, ISingletonDependency
    {

    }
}
