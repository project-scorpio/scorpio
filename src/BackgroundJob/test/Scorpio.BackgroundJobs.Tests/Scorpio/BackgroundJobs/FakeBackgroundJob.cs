using System;

namespace Scorpio.BackgroundJobs
{
    public class FakeBackgroundJob : BackgroundJob<string>
    {
        public override void Execute(string args) => throw new NotImplementedException();
    }
}
