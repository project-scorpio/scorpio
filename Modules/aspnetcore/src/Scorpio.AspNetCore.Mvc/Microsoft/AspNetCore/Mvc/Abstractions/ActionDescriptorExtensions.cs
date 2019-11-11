using Microsoft.AspNetCore.Mvc.Controllers;
using Scorpio;
using Scorpio.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Microsoft.AspNetCore.Mvc.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ActionDescriptorExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public static ControllerActionDescriptor AsControllerActionDescriptor(this ActionDescriptor actionDescriptor)
        {
            if (!actionDescriptor.IsControllerAction())
            {
                throw new ScorpioException($"{nameof(actionDescriptor)} should be type of {typeof(ControllerActionDescriptor).AssemblyQualifiedName}");
            }

            return actionDescriptor as ControllerActionDescriptor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public static MethodInfo GetMethodInfo(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.AsControllerActionDescriptor().MethodInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public static Type GetReturnType(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetMethodInfo().ReturnType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public static bool HasObjectResult(this ActionDescriptor actionDescriptor)
        {
            return ActionResultHelper.IsObjectResult(actionDescriptor.GetReturnType());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public static bool IsControllerAction(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor is ControllerActionDescriptor;
        }
    }
}
