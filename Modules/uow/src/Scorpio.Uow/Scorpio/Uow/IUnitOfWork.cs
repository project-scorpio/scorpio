using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scorpio.Uow
{
    /// <summary>
    /// Defines a unit of work.
    /// This interface is internally used by framework.
    /// Use <see cref="IUnitOfWorkManager.Begin()"/> to start a new unit of work.
    /// </summary>
    public interface IUnitOfWork : IActiveUnitOfWork, IUnitOfWorkCompleteHandle
    {
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        /// <summary>
        /// This event is raised when this UOW is successfully completed.
        /// </summary>
        event EventHandler Completed;

        /// <summary>
        /// This event is raised when this UOW is failed.
        /// </summary>
        event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// This event is raised when this UOW is disposed.
        /// </summary>
        event EventHandler Disposed;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        /// <summary>
        /// Unique id of this UOW.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Reference to the outer UOW if exists.
        /// </summary>
        IUnitOfWork Outer { get; set; }

        /// <summary>
        /// Begins the unit of work with given options.
        /// </summary>
        /// <param name="options">Unit of work options</param>
        void Begin(UnitOfWorkOptions options);
    }
}
