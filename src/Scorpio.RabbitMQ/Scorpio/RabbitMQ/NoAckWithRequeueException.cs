using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Scorpio.RabbitMQ
{

    /// <summary>
    /// 需要发送NAck并退回消息时引发该异常。
    /// </summary>  
    [ExcludeFromCodeCoverage]
    public class NoAckWithRequeueException : Exception
    {

    }
}
