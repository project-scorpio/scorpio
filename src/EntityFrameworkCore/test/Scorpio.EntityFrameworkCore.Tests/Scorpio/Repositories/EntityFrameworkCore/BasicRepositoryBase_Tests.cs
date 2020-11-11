
using Microsoft.Extensions.DependencyInjection;

using Scorpio.EntityFrameworkCore;
using Scorpio.TestBase;

namespace Scorpio.Repositories.EntityFrameworkCore
{
    public partial class BasicRepositoryBase_Tests : EntityFrameworkCoreTestBase
    {
        private IBasicRepository<TestTable, int> GetUsers()
        {
            var repo = ServiceProvider.GetRequiredService<IBasicRepository<TestTable, int>>();
            return repo;
        }

    }
}
