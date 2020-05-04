using Microsoft.AspNetCore.Mvc;
using Scorpio.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scorpio.AspNetCore.Mvc
{
    /// <summary>
    /// 
    /// </summary>
    public static class ActionResultHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static List<Type> ObjectResultTypes { get; }

        static ActionResultHelper()
        {
            ObjectResultTypes = new List<Type>
            {
                typeof(JsonResult),
                typeof(ObjectResult),
                typeof(NoContentResult)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnType"></param>
        /// <returns></returns>
        public static bool IsObjectResult(Type returnType)
        {
            returnType = AsyncHelper.UnwrapTask(returnType);

            if (!typeof(IActionResult).IsAssignableFrom(returnType))
            {
                return true;
            }

            return ObjectResultTypes.Any(t => t.IsAssignableFrom(returnType));
        }
    }
}
