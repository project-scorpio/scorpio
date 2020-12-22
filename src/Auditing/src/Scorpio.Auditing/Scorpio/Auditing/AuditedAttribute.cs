using System;
namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false)]
    public class AuditedAttribute : Attribute
    {

    }
}
