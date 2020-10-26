using System.Collections.Generic;
using System.Threading.Tasks;

using Scorpio.Aspects;

namespace Scorpio.Authorization
{
    class AuthorizeAttributeTestService : IAuthorizeAttributeTestService, IAvoidDuplicateCrossCuttingConcerns
    {
        public virtual List<string> AppliedCrossCuttingConcerns { get; } = new List<string>();

        public Task AuthorizeAnonymousAsync()
        {
            return Task.CompletedTask;
        }

        public Task AuthorizeByAllAttributeAsync()
        {
            return Task.CompletedTask;
        }

        public Task AuthorizeByAttributeAsync()
        {
            return Task.CompletedTask;
        }

        public Task AuthorizeByNotAllAttributeAsync()
        {
            return Task.CompletedTask;
        }

        public Task AuthorizeByServcieAsync()
        {
            return Task.CompletedTask;
        }
    }
}
