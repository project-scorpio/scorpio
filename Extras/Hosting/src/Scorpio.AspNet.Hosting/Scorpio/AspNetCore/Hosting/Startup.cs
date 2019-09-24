using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting.Internal;
using System.Reflection;
using System.Globalization;
using System.Linq;

namespace Scorpio.AspNetCore.Hosting
{
    class Startup : StartupBase
    {
        private InternalBootstrapper _bootstrapper;
        private readonly Type _startuModuleType;
        private readonly WebHostBuilderContext _context;
        private readonly Action<BootstrapperCreationOptions> _optionsAction;

        public Startup(Type startuModuleType,WebHostBuilderContext context,Action<BootstrapperCreationOptions> optionsAction)
        {
            _startuModuleType = startuModuleType;
            _context = context;
            _optionsAction = optionsAction;
        }
        public override void ConfigureServices(IServiceCollection services)
        {
            _bootstrapper = new InternalBootstrapper(_startuModuleType, services, _context.Configuration, _optionsAction);
            _bootstrapper.Properties["HostingEnvironment"] = _context.HostingEnvironment;
            services.AddSingleton(_bootstrapper);
        }

        public override void Configure(IApplicationBuilder app)
        {
            _bootstrapper.SetServiceProviderInternal(app.ApplicationServices);
            _bootstrapper.Initialize(app);
            var hostingEnvironment = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();
            var instance = app.ApplicationServices.GetRequiredService(_startuModuleType);
            var action = FindConfigureDelegate(_startuModuleType, hostingEnvironment.EnvironmentName)?.Build(instance);
            action?.Invoke(app);

        }

        public override IServiceProvider CreateServiceProvider(IServiceCollection services)
        {
            return services.BuildAspectInjectorProvider();
        }

        private static ConfigureBuilder FindConfigureDelegate(Type startupModuleType, string environmentName)
        {
            var configureMethod = FindMethod(startupModuleType, "Configure{0}", environmentName, typeof(void), required: false);
            if (configureMethod==null)
            {
                return null;
            }
            return new ConfigureBuilder(configureMethod);
        }

        private static MethodInfo FindMethod(Type startupType, string methodName, string environmentName, Type returnType = null, bool required = true)
        {
            var methodNameWithEnv = string.Format(CultureInfo.InvariantCulture, methodName, environmentName);
            var methodNameWithNoEnv = string.Format(CultureInfo.InvariantCulture, methodName, "");

            var methods = startupType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            var selectedMethods = methods.Where(method => method.Name.Equals(methodNameWithEnv, StringComparison.OrdinalIgnoreCase)).ToList();
            if (selectedMethods.Count > 1)
            {
                throw new InvalidOperationException(string.Format("Having multiple overloads of method '{0}' is not supported.", methodNameWithEnv));
            }
            if (selectedMethods.Count == 0)
            {
                selectedMethods = methods.Where(method => method.Name.Equals(methodNameWithNoEnv, StringComparison.OrdinalIgnoreCase)).ToList();
                if (selectedMethods.Count > 1)
                {
                    throw new InvalidOperationException(string.Format("Having multiple overloads of method '{0}' is not supported.", methodNameWithNoEnv));
                }
            }

            var methodInfo = selectedMethods.FirstOrDefault();
            if (methodInfo == null)
            {
                if (required)
                {
                    throw new InvalidOperationException(string.Format("A public method named '{0}' or '{1}' could not be found in the '{2}' type.",
                        methodNameWithEnv,
                        methodNameWithNoEnv,
                        startupType.FullName));

                }
                return null;
            }
            if (returnType != null && methodInfo.ReturnType != returnType)
            {
                if (required)
                {
                    throw new InvalidOperationException(string.Format("The '{0}' method in the type '{1}' must have a return type of '{2}'.",
                        methodInfo.Name,
                        startupType.FullName,
                        returnType.Name));
                }
                return null;
            }
            return methodInfo;
        }

    }
}
