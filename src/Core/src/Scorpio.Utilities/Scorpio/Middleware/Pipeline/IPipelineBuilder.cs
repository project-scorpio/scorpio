using System;

namespace Scorpio.Middleware.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPipelineBuilder<TPipelineContext>
    {
        /// <summary>
        /// 
        /// </summary>
        IServiceProvider ApplicationServices { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="middleware"></param>
        /// <returns></returns>
        IPipelineBuilder<TPipelineContext> Use(Func<PipelineRequestDelegate<TPipelineContext>, PipelineRequestDelegate<TPipelineContext>> middleware);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        PipelineRequestDelegate<TPipelineContext> Build();
    }
}
