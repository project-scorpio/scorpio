using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Uow
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class DisableUnitOfWorkAttribute:Attribute
    {
    }
}
