using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

using EasyNetQ.Topology;

namespace Scorpio.RabbitMQ
{
    /// <summary>
    /// 需要发送NAck并【不用】退回消息时引发该异常。
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class NoAckWithoutRequeueException : Exception
    {

    }
}
