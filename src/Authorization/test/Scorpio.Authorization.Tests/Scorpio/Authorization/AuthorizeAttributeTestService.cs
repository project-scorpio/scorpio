using System.Collections.Generic;
using System.Threading.Tasks;

using Scorpio.Aspects;

namespace Scorpio.Authorization
{
    internal class AuthorizeAttributeTestService : IAuthorizeAttributeTestService, IAvoidDuplicateCrossCuttingConcerns
    {
        public virtual List<string> AppliedCrossCuttingConcerns { get; } = new List<string>();

        public Task AuthorizeAnonymousAsync() => Task.CompletedTask;

        public Task AuthorizeByAllAttributeAsync() => Task.CompletedTask;

        public Task AuthorizeByAttributeAsync() => Task.CompletedTask;

        public Task AuthorizeByNotAllAttributeAsync() => Task.CompletedTask;

        public Task AuthorizeByServcieAsync() => Task.CompletedTask;
    }
}
