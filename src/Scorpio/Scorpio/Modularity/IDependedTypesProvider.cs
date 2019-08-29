using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Modularity
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDependedTypesProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [NotNull]
        Type[] GetDependedTypes();
    }
}
