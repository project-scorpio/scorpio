using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.BackgroundWorkers
{
    /// <summary>
    /// Options for configuring various behaviors of the default <see cref="IBackgroundWorkerManager"/>  implementation.
    /// </summary>
    public class BackgroundWorkerOptions
    {
        /// <summary>
        /// true to enabled background workers and the background workers will be started when the application is initialized; otherwise false. defaults to true.
        /// </summary>
        public bool IsEnabled { get; set; } = true;
    }
}
