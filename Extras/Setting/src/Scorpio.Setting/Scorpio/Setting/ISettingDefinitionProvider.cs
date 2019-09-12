using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISettingDefinitionProvider:ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void Define(ISettingDefinitionContext context);
    }
}
