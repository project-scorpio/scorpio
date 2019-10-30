using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Scorpio.Uow
{
    internal sealed class NullUnitOfWork : UnitOfWorkBase
    {
        public NullUnitOfWork(IServiceProvider serviceProvider, IOptions<UnitOfWorkDefaultOptions> options) : base(serviceProvider, options)
        {
        }

        public override void SaveChanges()
        {
        }

        public override Task SaveChangesAsync( CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override void BeginUow()
        {
        }

        protected override void CompleteUow()
        {
        }

        protected override Task CompleteUowAsync( CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override void DisposeUow()
        {
            
        }
    }
}
