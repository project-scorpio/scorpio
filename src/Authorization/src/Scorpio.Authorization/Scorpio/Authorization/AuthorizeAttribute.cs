using System;
using System.Threading.Tasks;


using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeAttribute : Attribute
    {

        /// <summary>
        /// 
        /// </summary>
        public AuthorizeAttribute(params string[] permissions) => Permissions = permissions;

        /// <summary>
        /// 
        /// </summary>
        public string[] Permissions { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool RequireAllPermissions { get; set; }

       
    }
}
