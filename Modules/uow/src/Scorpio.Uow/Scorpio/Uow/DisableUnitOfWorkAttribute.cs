using System;

namespace Scorpio.Uow
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class DisableUnitOfWorkAttribute : Attribute
    {
    }
}
