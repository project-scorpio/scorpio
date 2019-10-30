using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scorpio.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        private bool _isBeginCalledBefore;
        private bool _isCompleteCalledBefore;
        private bool _succeed;
        private Exception _exception;
        private readonly UnitOfWorkDefaultOptions _defaultOptions;

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 
        /// </summary>
        public IUnitOfWork Outer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UnitOfWorkOptions Options { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Completed;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// 
        /// </summary>
        protected UnitOfWorkBase(
            IServiceProvider serviceProvider,
             IOptions<UnitOfWorkDefaultOptions> options
            )
        {
            _defaultOptions = options.Value;
            ServiceProvider = serviceProvider;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public void Begin(UnitOfWorkOptions options)
        {
            Check.NotNull(options, nameof(options));

            PreventMultipleBegin();
            Options = _defaultOptions.Normalize(options.Clone()); 
            BeginUow();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Complete()
        {
            PreventMultipleComplete();
            try
            {
                CompleteUow();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task CompleteAsync(CancellationToken cancellationToken=default)
        {
            PreventMultipleComplete();
            try
            {
                await CompleteUowAsync(cancellationToken);
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (!_isBeginCalledBefore || IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (!_succeed)
            {
                OnFailed(_exception);
            }

            DisposeUow();
            OnDisposed();
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract void SaveChanges();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract Task SaveChangesAsync(CancellationToken cancellationToken=default);

        /// <summary>
        /// Can be implemented by derived classes to start UOW.
        /// </summary>
        protected abstract void BeginUow();

        /// <summary>
        /// Should be implemented by derived classes to complete UOW.
        /// </summary>
        protected abstract void CompleteUow();

        /// <summary>
        /// Should be implemented by derived classes to complete UOW.
        /// </summary>
        protected abstract Task CompleteUowAsync(CancellationToken cancellationToken=default);

        /// <summary>
        /// Should be implemented by derived classes to dispose UOW.
        /// </summary>
        protected abstract void DisposeUow();

        /// <summary>
        /// Called to trigger <see cref="Completed"/> event.
        /// </summary>
        protected virtual void OnCompleted()
        {
            Completed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called to trigger <see cref="Failed"/> event.
        /// </summary>
        /// <param name="exception">Exception that cause failure</param>
        protected virtual void OnFailed(Exception exception)
        {
            Failed?.Invoke(this, new UnitOfWorkFailedEventArgs(exception));
        }

        /// <summary>
        /// Called to trigger <see cref="Disposed"/> event.
        /// </summary>
        protected virtual void OnDisposed()
        {
            Disposed?.Invoke(this, EventArgs.Empty);
        }


        private void PreventMultipleBegin()
        {
            if (_isBeginCalledBefore)
            {
                throw new ScorpioException("This unit of work has started before. Can not call Start method more than once.");
            }

            _isBeginCalledBefore = true;
        }

        private void PreventMultipleComplete()
        {
            if (_isCompleteCalledBefore)
            {
                throw new ScorpioException("Complete is called before!");
            }

            _isCompleteCalledBefore = true;
        }
    }
}
