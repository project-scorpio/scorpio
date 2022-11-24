
using Scorpio.DependencyInjection;

namespace Scorpio.EntityFrameworkCore
{
    public interface IStringValueProvider
    {
        string Value { get; set; }
    }

    internal class StringValueProvider : IStringValueProvider, ISingletonDependency
    {
        public string Value { get; set; } = "Row0";
    }
}
