using System.Diagnostics.CodeAnalysis;

namespace Scorpio.RabbitMQ
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class RabbitMQOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString { get;  set; }
    }
}