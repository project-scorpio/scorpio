using System;
using System.Threading;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using Scorpio.DependencyInjection;

namespace Scorpio.Threading
{
    /// <summary>
    /// A roboust timer implementation that ensures no overlapping occurs. It waits exactly specified <see cref="Period"/> between ticks.
    /// </summary>
    public class ScorpioTimer : ITransientDependency, IDisposable, IScorpioTimer
    {
        /// <summary>
        /// This event is raised periodically according to Period of Timer.
        /// </summary>
        public event EventHandler Elapsed;

        /// <summary>
        /// Task period of timer (as milliseconds).
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// Indicates whether timer raises Elapsed event on Start method of Timer for once.
        /// Default: False.
        /// </summary>
        public bool RunOnStart { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ILogger{TCategoryName}"/> used to log messages from the timer.
        /// </summary>
        /// <value>The <see cref="ILogger{TCategoryName}"/> used to log messages from the timer.</value>
        public ILogger<ScorpioTimer> Logger { get; set; }

        private readonly Timer _taskTimer;
        private volatile bool _performingTasks;
        private volatile bool _isRunning;
        private bool _disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScorpioTimer"/> class with an infinite
        /// period and an infinite due time.
        /// </summary>
        public ScorpioTimer()
        {
            Logger = NullLogger<ScorpioTimer>.Instance;

            _taskTimer = new Timer(TimerCallBack, null, Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        /// Starts raising the Elapsed event.
        /// </summary>
        public void Start()
        {
            if (Period <= 0)
            {
                throw new ScorpioException("Period should be set before starting the timer!");
            }
            _taskTimer.Locking(t =>
            {
                t.Change(RunOnStart ? 0 : Period, Timeout.Infinite);
                _isRunning = true;
            });
        }

        /// <summary>
        /// Stops raising the Elapsed event.
        /// </summary>
        public void Stop()
        {
            _taskTimer.Locking(t =>
            {
                t.Change(Timeout.Infinite, Timeout.Infinite);
                while (_performingTasks)
                {
                    Monitor.Wait(t);
                }

                _isRunning = false;
            });
        }

        /// <summary>
        /// This method is called by _taskTimer.
        /// </summary>
        /// <param name="state">Not used argument</param>
        private void TimerCallBack(object state)
        {
            _taskTimer.Locking(t =>
            {
                if (!_isRunning || _performingTasks)
                {
                    return;
                }

                t.Change(Timeout.Infinite, Timeout.Infinite);
                _performingTasks = true;
            }
            );
            try
            {
                Elapsed.Invoke(this, new EventArgs());
            }
            catch
            {
                //do nothing.
            }
            finally
            {
                _taskTimer.Locking(t =>
                    {
                        _performingTasks = false;
                        if (_isRunning)
                        {
                            t.Change(Period, Timeout.Infinite);
                        }

                        Monitor.Pulse(_taskTimer);
                    });
            }
        }
        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="ScorpioTimer"/>
        /// and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Stop();
                    _taskTimer.Dispose();
                }
                _disposedValue = true;
            }
        }

        /// <summary>
        /// Releases all resources used by the current instance of <see cref="ScorpioTimer"/>.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
