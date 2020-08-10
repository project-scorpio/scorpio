using System;
using System.Collections.Generic;
using System.Linq;

using Scorpio.EntityFrameworkCore.DependencyInjection;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public class ScorpioDbContextOptions
    {
        private readonly Dictionary<Type, List<IModelCreatingContributor>> _modelCreatingContributors;

        private readonly List<IModelCreatingContributor> _commonModelCreatingContributors;

        internal List<Action<DbContextConfigurationContext>> DefaultPreConfigureActions { get; }

        internal Action<DbContextConfigurationContext> DefaultConfigureAction { get; private set; }

        internal Dictionary<Type, List<object>> PreConfigureActions { get; }

        internal Dictionary<Type, object> ConfigureActions { get; }

        internal IEnumerable<IModelCreatingContributor> GetModelCreatingContributors(Type dbcontextType)
              => _commonModelCreatingContributors.Concat(_modelCreatingContributors.GetOrDefault(dbcontextType,key=> new List<IModelCreatingContributor>()));


        /// <summary>
        /// 
        /// </summary>
        public ScorpioDbContextOptions()
        {
            DefaultPreConfigureActions = new List<Action<DbContextConfigurationContext>>();
            PreConfigureActions = new Dictionary<Type, List<object>>();
            ConfigureActions = new Dictionary<Type, object>();
            _modelCreatingContributors = new Dictionary<Type, List<IModelCreatingContributor>>();
            _commonModelCreatingContributors = new List<IModelCreatingContributor>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void PreConfigure(Action<DbContextConfigurationContext> action)
        {
            Check.NotNull(action, nameof(action));

            DefaultPreConfigureActions.Add(action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="action"></param>
        public void PreConfigure<TDbContext>(Action<DbContextConfigurationContext<TDbContext>> action)
            where TDbContext : ScorpioDbContext<TDbContext>
        {
            Check.NotNull(action, nameof(action));

            var actions = PreConfigureActions.GetOrAdd(typeof(TDbContext), t => new List<object>());
            actions.Add(action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void Configure(Action<DbContextConfigurationContext> action)
        {
            Check.NotNull(action, nameof(action));

            DefaultConfigureAction = action;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="action"></param>
        public void Configure<TDbContext>(Action<DbContextConfigurationContext<TDbContext>> action)
            where TDbContext : ScorpioDbContext<TDbContext>
        {
            Check.NotNull(action, nameof(action));

            ConfigureActions[typeof(TDbContext)] = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelCreatingContributor"></param>
        /// <typeparam name="TDbContext"></typeparam>
        public void AddModelCreatingContributor<TDbContext>(IModelCreatingContributor modelCreatingContributor)
                where TDbContext : ScorpioDbContext<TDbContext>
        {
            var contributors = _modelCreatingContributors.GetOrAdd(typeof(TDbContext),k => new List<IModelCreatingContributor>());
            contributors.AddIfNotContains(modelCreatingContributor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TContributor"></typeparam>
        /// <typeparam name="TDbContext"></typeparam>
        public void AddModelCreatingContributor<TDbContext, TContributor>()
            where TDbContext : ScorpioDbContext<TDbContext>
            where TContributor : class, IModelCreatingContributor
        {
            AddModelCreatingContributor<TDbContext>(Activator.CreateInstance<TContributor>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelCreatingContributor"></param>
        public void AddModelCreatingContributor(IModelCreatingContributor modelCreatingContributor)
        {
            _commonModelCreatingContributors.AddIfNotContains(modelCreatingContributor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TContributor"></typeparam>
        public void AddModelCreatingContributor<TContributor>()
            where TContributor : class, IModelCreatingContributor
        {
            AddModelCreatingContributor(Activator.CreateInstance<TContributor>());
        }
    }
}
