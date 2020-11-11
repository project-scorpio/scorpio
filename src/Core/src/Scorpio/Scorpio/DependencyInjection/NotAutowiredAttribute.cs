using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class NotAutowiredAttribute : Attribute
    {
    }
}
