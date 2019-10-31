using Microsoft.AspNetCore.Http;
using Scorpio.Uow;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.AspNetCore.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitOfWorkMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="unitOfWorkManager"></param>
        public UnitOfWorkMiddleware(RequestDelegate next,IUnitOfWorkManager unitOfWorkManager)
        {
            _next = next;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            using (var uow=_unitOfWorkManager.Begin())
            {
                await _next(httpContext);
                await uow.CompleteAsync(httpContext.RequestAborted);
            }
        }
    }
}
