using System;
using System.Threading.Tasks;


using Microsoft.Extensions.DependencyInjection;
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
