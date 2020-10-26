using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Entities;

namespace Scorpio.Repositories
{
    public partial class RepositoryBase_Tests
    {
        private (RepositoryBase<User, int>, List<User>) GetUsers()
        {

            var list = new List<User> { };
            var repo = new TestRepository(list, new ServiceCollection().BuildServiceProvider());
            return (repo, list);
        }



        class TestRepository : RepositoryBase<User, int>
        {
            private readonly IList<User> _sources;

            public TestRepository(IList<User> sources, IServiceProvider serviceProvider) : base(serviceProvider)
            {
                _sources = sources;
            }

            public override void Delete(User entity, bool autoSave = true)
            {
                _sources.Remove(entity);
            }

            public override long GetCount()
            {
                return _sources.Count;
            }

            public override IEnumerable<User> GetList(bool includeDetails = false)
            {
                return _sources.ToList();
            }

            public override User Insert(User entity, bool autoSave = true)
            {
                _sources.Add(entity);
                return entity;
            }

            public override User Update(User entity, bool autoSave = true)
            {
                _sources.ReplaceOne(u => u.Id == entity.Id, entity);
                return entity;
            }

            protected override IQueryable<User> GetQueryable()
            {
                return _sources.AsQueryable();
            }
        }
        public class User : Entity<int>
        {
            public string Name { get; set; }
        }
    }
}
