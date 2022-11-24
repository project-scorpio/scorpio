using System;
using System.Collections.Generic;
using System.Text;

using EasyNetQ.Topology;

namespace Scorpio.RabbitMQ
{
    /// <summary>
    /// 需要发送NAck并【不用】退回消息时引发该异常。
    /// </summary>
    public class NoAckWithoutRequeueException : Exception
    {

    }
}
