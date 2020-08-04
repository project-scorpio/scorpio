using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Entities;
using Scorpio.Threading;

namespace Scorpio.Repositories
{
    public partial class BasicRepositoryBase_Tests
    {
        private (BasicRepositoryBase<User, int>, List<User>) GetUsers()
        {

            var list = new List<User> { };
            var repo = new TestRepository(list, new ServiceCollection().BuildServiceProvider());
            return (repo, list);
        }



        class TestRepository : BasicRepositoryBase<User, int>
        {
            private readonly List<User> _sources;

            public TestRepository(List<User> sources, IServiceProvider serviceProvider) : base(serviceProvider)
            {
                _sources = sources;
            }

            public override void Delete(User entity, bool autoSave = true)
            {
                _sources.Remove(entity);
            }

            public override User Find(int id, bool includeDetails = true)
            {
                return _sources.Find(u => u.Id == id);
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

        }
        public class User : Entity<int>
        {
            public string Name { get; set; }
        }
    }
}
