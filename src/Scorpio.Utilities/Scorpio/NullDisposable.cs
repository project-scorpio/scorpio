using System;
using System.Threading.Tasks;

namespace System
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

    /// <summary>
    /// 
    /// </summary>
    public sealed class NullAsyncDispose : IAsyncDisposable
    {

        /// <summary>
        /// 
        /// </summary>
        public static readonly NullAsyncDispose Instance = new NullAsyncDispose();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ValueTask DisposeAsync() => new ValueTask();
    }
}
