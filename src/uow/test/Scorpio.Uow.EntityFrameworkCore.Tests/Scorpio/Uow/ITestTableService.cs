using System;
using System.Threading.Tasks;

using Scorpio.DependencyInjection;
using Scorpio.Repositories;
using Scorpio.Repositories.EntityFrameworkCore;

namespace Scorpio.Uow
{
    public interface ITestTableService
    {
        public TestTable Get(int id);

        public TestTable Add(TestTable testTable);

        public Task<TestTable> AddAsync(TestTable testTable);
    }

    [UnitOfWork]
    public class TestTableService : ITestTableService, ITransientDependency
    {
        private readonly IRepository<TestTable, int> _repository;

        public TestTableService(IRepository<TestTable, int> repository) => _repository = repository;

        public TestTable Add(TestTable testTable)
        {
            _repository.As<IEfCoreRepository<TestTable>>().DbContext.Database.EnsureCreated();
            return _repository.Insert(testTable, false);
        }

        public Task<TestTable> AddAsync(TestTable testTable)
        {
            _repository.As<IEfCoreRepository<TestTable>>().DbContext.Database.EnsureCreated();
            return _repository.InsertAsync(testTable, false);
        }

        public TestTable Get(int id)
        {
            _repository.As<IEfCoreRepository<TestTable>>().DbContext.Database.EnsureCreated();
            return _repository.Get(id);
        }
    }
}
