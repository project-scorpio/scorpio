using System.Collections.Generic;

namespace Scorpio.Authorization.Permissions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPermissionDefinitionManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        PermissionDefinition Get(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        PermissionDefinition GetOrNull(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<PermissionDefinition> GetPermissions();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<PermissionGroupDefinition> GetGroups();
    }
}
