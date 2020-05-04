using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio
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
        public DisposeAction(Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
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
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
