using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Scorpio.BackgroundWorkers
{
    /// <summary>
    /// Base class that can be used to implement <see cref="IBackgroundWorker"/>.
    /// </summary>
    public abstract class BackgroundWorkerBase : IBackgroundWorker
    {

        /// <summary>
        /// 
        /// </summary>
        public ILogger<BackgroundWorkerBase> Logger { protected get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected BackgroundWorkerBase(IServiceProvider serviceProvider)
        {
            Logger =serviceProvider.GetService<ILogger<BackgroundWorkerBase>>()??NullLogger<BackgroundWorkerBase>.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task StartAsync(CancellationToken cancellationToken = default)
        {
            Logger.LogDebug("Started background worker: " + ToString());
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task StopAsync(CancellationToken cancellationToken = default)
        {
            Logger.LogDebug("Stopped background worker: " + ToString());
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetType().FullName;
        }
    }
}
