using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Scorpio.DependencyInjection;
using Scorpio.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Scorpio.Threading;
using Scorpio.Runtime;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class KernelModule : ScorpioModule
    {

        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.Replace(ServiceDescriptor.Transient(typeof(IOptionsFactory<>), typeof(Options.OptionsFactory<>)));
            context.Services.AddConventionalRegistrar(new BasicConventionalRegistrar());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.TryAddSingleton(typeof(IAmbientScopeProvider<>), typeof(AmbientDataContextAmbientScopeProvider<>));
            context.Services.TryAddSingleton<ICancellationTokenProvider>(NoneCancellationTokenProvider.Instance);
            context.Services.RegisterAssemblyByConventionOfType<KernelModule>();
        }

        public override void PostConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterConventionalInterceptor();
        }
    }
}
