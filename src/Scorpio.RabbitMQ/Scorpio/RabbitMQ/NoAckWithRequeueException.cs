using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.RabbitMQ
{

    /// <summary>
    /// 需要发送NAck并退回消息时引发该异常。
    /// </summary>  
    public class NoAckWithRequeueException : Exception
    {

    }
}
