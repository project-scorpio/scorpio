using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EntityFrameworkCore
{
    class OnSaveChangeHandlersFactory : IOnSaveChangeHandlersFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public OnSaveChangeHandlersFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOnSaveChangeHandler> CreateHandlers()
        {
            return _serviceProvider.GetService<IEnumerable<IOnSaveChangeHandler>>();
        }
    }
}
