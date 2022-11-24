using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Scorpio.Threading;

namespace Scorpio.BackgroundWorkers
{
    /// <summary>
    /// Extends <see cref="BackgroundWorkerBase"/> to add a periodic running Timer. 
    /// </summary>
    public abstract class PeriodicBackgroundWorkerBase : BackgroundWorkerBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected IServiceScopeFactory ServiceScopeFactory { get; }

        /// <summary>
        /// 
        /// </summary>
        protected ScorpioTimer Timer { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="serviceScopeFactory"></param>
        protected PeriodicBackgroundWorkerBase(
            ScorpioTimer timer,
            IServiceScopeFactory serviceScopeFactory) : base()
        {
            ServiceScopeFactory = serviceScopeFactory;
            Timer = timer;
            Timer.Elapsed += Timer_Elapsed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await base.StartAsync(cancellationToken);
            Timer.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StopAsync(CancellationToken cancellationToken = default)
        {
            Timer.Stop();
            await base.StopAsync(cancellationToken);
        }

        private void Timer_Elapsed(object sender, System.EventArgs e)
        {
            try
            {
                using (var scope = ServiceScopeFactory.CreateScope())
                {
                    DoWork(new BackgroundWorkerContext(scope.ServiceProvider));
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        /// <summary>
        /// Periodic works should be done by implementing this method.
        /// </summary>
        protected abstract void DoWork(BackgroundWorkerContext workerContext);
    }
}
