using System;
using System.Collections.Generic;
using System.Linq;

namespace Scorpio.Middleware.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPipelineContext"></typeparam>
    public abstract class PipelineBuilder<TPipelineContext> : IPipelineBuilder<TPipelineContext>
    {
        private readonly IList<Func<PipelineRequestDelegate<TPipelineContext>, PipelineRequestDelegate<TPipelineContext>>> _middlewares;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        protected PipelineBuilder(IServiceProvider serviceProvider)
        {
            ApplicationServices = serviceProvider;
            _middlewares = new List<Func<PipelineRequestDelegate<TPipelineContext>, PipelineRequestDelegate<TPipelineContext>>>();
        }

        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider ApplicationServices { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PipelineRequestDelegate<TPipelineContext> Build()
        {
            var app = TailDelegate;
            foreach (var component in _middlewares.Reverse())
            {
                app = component(app);
            }
            return app;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract PipelineRequestDelegate<TPipelineContext> TailDelegate { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="middleware"></param>
        /// <returns></returns>
        public IPipelineBuilder<TPipelineContext> Use(Func<PipelineRequestDelegate<TPipelineContext>, PipelineRequestDelegate<TPipelineContext>> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }
    }
}
