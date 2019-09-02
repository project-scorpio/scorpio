using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ConventionalActionBase
    {
        private readonly IConventionalConfiguration  _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        protected ConventionalActionBase(IConventionalConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal void Action()
        {
            var contexts = _configuration.GetContexts();
            contexts.ForEach(Action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
         protected abstract void Action(IConventionalContext context);
    }
}
