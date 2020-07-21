using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.Middleware.Pipeline
{
    public class TestPipelineContext
    {
        public bool PipelineInvoked { get; set; }
    }

    public class TestPipelineBuilder : PipelineBuilder<TestPipelineContext>
    {
        public TestPipelineBuilder(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override PipelineRequestDelegate<TestPipelineContext> TailDelegate => context => Task.CompletedTask;
    }
}
