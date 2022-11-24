using System;

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
