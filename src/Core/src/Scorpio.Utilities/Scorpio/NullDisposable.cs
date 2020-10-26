using System;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class NullDisposable : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public static NullDisposable Instance { get; } = new NullDisposable();

        private NullDisposable()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            // Method intentionally left empty.
        }
    }
}
