using System;
using System.Collections.Generic;
using System.Text;

using Scorpio.DependencyInjection;

namespace Scorpio.EntityFrameworkCore
{
    public interface IStringValueProvider
    {
        string Value { get; set; }
    }

    class StringValueProvider : IStringValueProvider,ISingletonDependency
    {
        public string Value { get; set; } = "Row0";
    }
}
