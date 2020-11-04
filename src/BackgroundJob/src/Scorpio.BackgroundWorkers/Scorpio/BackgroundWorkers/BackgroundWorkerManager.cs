using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Scorpio.DependencyInjection;
using Scorpio.Threading;

namespace Scorpio.BackgroundWorkers
{
    /// <summary>
    /// Implements <see cref="IBackgroundWorkerManager"/>.
    /// </summary>
    public class BackgroundWorkerManager : IBackgroundWorkerManager, ISingletonDependency, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        protected bool IsRunning { get; private set; }

        private bool _disposedValue;
        private readonly List<IBackgroundWorker> _backgroundWorkers;

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyCollection<IBackgroundWorker> BackgroundWorkers =>_backgroundWorkers;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundWorkerManager"/> class.
        /// </summary>
        public BackgroundWorkerManager()
        {
            _backgroundWorkers = new List<IBackgroundWorker>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="worker"></param>
        public virtual void Add(IBackgroundWorker worker)
        {
            _backgroundWorkers.Add(worker);
            if (IsRunning)
            {
                AsyncHelper.RunSync(
                    () => worker.StartAsync()
                );
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task StartAsync(CancellationToken cancellationToken = default)
        {
            IsRunning = true;

            foreach (var worker in _backgroundWorkers)
            {
                await worker.StartAsync(cancellationToken);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task StopAsync(CancellationToken cancellationToken = default)
        {
            IsRunning = false;

            foreach (var worker in _backgroundWorkers)
            {
                await worker.StopAsync(cancellationToken);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    this.Action(IsRunning, i => AsyncHelper.RunSync(() => StopAsync()));
                }
                _disposedValue = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
