using System.Collections.Generic;
using System.Threading.Tasks;

using Scorpio.Aspects;

namespace Scorpio.Authorization
{
    [Authorize("Permission_Test_1.Permission_Test_2")]
  internal class AuthorizeAttributeTestService : IAuthorizeAttributeTestService, IAvoidDuplicateCrossCuttingConcerns
    {
        public virtual List<string> AppliedCrossCuttingConcerns { get; } = new List<string>();

        [AllowAnonymous]
        public Task AuthorizeAnonymousAsync() => Task.CompletedTask;

        [Authorize("Permission_Test_1", "Permission_Test_3", RequireAllPermissions = true)]
        public Task AuthorizeByAllAttributeAsync() => Task.CompletedTask;

        [Authorize("Permission_Test_1")]
        public Task AuthorizeByAttributeAsync() => Task.CompletedTask;

        [Authorize("Permission_Test_1", "Permission_Test_1.Permission_Test_2", RequireAllPermissions = true)]
        public Task AuthorizeByNotAllAttributeAsync() => Task.CompletedTask;

        public Task AuthorizeByServcieAsync() => Task.CompletedTask;
    }
}
