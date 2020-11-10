using System;

namespace Scorpio.Threading
{
    /// <summary>
    /// A roboust timer interface that ensures no overlapping occurs. It waits exactly specified <see cref="Period"/> between ticks.
    /// </summary>
    public interface IScorpioTimer
    {
        /// <summary>
        /// Task period of timer (as milliseconds).
        /// </summary>
        int Period { get; set; }
        /// <summary>
        /// Indicates whether timer raises Elapsed event on Start method of Timer for once.
        /// Default: False.
        /// </summary>
        bool RunOnStart { get; set; }

        /// <summary>
        /// This event is raised periodically according to Period of Timer.
        /// </summary>
        event EventHandler Elapsed;

        /// <summary>
        /// Starts raising the Elapsed event.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops raising the Elapsed event.
        /// </summary>
        void Stop();
    }
}