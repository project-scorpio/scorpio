namespace Scorpio.Authorization.Permissions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPermissionDefinitionContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        PermissionGroupDefinition GetGroupOrNull(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        PermissionGroupDefinition AddGroup(string name, string displayName = null);

    }
}
