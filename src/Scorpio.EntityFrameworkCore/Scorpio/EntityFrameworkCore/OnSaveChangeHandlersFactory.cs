using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.EntityFrameworkCore
{
    internal class OnSaveChangeHandlersFactory : IOnSaveChangeHandlersFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public OnSaveChangeHandlersFactory(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOnSaveChangeHandler> CreateHandlers() => _serviceProvider.GetService<IEnumerable<IOnSaveChangeHandler>>();
    }
}
