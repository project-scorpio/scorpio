using System.Linq;
using System.Reflection;

namespace Scorpio
{
    public static class SubstituteExtensions
    {

        public static object GetProperty(this object target, string name, params object[] args)
        {
            var type = target.GetType();
            var method = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                             .Single(x => x.Name == name);
            return method.GetValue(target, args);
        }
    }
}
