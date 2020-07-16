using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

namespace Scorpio.Modularity
{
    internal class ModuleManager : IModuleManager
    {
        private readonly IModuleContainer _moduleContainer;
        private readonly ILogger<ModuleManager> _logger;

        public ModuleManager(
            IModuleContainer moduleContainer,
            ILogger<ModuleManager> logger)
        {
            _moduleContainer = moduleContainer;
            _logger = logger;

        }

        public void InitializeModules(ApplicationInitializationContext  applicationInitializationContext)
        {
            LogListOfModules();

            _moduleContainer.Modules.ForEach(d => d.Instance.PreInitialize(applicationInitializationContext));
            _moduleContainer.Modules.ForEach(d => d.Instance.Initialize(applicationInitializationContext));
            _moduleContainer.Modules.ForEach(d => d.Instance.PostInitialize(applicationInitializationContext));

            _logger.LogInformation("Initialized all modules.");
        }

        private void LogListOfModules()
        {
            _logger.LogInformation("Loaded modules:");

            foreach (var module in _moduleContainer.Modules)
            {
                _logger.LogInformation($"- ({module.Type.FullName})");
            }
        }

        public void ShutdownModules(ApplicationShutdownContext  applicationShutdownContext)
        {
            var modules = _moduleContainer.Modules.Reverse().ToList();
            modules.ForEach(d => d.Instance.Shutdown(applicationShutdownContext));
        }
    }

}
