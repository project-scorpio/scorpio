using Microsoft.Extensions.Logging;
using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void InitializeModules(ApplicationInitializationContext context)
        {
            LogListOfModules();

            _moduleContainer.Modules.ForEach(d => d.Instance.PreInitialize(context));
            _moduleContainer.Modules.ForEach(d => d.Instance.Initialize(context));
            _moduleContainer.Modules.ForEach(d => d.Instance.PostInitialize(context));

            _logger.LogInformation("Initialized all modules.");
        }

        private void LogListOfModules()
        {
            _logger.LogInformation("Loaded modules:");

            foreach (var module in _moduleContainer.Modules)
            {
                _logger.LogInformation($"- ({module.Type.FullName})" );
            }
        }

        public void ShutdownModules(ApplicationShutdownContext context)
        {
            var modules = _moduleContainer.Modules.Reverse().ToList();
            modules.ForEach(d => d.Instance.Shutdown(context));
        }
    }

}
