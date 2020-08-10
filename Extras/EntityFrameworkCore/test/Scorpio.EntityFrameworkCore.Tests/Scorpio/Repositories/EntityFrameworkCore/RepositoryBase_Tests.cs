using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Entities;
using Scorpio.EntityFrameworkCore;
using Scorpio.TestBase;

namespace Scorpio.Repositories.EntityFrameworkCore
{
    public partial class RepositoryBase_Tests:IntegratedTest<TestModule>
    {
        private IRepository<TestTable, int> GetUsers()
        {
            var repo = ServiceProvider.GetRequiredService<IRepository<TestTable, int>>();
            return repo;
        }
    }
}
