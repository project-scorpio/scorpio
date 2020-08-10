using System;
using System.Collections.Generic;
using System.Text;

using NSubstitute;

using Scorpio.Data;
using Scorpio.EntityFrameworkCore.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio.EntityFrameworkCore
{
    public class ScorpioDbContextOptions_Tests
    {
        [Fact]
        public void PreConfigure()
        {
            var context = new ScorpioDbContextOptions();
            var action = Substitute.For<Action<DbContextConfigurationContext>>();
            context.PreConfigure(action);
            context.DefaultPreConfigureActions.ShouldHaveSingleItem().Invoke(null);
            action.ReceivedWithAnyArgs(1).Invoke(null);
        }

        [Fact]
        public void PreConfigure_T()
        {
            var context = new ScorpioDbContextOptions();
            var action = Substitute.For<Action<DbContextConfigurationContext<TestDbContext>>>();
            context.PreConfigure(action);
            context.PreConfigureActions
                .ShouldHaveSingleItem()
                .Action(kv => kv.Key.ShouldBe(typeof(TestDbContext)))
                .Value.ShouldHaveSingleItem()
                .ShouldBeOfType<Action<DbContextConfigurationContext<TestDbContext>>>().Invoke(null);
            action.ReceivedWithAnyArgs(1).Invoke(null);
        }

        [Fact]
        public void Configure()
        {
            var context = new ScorpioDbContextOptions();
            var action = Substitute.For<Action<DbContextConfigurationContext>>();
            context.Configure(action);
            context.DefaultConfigureAction.Invoke(null);
            action.ReceivedWithAnyArgs(1).Invoke(null);
        }

        [Fact]
        public void Configure_T()
        {
            var context = new ScorpioDbContextOptions();
            var action = Substitute.For<Action<DbContextConfigurationContext<TestDbContext>>>();
            context.Configure(action);
            context.ConfigureActions
                .ShouldHaveSingleItem()
                .Action(kv=>kv.Key.ShouldBe(typeof(TestDbContext)))
                .Value.ShouldBeOfType<Action<DbContextConfigurationContext<TestDbContext>>>().Invoke(null);
            action.ReceivedWithAnyArgs(1).Invoke(null);
        }

        [Fact]
        public void AddModelCreatingContributor()
        {
            var context = new ScorpioDbContextOptions();
            var  contributor = Substitute.For<IModelCreatingContributor>();
            context.AddModelCreatingContributor(contributor);
            context.GetModelCreatingContributors(typeof(ScorpioDbContext)).ShouldHaveSingleItem().ShouldBe(contributor);
            context.GetModelCreatingContributors(typeof(TestDbContext)).ShouldHaveSingleItem().ShouldBe(contributor);
        }

        [Fact]
        public void AddModelCreatingContributor__T()
        {
            var context = new ScorpioDbContextOptions();
            context.AddModelCreatingContributor<DataModelCreatingContributor>();
            context.GetModelCreatingContributors(typeof(ScorpioDbContext)).ShouldHaveSingleItem().ShouldBeOfType<DataModelCreatingContributor>();
            context.GetModelCreatingContributors(typeof(TestDbContext)).ShouldHaveSingleItem().ShouldBeOfType<DataModelCreatingContributor>();
        }

        [Fact]
        public void AddModelCreatingContributor_T()
        {
            var context = new ScorpioDbContextOptions();
            var contributor = Substitute.For<IModelCreatingContributor>();
            context.AddModelCreatingContributor<TestDbContext>(contributor);
            context.GetModelCreatingContributors(typeof(ScorpioDbContext)).ShouldBeEmpty();
            context.GetModelCreatingContributors(typeof(TestDbContext)).ShouldHaveSingleItem().ShouldBe(contributor);
        }

        [Fact]
        public void AddModelCreatingContributor_T_T()
        {
            var context = new ScorpioDbContextOptions();
            context.AddModelCreatingContributor<TestDbContext, DataModelCreatingContributor>();
            context.GetModelCreatingContributors(typeof(ScorpioDbContext)).ShouldBeEmpty();
            context.GetModelCreatingContributors(typeof(TestDbContext)).ShouldHaveSingleItem().ShouldBeOfType<DataModelCreatingContributor>();
        }

    }
}
