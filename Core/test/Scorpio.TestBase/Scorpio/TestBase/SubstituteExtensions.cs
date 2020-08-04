using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Scorpio
{
    public static class SubstituteExtensions
    {
        public static object Protected(this object target, string name, params object[] args)
        {
            var type = target.GetType();
            var method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                             .Where(x => x.Name == name).Single();
            return method.Invoke(target, args);
        }
    }
}
