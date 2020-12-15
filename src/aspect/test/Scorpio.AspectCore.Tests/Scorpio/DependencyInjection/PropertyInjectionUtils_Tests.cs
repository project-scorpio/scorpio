using System;
using System.Collections.Generic;
using System.Text;

using Scorpio.DependencyInjection.TestClasses;

using Shouldly;

using Xunit;

namespace Scorpio.DependencyInjection
{
    public class PropertyInjectionUtils_Tests
    {
        [Fact]
        public void TypeRequired()
        {
           Should.Throw<ArgumentNullException>(()=> PropertyInjectionUtils.TypeRequired(null));
           Should.NotThrow(()=> PropertyInjectionUtils.TypeRequired(typeof(PropertyInjectionService))).ShouldBeTrue();
           Should.NotThrow(()=> PropertyInjectionUtils.TypeRequired(typeof(NonPropertyInjectionService))).ShouldBeFalse();
           Should.NotThrow(()=> PropertyInjectionUtils.TypeRequired(typeof(ReadOnlyPropertyInjectionService))).ShouldBeFalse();
        }
    }
}
