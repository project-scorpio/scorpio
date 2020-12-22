using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AspectCore.Configuration;
using AspectCore.DynamicProxy;

namespace Scorpio.DynamicProxy
{
    public sealed class ConfigureAdditionalAspectValidationHandler : IAspectValidationHandler
    {
        private readonly IAspectConfiguration _aspectConfiguration;

        public ConfigureAdditionalAspectValidationHandler(IAspectConfiguration aspectConfiguration)
        {
            _aspectConfiguration = aspectConfiguration ?? throw new ArgumentNullException(nameof(aspectConfiguration));
        }

        public int Order { get; } = 11;

        public bool Invoke(AspectValidationContext context, AspectValidationDelegate next)
        {
            if (!context.StrictValidation)
            {
                var method = context.Method;
                if (_aspectConfiguration.NonAspectPredicates.Any(x => x(method)))
                {
                    return false;
                }
                if (_aspectConfiguration.Interceptors.Where(x => x.Predicates.Length != 0).Any(x => x.CanCreated(method)))
                {
                    return true;
                }
                if (_aspectConfiguration.Interceptors.Where(x => x.Predicates.Length == 0).Any(x => x.CanCreated(method)))
                {
                    return true;
                }
            }
           
            return next(context);
        }
    }
}
