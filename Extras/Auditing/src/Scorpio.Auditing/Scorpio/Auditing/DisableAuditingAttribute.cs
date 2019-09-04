using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DisableAuditingAttribute : Attribute
    {

    }
}
