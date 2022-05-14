using System;
using System.Threading;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DisposeAction : IDisposable
    {
        private readonly Action _action;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public DisposeAction(Action action) => _action = action ?? throw new ArgumentNullException(nameof(action));

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        private void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _action();
                }
                _disposedValue = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose() => Dispose(true);
        #endregion

    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class AsyncDisposeAction : IAsyncDisposable
    {
        private readonly Func<ValueTask> _action;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public AsyncDisposeAction (Func<ValueTask> action)
        {
            _action = action;
        }
        private int _disposedValue = 0; // To detect redundant calls

        private async ValueTask DisposeAsync(bool disposing)
        {
            if (Interlocked.CompareExchange(ref _disposedValue,1,0)==0)
            {
                if (disposing)
                {
                   await _action();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ValueTask DisposeAsync() => DisposeAsync(true);
    }
}
