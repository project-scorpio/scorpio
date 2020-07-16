namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModelCreatingContributor
    {
        /// <summary>
        /// 
        /// </summary>
        void Contributor<TEntity>(ModelCreatingContributionContext context) where TEntity : class;
    }
}
