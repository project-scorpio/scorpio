using System;
using System.Collections.Generic;
using System.Text;

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
        void Contributor<TEntity>(ModelCreatingContributionContext<TEntity> context) where TEntity:class;
    }
}
