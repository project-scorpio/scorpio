using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Uow
{
    /// <summary>
    /// Used as event arguments on <see cref="IUnitOfWork.Failed"/> event.
    /// </summary>
    public class UnitOfWorkFailedEventArgs:EventArgs
    {

        /// <summary>
        /// Exception that caused failure. This is set only if an error occured during <see cref="IUnitOfWorkCompleteHandle.Complete"/>.
        /// Can be null if there is no exception, but <see cref="IUnitOfWorkCompleteHandle.Complete"/> is not called. 
        /// Can be null if another exception occurred during the UOW.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public UnitOfWorkFailedEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}
