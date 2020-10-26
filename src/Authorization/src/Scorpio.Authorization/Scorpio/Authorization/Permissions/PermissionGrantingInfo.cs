namespace Scorpio.Authorization.Permissions
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionGrantingInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public static PermissionGrantingInfo NonGranted { get; } = new PermissionGrantingInfo(false);

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsGranted { get; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ProviderKey { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isGranted"></param>
        /// <param name="providerKey"></param>
        public PermissionGrantingInfo(bool isGranted, string providerKey = null)
        {
            IsGranted = isGranted;
            ProviderKey = providerKey;
        }

    }
}