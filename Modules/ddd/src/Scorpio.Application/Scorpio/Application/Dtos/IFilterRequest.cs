using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Scorpio.Application.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFilterRequest
    {
        /// <summary>
        /// 
        /// </summary>
        string Where { get; }

        /// <summary>
        /// 
        /// </summary>
        object[] Parameters { get; }
    }

}
