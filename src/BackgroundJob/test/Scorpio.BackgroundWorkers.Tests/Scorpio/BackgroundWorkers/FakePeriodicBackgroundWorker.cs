using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Threading;

namespace Scorpio.BackgroundWorkers
{
    public class FakePeriodicBackgroundWorker : PeriodicBackgroundWorkerBase
    {
        public int InvokeCount { get; set; }=0;

        public ScorpioTimer  ScorpioTimer =>Timer;
        public FakePeriodicBackgroundWorker( ScorpioTimer timer, IServiceScopeFactory serviceScopeFactory) : base(timer, serviceScopeFactory)
        {
            timer.Period=3600000;
            timer.RunOnStart=true;
        }

        protected override void DoWork(BackgroundWorkerContext workerContext)
        {
            InvokeCount++;
        }
    }
}
