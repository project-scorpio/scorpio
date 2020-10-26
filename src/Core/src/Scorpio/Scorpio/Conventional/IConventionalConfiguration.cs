using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConventionalConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        IServiceCollection Services { get; }

    }

    /// <summary>
    /// 
    /// </summary>
    public interface IConventionalConfiguration<out TAction> : IConventionalConfiguration
    {


    }

}
