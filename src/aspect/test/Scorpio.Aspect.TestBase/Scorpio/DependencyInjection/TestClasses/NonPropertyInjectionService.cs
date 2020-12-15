using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.DependencyInjection.TestClasses
{
    public class NonPropertyInjectionService:ITransientDependency
    {
        [NotAutowired]
        public PropertyService  PropertyService { get; set; }
    }
}
