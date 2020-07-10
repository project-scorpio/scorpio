using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity;

namespace Scorpio.Rpc
{
    /// <summary>
    /// 
    /// </summary>
    public class RpcModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
            base.ConfigureServices(context);
        }
    }
}
