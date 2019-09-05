using System.Collections.Generic;

namespace Scorpio.Authorization.Permissions
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public ITypeList<IPermissionDefinitionProvider> DefinitionProviders { get; }

        /// <summary>
        /// 
        /// </summary>
        public ITypeList<IPermissionGrantingProvider> GrantingProviders { get; }


        /// <summary>
        /// 
        /// </summary>
        public PermissionOptions()
        {
            DefinitionProviders = new TypeList<IPermissionDefinitionProvider>();
            GrantingProviders = new TypeList<IPermissionGrantingProvider>();
        }
    }
}