using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.DependencyInjection.TestClasses
{
    public class PropertyInjectionService:ITransientDependency
    {
        public PropertyService  PropertyService { get; set; }
    }
}
