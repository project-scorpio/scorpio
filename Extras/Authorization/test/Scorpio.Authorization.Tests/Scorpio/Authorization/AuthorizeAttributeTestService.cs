using System.Threading.Tasks;

namespace Scorpio.Authorization
{
    class AuthorizeAttributeTestService : IAuthorizeAttributeTestService
    {
        public Task AuthorizeAnonymousAsync()
        {
            return Task.CompletedTask;
        }

        public Task AuthorizeByAttributeAsync()
        {
            return Task.CompletedTask;
        }

        public Task AuthorizeByServcieAsync()
        {
            return Task.CompletedTask;
        }
    }
}
