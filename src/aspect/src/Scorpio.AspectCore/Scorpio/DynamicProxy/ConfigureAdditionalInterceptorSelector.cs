using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using AspectCore.Configuration;
using AspectCore.DynamicProxy;
using IAspectInterceptor=AspectCore.DynamicProxy.IInterceptor;
namespace Scorpio.DynamicProxy
{
    [NonAspect]
    class ConfigureAdditionalInterceptorSelector : IAdditionalInterceptorSelector
    {
        private readonly IAspectConfiguration _aspectConfiguration;
        private readonly IServiceProvider _serviceProvider;

        public ConfigureAdditionalInterceptorSelector(IAspectConfiguration aspectConfiguration, IServiceProvider serviceProvider)
        {
            _aspectConfiguration = aspectConfiguration ?? throw new ArgumentNullException(nameof(aspectConfiguration));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IEnumerable<IAspectInterceptor> Select(MethodInfo serviceMethod, MethodInfo implementationMethod)
        {
            //todo fix nonaspect
            foreach (var interceptorFactory in _aspectConfiguration.Interceptors)
            {
                if (interceptorFactory.Predicates.Length != 0)
                {
                    if (interceptorFactory.CanCreated(implementationMethod))
                        yield return interceptorFactory.CreateInstance(_serviceProvider);
                }
                else
                {
                    if (!_aspectConfiguration.NonAspectPredicates.Any(x => x(implementationMethod)))
                        yield return interceptorFactory.CreateInstance(_serviceProvider);
                }
            }
        }

    }
}
