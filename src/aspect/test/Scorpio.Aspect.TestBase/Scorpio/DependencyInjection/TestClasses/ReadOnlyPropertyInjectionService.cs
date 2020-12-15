using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.DependencyInjection.TestClasses
{
    public class ReadOnlyPropertyInjectionService:ITransientDependency
    {
        public PropertyService PropertyService { get; }
    }
}
