using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Entities;

namespace Scorpio.Repositories
{
    public partial class BasicRepositoryBase_Tests
    {
        private (BasicRepositoryBase<User, int>, List<User>) GetUsers()
        {

            var list = new List<User> { };
            var repo = new TestRepository(list);
            return (repo, list);
        }

        private class TestRepository : BasicRepositoryBase<User, int>
        {
            private readonly List<User> _sources;

            public TestRepository(List<User> sources) => _sources = sources;

            public override void Delete(User entity, bool autoSave = true) => _sources.Remove(entity);

            public override User Find(int id, bool includeDetails = true) => _sources.Find(u => u.Id == id);

            public override long GetCount() => _sources.Count;

            public override IEnumerable<User> GetList(bool includeDetails = false) => _sources.ToList();

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
