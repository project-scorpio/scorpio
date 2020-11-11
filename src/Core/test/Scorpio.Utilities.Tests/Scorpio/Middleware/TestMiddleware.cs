using System;
using System.Threading.Tasks;

using Scorpio.Middleware.Pipeline;

namespace Scorpio.Middleware
{
    public class TestMiddleware
    {
        private readonly PipelineRequestDelegate<TestPipelineContext> _next;

        public TestMiddleware(PipelineRequestDelegate<TestPipelineContext> next) => _next = next;
        public Task InvokeAsync(TestPipelineContext context)
        {
            context.PipelineInvoked = true;
            return _next(context);
        }
    }

    public class NonParameterMiddleware
    {
        private readonly PipelineRequestDelegate<TestPipelineContext> _next;

        public NonParameterMiddleware(PipelineRequestDelegate<TestPipelineContext> next) => _next = next;
        public Task InvokeAsync() => _next(null);
    }

    public class ManyParametersMiddleware
    {
        private readonly PipelineRequestDelegate<TestPipelineContext> _next;

        public ManyParametersMiddleware(PipelineRequestDelegate<TestPipelineContext> next) => _next = next;
        public Task InvokeAsync(TestPipelineContext context, IServiceProvider serviceProvider)
        {
            Check.NotNull(serviceProvider, nameof(serviceProvider));
            context.PipelineInvoked = true;
            return _next(context);
        }
    }

    public class ByRefParametersMiddleware
    {
        private readonly PipelineRequestDelegate<TestPipelineContext> _next;

        public ByRefParametersMiddleware(PipelineRequestDelegate<TestPipelineContext> next) => _next = next;
        public Task InvokeAsync(TestPipelineContext context, ref int age)
        {
            age = 100;
            context.PipelineInvoked = true;
            return _next(context);
        }
    }

    public class FuncResultMiddleware
    {
        private readonly PipelineRequestDelegate<TestPipelineContext> _next;

        public FuncResultMiddleware(PipelineRequestDelegate<TestPipelineContext> next) => _next = next;
        public async Task<string> InvokeAsync(TestPipelineContext context)
        {
            context.PipelineInvoked = true;
            await _next(context);
            return "";
        }
    }
    public class NonMethodMiddleware
    {
        private readonly PipelineRequestDelegate<TestPipelineContext> _next;

        public NonMethodMiddleware(PipelineRequestDelegate<TestPipelineContext> next) => _next = next;

        public void Execute(TestPipelineContext context) => _next(context).Wait();


    }

    public class NotTaskMiddleware
    {
        private readonly PipelineRequestDelegate<TestPipelineContext> _next;

        public NotTaskMiddleware(PipelineRequestDelegate<TestPipelineContext> next) => _next = next;

        public void Invoke(TestPipelineContext context)
        {
            context.PipelineInvoked = true;
            _next(context).Wait();
        }

    }

    public class DoublyMethodMiddleware
    {
        private readonly PipelineRequestDelegate<TestPipelineContext> _next;

        public DoublyMethodMiddleware(PipelineRequestDelegate<TestPipelineContext> next) => _next = next;
        public async Task InvokeAsync(TestPipelineContext context)
        {
            context.PipelineInvoked = true;
            await _next(context);
        }

        public void Invoke(TestPipelineContext context) => context.PipelineInvoked = true;
    }
}
