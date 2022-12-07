using System;
using System.Collections.Generic;
using System.Text;

using Scorpio.DependencyInjection.Conventional;

using Shouldly;

using Xunit;

namespace Scorpio.Conventional
{
    public class ConventionalConfigurationExtensions_Tests
    {
        [Fact]
        public void Where()
        {
            Predicate<Type> func = (Type t) => t.Name == "ConventionalConfigurationExtensions_Tests";
            var config=new ConventionalConfiguration<ConventionalDependencyAction>(null,null);
            config.Where(func);
            config.GetContexts().ShouldHaveSingleItem().TypePredicate.Compile().Invoke(typeof(ConventionalConfigurationExtensions_Tests)).ShouldBeTrue();
        }
    }
}
