using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Scorpio.DependencyInjection.Conventional;
using Scorpio.Modularity;
using Scorpio.Runtime;
using Scorpio.Threading;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class KernelModule : ScorpioModule
    {

        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.ReplaceOrAdd(ServiceDescriptor.Transient(typeof(IOptionsFactory<>), typeof(Options.OptionsFactory<>)), true);
            context.AddConventionalRegistrar(new BasicConventionalRegistrar());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.TryAddSingleton(typeof(IAmbientScopeProvider<>), typeof(AmbientDataContextAmbientScopeProvider<>));
            context.Services.TryAddSingleton<ICancellationTokenProvider>(NoneCancellationTokenProvider.Instance);
            context.RegisterAssemblyByConvention();
        }
    }
}
