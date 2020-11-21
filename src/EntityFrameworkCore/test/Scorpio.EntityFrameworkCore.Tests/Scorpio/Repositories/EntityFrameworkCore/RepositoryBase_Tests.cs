
using Microsoft.Extensions.DependencyInjection;

using Scorpio.EntityFrameworkCore;

namespace Scorpio.Repositories.EntityFrameworkCore
{
    public partial class RepositoryBase_Tests : EntityFrameworkCoreTestBase
    {
        private IRepository<TestTable, int> GetUsers()
        {
            var repo = ServiceProvider.GetRequiredService<IRepository<TestTable, int>>();
            return repo;
        }
    }
}
