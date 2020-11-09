using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.BackgroundJobs
{
    public class FakeBackgroundJob : BackgroundJob<string>
    {
        public override void Execute(string args)
        {
            throw new NotImplementedException();
        }
    }
}
